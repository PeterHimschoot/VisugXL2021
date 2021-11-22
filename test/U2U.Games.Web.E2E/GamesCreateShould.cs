using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using FluentAssertions;
using System.Net;

namespace U2U.Games.Web.E2E
{
  public class GamesCreateShould
    : IClassFixture<WebApplicationFactory<Startup>>
    , IClassFixture<DbInjectionFixture<GamesDb>>
    , IClassFixture<TestableGameSpecificationFactories>

  {
    private readonly WebApplicationFactory<Startup> clientFactory;
    private readonly DbInjectionFixture<GamesDb> gamesDbContextFixture;
    private readonly TestableGameSpecificationFactories specificationFactories;

    public GamesCreateShould(WebApplicationFactory<Startup> clientFactory,
      DbInjectionFixture<GamesDb> gamesDbInjectionFixture,
      TestableGameSpecificationFactories specificationFactories)
    {
      this.clientFactory = clientFactory;
      this.gamesDbContextFixture = gamesDbInjectionFixture;
      this.specificationFactories = specificationFactories;
    }

    [Fact]
    public async Task ReturnViewForCreatingGame()
    {
      GamesDb dbContext = DbFixtures.InMemGamesDbFixture().CreateInMemoryDbContext("CreateView_Game");
      dbContext.Database.EnsureCreated();
      Repository<Publisher, GamesDb>? pubRepo = new Repository<Publisher, GamesDb>(dbContext);
      HttpClient client = this.clientFactory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
      {
        services.ReplaceSingleton<IReadonlyRepository<Publisher>>(pubRepo);
      })).CreateClient();

      HttpResponseMessage response = await client.GetAsync(GamesWebUris.Uri_Create);
      response.EnsureSuccessStatusCode();
      Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }

    [Fact()]
    public async Task InsertGameWithPostAndRedirectToIndex()
    {
      GamesDb dbContext = DbFixtures.InMemGamesDbFixture().CreateInMemoryDbContext("Create_Game");
      dbContext.Database.EnsureCreated();

      Repository<Game, GamesDb>? gameRepo = new Repository<Game, GamesDb>(dbContext);
      Repository<Publisher, GamesDb>? pubRepo = new Repository<Publisher, GamesDb>(dbContext);

      string pubName = "Mattel Games";
      await pubRepo.InsertAsync(new Publisher(0, pubName));
      await pubRepo.SaveChangesAsync();
      ISpecification<Publisher>? spec = this.specificationFactories.For<Publisher>()
        .Where(p => p.Name == pubName);
      Publisher? mattelGames = await pubRepo.SingleAsync(spec); // new PublisherWithName(pubName));
      mattelGames.Should().NotBeNull();
      mattelGames!.Name.Should().Be(pubName);

      HttpClient client = this.clientFactory.WithWebHostBuilder(config
        => config.ConfigureTestServices(services =>
        {
          services.ReplaceSingleton<IRepository<Game>>(gameRepo);
          services.ReplaceSingleton<IRepository<Publisher>>(pubRepo);
          services.ReplaceSingleton<IReadonlyRepository<Publisher>>(pubRepo);
        }))
        .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

      string? gameName = "Blokus";
      using (this.gamesDbContextFixture.StartTest())
      {
        FormValues? formValues = new FormValues();
        formValues.Add("Name", gameName);
        formValues.Add("PublisherId", mattelGames.Id.ToString());

        HttpResponseMessage response = await client.PostAsync(GamesWebUris.Uri_Create, formValues);
        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        dbContext.Games.Last().Name.Should().Be(gameName);
      }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(130)]
    public async Task InsertGameWithMissingNameShouldReportValidationErrors(int nameLen)
    {
      string name = string.Concat(Enumerable.Repeat("A", nameLen));
      GamesDb dbContext = DbFixtures.InMemGamesDbFixture().CreateInMemoryDbContext("Create_Game");
      dbContext.Database.EnsureCreated();

      Repository<Game, GamesDb>? gameRepo = new Repository<Game, GamesDb>(dbContext);
      Repository<Publisher, GamesDb>? pubRepo = new Repository<Publisher, GamesDb>(dbContext);

      string pubName = "Mattel Games";
      await pubRepo.InsertAsync(new Publisher(0, pubName));
      await pubRepo.SaveChangesAsync();

      ISpecification<Publisher>? spec = this.specificationFactories.For<Publisher>()
        .Where(p => p.Name == pubName);
      Publisher? mattelGames = await pubRepo.SingleAsync(spec); // new PublisherWithName(pubName));
      mattelGames.Should().NotBeNull();

      HttpClient client = this.clientFactory.WithWebHostBuilder(config
        => config.ConfigureTestServices(services =>
        {
          services.ReplaceSingleton<IRepository<Game>>(gameRepo);
          services.ReplaceSingleton<IRepository<Publisher>>(pubRepo);
          services.ReplaceSingleton<IReadonlyRepository<Publisher>>(pubRepo);
        }))
        .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

      Game? newGame = mattelGames!.CreateGame(name);
      FormValues? formValues = new FormValues();
      formValues.Add(g => newGame.Name);
      formValues.Add("PublisherId", newGame.Publisher.Id.ToString());

      HttpResponseMessage response = await client.PostAsync(GamesWebUris.Uri_Create, formValues);

      string content = await response.Content.ReadAsStringAsync();
      HtmlParser? parser = new HtmlParser();
      IHtmlDocument document = await parser.ParseDocumentAsync(content);
      IHtmlCollection<IElement> listItems = 
        document.QuerySelectorAll($".field-validation-error");

      Assert.NotEmpty(listItems);
    }
  }
}
