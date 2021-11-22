namespace U2U.Games.Core.Tests;

public class GameServiceShould
  : IClassFixture<TestableGameSpecificationFactories>
{
  private readonly TestableGameSpecificationFactories gameSpecificationFactories;

  public GameServiceShould(
    TestableGameSpecificationFactories gameSpecificationFactories)
  {
    this.gameSpecificationFactories = gameSpecificationFactories;
  }

  private GamePriceService CreateGamePriceService()
  {
    var cultureMock = new Mock<ICultureToCurrencyService>();
    cultureMock.Setup(m => m.CurrencyForCultureAsync(It.IsAny<CultureInfo>()))
      .Returns(new ValueTask<CurrencyName>(CurrencyName.USD));
    var conversionMock = new Mock<ICurrencyConverterService>();
    conversionMock.Setup(m => m.ConvertAmountFromAsync(It.IsAny<Money>(), It.IsAny<CurrencyName>()))
     .Returns((Money from, CurrencyName currency) => new ValueTask<Money>(new Money(from.Amount * 2, CurrencyName.USD)));
    var currencyFacade = new CurrencyFacade(conversionMock.Object, cultureMock.Object);
    var priceService = new GamePriceService(currencyFacade);
    return priceService;
  }

  [Theory]
  [ClassData(typeof(SomeCultures))]
  public async Task ReturnGamesWithCorrectRoundedAndConvertedCurrency(string cultureName)
  {
    var ci = new CultureInfo(cultureName);
    var gameRepoFactory = new FakeRepositoryFactory<Game>();
    IRepository<Game> fakeGameRepo = gameRepoFactory.FakeRepoWithData(Fake.Games);
    var pubRepoFactory = new FakeRepositoryFactory<Publisher>();
    IRepository<Publisher> fakePubRepo = pubRepoFactory.FakeRepoWithData(Fake.Publishers);

    var sut = new GameService(fakeGameRepo, fakePubRepo, CreateGamePriceService(), gameSpecificationFactories);

    IAsyncEnumerable<(Game, Money)> result = sut.GamesAsync();
    await foreach ((Game g, Money price) in result)
    {
      Money expected = new Money(g.Price.Amount * 2, CurrencyName.USD).Rounded;
      Assert.Equal(expected, price);
    }
  }

  [Fact]
  public async Task ReturnExistingPublisher()
  {
    var gameRepoFactory = new FakeRepositoryFactory<Game>();
    IRepository<Game> fakeGameRepo = gameRepoFactory.FakeRepoWithData(Fake.Games);
    var pubRepoFactory = new FakeRepositoryFactory<Publisher>();
    IRepository<Publisher> fakePubRepo = pubRepoFactory.FakeRepoWithData(Fake.Publishers);
    var sut = new GameService(fakeGameRepo, fakePubRepo, CreateGamePriceService(), gameSpecificationFactories);
    Publisher? pub = await sut.PublisherWithIdAsync(2);
    Assert.NotNull(pub);
  }
}

