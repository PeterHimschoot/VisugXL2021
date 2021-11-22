namespace U2U.Games.Web.E2E;

// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.0

public class GamesIndexShould
    : IClassFixture<WebApplicationFactory<Startup>>
{
  private readonly WebApplicationFactory<Startup> clientFactory;

  public GamesIndexShould(WebApplicationFactory<Startup> clientFactory)
    => this.clientFactory = clientFactory;

  [Fact]
  public async Task FetchIndexPageSuccessfullyWithCorrectContentType()
  {
    GamesDb dbContext = DbFixtures.InMemGamesDbFixture().CreateInMemoryDbContext("IndexView_Game");
    CurrencyDb currencyDb = DbFixtures.InMemCurrencyDbFixture().CreateInMemoryDbContext("IndexView_Game");
    dbContext.Database.EnsureCreated();
    currencyDb.Database.EnsureCreated();
    Repository<Game, GamesDb> gameRepo = new Repository<Game, GamesDb>(dbContext);
    Repository<Publisher, GamesDb> pubRepo = new Repository<Publisher, GamesDb>(dbContext);
    Repository<Currency, CurrencyDb> currencyRepo = new Repository<Currency, CurrencyDb>(currencyDb);
    System.Net.Http.HttpClient client = this.clientFactory.WithWebHostBuilder(builder
      => builder.ConfigureTestServices(services =>
    {
      services.ReplaceSingleton<IRepository<Game>>(gameRepo);
      services.ReplaceSingleton<IRepository<Publisher>>(pubRepo);
      services.ReplaceSingleton<IReadonlyRepository<Publisher>>(pubRepo);
      services.ReplaceSingleton<IReadonlyRepository<Currency>>(currencyRepo);
    })).CreateClient();

    System.Net.Http.HttpResponseMessage response = await client.GetAsync("/");

    response.EnsureSuccessStatusCode();
    Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
  }

  [Fact]
  public async Task ListGamesCorrectly()
  {
    GamesDb dbContext = DbFixtures.InMemGamesDbFixture().CreateInMemoryDbContext("IndexView_Game");
    CurrencyDb currencyDb = DbFixtures.InMemCurrencyDbFixture().CreateInMemoryDbContext("IndexView_Game");
    dbContext.Database.EnsureCreated();
    currencyDb.Database.EnsureCreated();
    Repository<Publisher, GamesDb> pubRepo = new Repository<Publisher, GamesDb>(dbContext);
    Repository<Currency, CurrencyDb> currencyRepo = new Repository<Currency, CurrencyDb>(currencyDb);
    FakeRepositoryFactory<Game> repoFactory = new FakeRepositoryFactory<Game>();
    IRepository<Game> fakeRepo = repoFactory.FakeRepoWithData(Fake.Games);

    HttpClient client = this.clientFactory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureTestServices(services =>
      {
        services.ReplaceSingleton<IRepository<Game>>(fakeRepo);
        services.ReplaceSingleton<IRepository<Publisher>>(pubRepo);
        services.ReplaceSingleton<IReadonlyRepository<Publisher>>(pubRepo);
        services.ReplaceSingleton<IReadonlyRepository<Currency>>(currencyRepo);
      });
    }).CreateClient();

    HttpResponseMessage response = await client.GetAsync("/");

    response.EnsureSuccessStatusCode();

    string content = await response.Content.ReadAsStringAsync();
    HtmlParser parser = new HtmlParser();
    AngleSharp.Html.Dom.IHtmlDocument document = await parser.ParseDocumentAsync(content);
    AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> listItems =
      document.QuerySelectorAll($"div.game");

    Assert.Equal(Fake.Games.Count(), listItems.Length);
  }
}

