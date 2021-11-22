namespace U2U.Games.Infra.Tests;

public class GamesRepositoryShould
  : IClassFixture<TestableGameSpecificationFactories>
{
  private readonly TestableGameSpecificationFactories gameSpecificationFactories;
  private readonly GameSpecificationFactory gameSpecificationFactory;
  private readonly PublisherSpecificationFactory publisherSpecificationFactory;

  public GamesRepositoryShould(TestableGameSpecificationFactories gameSpecificationFactories)
  {
    this.gameSpecificationFactories = gameSpecificationFactories;
    this.gameSpecificationFactory =
      gameSpecificationFactories.For<Game, GameSpecificationFactory>();
    this.publisherSpecificationFactory =
      gameSpecificationFactories.For<Publisher, PublisherSpecificationFactory>();
  }

  private DbInjectionFixture<GamesDb> InMemFixture()
    => new DbInjectionFixture<GamesDb>().ConfigureServices(services
      => services
      .AddEntityFrameworkInMemoryDatabase()
      .AddMaintenanceInspectors<Game>());

  private DbInjectionFixture<GamesDb> SqlFixture()
    => new DbInjectionFixture<GamesDb>().ConfigureServices(services
      => services
      .AddEntityFrameworkSqlServer()
      .AddMaintenanceInspectors<Game>());

  [Fact]
  public async Task ReturnAllGamesForSomePublisher()
  {
    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      dbContext.Database.EnsureCreated();

      IRepository<Publisher> pubRepo = new Repository<Publisher, GamesDb>(dbContext);
      Publisher? pub = new Publisher(0, "Irish Pub");
      await pubRepo.InsertAsync(pub);
      await pubRepo.SaveChangesAsync();
      int pubId = pub.Id;
      IRepository<Game> gameRepo = new Repository<Game, GamesDb>(dbContext);
      Game tictactoe = pub.CreateGame("TicTacToe");
      tictactoe.SetImage("someimageurl");
      await gameRepo.InsertAsync(tictactoe);
      await gameRepo.SaveChangesAsync();
      ISpecification<Game> spec = this.gameSpecificationFactory.WithPublisher(pub);
      IEnumerable<Game> games = await gameRepo.ListAsync(spec);
      games.Should().HaveCount(1);
      foreach (Game game in games)
      {
        game.Publisher.Should().NotBeNull();
      }
    }
  }

  [Fact]
  public async Task ReturnAllGamesForSomePublisherID()
  {
    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      dbContext.Database.EnsureCreated();
      GamesRepository? sut = new GamesRepository(dbContext);
      ISpecification<Game> spec = this.gameSpecificationFactory.GamesForPublisher(publisherID: 1);
      IEnumerable<Game> games = await sut.ListAsync(spec);
      games.Should().HaveCount(1);
      foreach (Game game in games)
      {
        game.Publisher.Should().BeNull();
      }
    }
  }

  [Fact]
  public async Task ReturnAllGamesForMaxAmount()
  {
    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      dbContext.Database.EnsureCreated();
      Money? maxAmount = new Money(30);
      ISpecification<Game> spec = this.gameSpecificationFactory.WithMaxAmount(maxAmount);
      IReadonlyRepository<Game> gameRepo = new Repository<Game, GamesDb>(dbContext);
      IEnumerable<Game> games = await gameRepo.ListAsync(spec);
      games.Should().HaveCount(2);
    }
  }

  [Fact]
  public async Task ReturnAllGamesForMaxAmountInUSD()
  {
    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      dbContext.Database.EnsureCreated();
      Money? maxAmount = new Money(15, Currencies.Core.Entities.CurrencyName.USD);
      ISpecification<Game> spec = this.gameSpecificationFactory.WithMaxAmount(maxAmount);
      IReadonlyRepository<Game> gameRepo = new Repository<Game, GamesDb>(dbContext);
      IEnumerable<Game> games = await gameRepo.ListAsync(spec);
      games.Should().HaveCount(2);
    }
  }


  [Fact]
  public async Task ReturnAllGamesForMinAmount()
  {
    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      dbContext.Database.EnsureCreated();
      Money? maxAmount = new Money(30);
      ISpecification<Game> spec = this.gameSpecificationFactory.WithMinAmount(maxAmount);
      IReadonlyRepository<Game> gameRepo = new Repository<Game, GamesDb>(dbContext);
      IEnumerable<Game> games = await gameRepo.ListAsync(spec);
      games.Should().HaveCount(1);
    }
  }

  [Fact]
  public async Task ReturnAllGamesForPriceRange()
  {
    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      dbContext.Database.EnsureCreated();
      Money? minAmount = new Money(28);
      Money? maxAmount = new Money(30);
      ISpecification<Game> spec = this.gameSpecificationFactory.InRangeInclusive(minAmount, maxAmount);
      IReadonlyRepository<Game> gameRepo = new Repository<Game, GamesDb>(dbContext);
      IEnumerable<Game> games = await gameRepo.ListAsync(spec);
      games.Should().HaveCount(2);
    }
  }

  [Fact]
  public async Task ReturnAllGamesForSomePublisherForSQL()
  {
    //DbInjectionFixture<GamesDb> fixture = new();
    //fixture.ConfigureServices(services
    //  => services
    //  .AddEntityFrameworkSqlServer()
    //  .AddMaintenanceInspectors<Game>());

    using (SqlFixture().StartTest())
    {
      int pubId;
      using (GamesDb dbContext = SqlFixture().CreateSqlServerDbContext("GamesDb"))
      {
        IRepository<Publisher> pubRepo = new Repository<Publisher, GamesDb>(dbContext);
        Publisher? pub = new Publisher(0, "Irish Pub");
        await pubRepo.InsertAsync(pub);
        await pubRepo.SaveChangesAsync();
        pubId = pub.Id;
      }
      using (GamesDb dbContext = SqlFixture().CreateSqlServerDbContext("GamesDb"))
      {
        IRepository<Publisher> pubRepo = new Repository<Publisher, GamesDb>(dbContext);
        ISpecification<Publisher> spec = this.publisherSpecificationFactory.WithId(pubId);
        Publisher? pub = await pubRepo.SingleAsync(spec);
        pub.Should().NotBeNull();
        IRepository<Game> gameRepo = new GamesRepository(dbContext);
        await gameRepo.InsertAsync(pub!.CreateGame("TicTacToe"));
        await gameRepo.SaveChangesAsync();
      }
      using (GamesDb dbContext = SqlFixture().CreateSqlServerDbContext("GamesDb"))
      {
        IRepository<Publisher> pubRepo = new Repository<Publisher, GamesDb>(dbContext);
        ISpecification<Publisher> spec = this.publisherSpecificationFactory.WithId(pubId);
        Publisher? pub = await pubRepo.SingleAsync(spec);
        pub.Should().NotBeNull();
        IRepository<Game> gameRepo = new GamesRepository(dbContext);
        ISpecification<Game> gameSpec = this.gameSpecificationFactory.WithPublisher(pub!);
        IEnumerable<Game> games = await gameRepo.ListAsync(gameSpec);
        games.Should().HaveCount(1);
      }
    }
  }

  [Fact]
  public async Task InsertingAGameShouldSetCreatedColumn()
  {
    //DbInjectionFixture<GamesDb> fixture = new();
    //fixture.ConfigureServices(services
    //  => services
    //  .AddEntityFrameworkInMemoryDatabase()
    //  .AddMaintenanceInspectors<Game>());

    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      // Arrange
      dbContext.Database.EnsureCreated();

      IRepository<Publisher> pubRepoForArrange = new Repository<Publisher, GamesDb>(dbContext);
      Publisher? pubForArrange = new Publisher(0, "Irish Pub");
      await pubRepoForArrange.InsertAsync(pubForArrange);
      await pubRepoForArrange.SaveChangesAsync();

      // Act
      IRepository<Publisher> pubRepoForAct = new Repository<Publisher, GamesDb>(dbContext);
      ISpecification<Publisher> pubSpec = this.publisherSpecificationFactory.WithId(pubForArrange.Id);

      Publisher? pubForAct = await pubRepoForAct.SingleAsync(pubSpec);
      pubForAct.Should().NotBeNull();
      IRepository<Game> gameRepoForAct = new GamesRepository(dbContext);
      Game newGame = pubForAct!.CreateGame("TicTacToe");
      await gameRepoForAct.InsertAsync(newGame);
      await gameRepoForAct.SaveChangesAsync();

      // Assert
      IRepository<Game> gameRepoForAssert = new GamesRepository(dbContext);
      ISpecification<Game> gameSpec = this.gameSpecificationFactory.WithId(newGame.Id);
      Game? addedGame = await gameRepoForAssert.SingleAsync(gameSpec);
      addedGame.Should().NotBeNull();
      DateTime createdTime = dbContext.Entry(addedGame!)!
        .Property<DateTime>(CreationInspector<Game>.UtcCreated).CurrentValue;
      DateTime lastModified = dbContext.Entry(addedGame!)!
        .Property<DateTime>(ModifiedInspector<Game>.UtcModified).CurrentValue;
      createdTime.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(1), because: "Inserting a game should set Created column to current time");
      lastModified.Should().BeSameDateAs(createdTime, because: "Inserting a game should set LastModified column to current time");
    }
  }

  [Fact]
  public async Task UpdatingAGameShouldUpdateLastUpdatedColumn()
  {
    using (GamesDb dbContext = InMemFixture().CreateInMemoryDbContext(Guid.NewGuid().ToString()))
    {
      // Arrange
      dbContext.Database.EnsureCreated();
      IRepository<Game> gameRepo = new GamesRepository(dbContext);
      ISpecification<Game> gameSpec = this.gameSpecificationFactory.WithId(1);
      Game? theGame = await gameRepo.SingleAsync(gameSpec);
      theGame.Should().NotBeNull();

      // Act
      theGame!.Rename("SomeOtherName");
      await gameRepo.SaveChangesAsync();

      // Assert
      IRepository<Game> gameRepoForAssert = new GamesRepository(dbContext);
      Game? updatedGame = await gameRepoForAssert.SingleAsync(gameSpec);
      updatedGame.Should().NotBeNull();

      DateTime originalCreatedTime = dbContext.Entry(theGame).Property<DateTime>(CreationInspector<Game>.UtcCreated).CurrentValue;
      DateTime createdTime = dbContext.Entry(updatedGame!)!
        .Property<DateTime>(CreationInspector<Game>.UtcCreated).CurrentValue;
      DateTime lastModified = dbContext.Entry(updatedGame!)!
        .Property<DateTime>(ModifiedInspector<Game>.UtcModified).CurrentValue;

      createdTime.Should().BeSameDateAs(originalCreatedTime, because: "Updating a game should not modify Created column");
      lastModified.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(0.1), because: "Updating a game should set LastModified to current time");
    }
  }

  [Fact]
  public async Task InsertGameInDbContextByCallingAdd()
  {
    Mock<DbSet<Game>>? mockSet = new Mock<DbSet<Game>>();
    Mock<GamesDb>? mockContext = new Mock<GamesDb>();
    mockContext
      .Setup(m => m.Set<Game>())
      .Returns(mockSet.Object);

    GamesRepository? repo = new GamesRepository(mockContext.Object);
    Publisher? pub = new Publisher(1, "Irish Pub");
    await repo.InsertAsync(pub.CreateGame("TicTacToe"));

    mockSet.Verify(m => m.Add(It.IsAny<Game>()), Times.Once());
  }
}
