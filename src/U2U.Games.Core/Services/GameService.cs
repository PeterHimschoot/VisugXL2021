namespace U2U.Games.Core.Services;

public class GameService : IGameService
{
  private readonly IRepository<Game> gameRepo;
  private readonly IReadonlyRepository<Publisher> pubRepo;
  private readonly GamePriceService gamePriceService;
  private readonly GameSpecificationFactories specificationFactories;

  public GameService(IRepository<Game> gameRepo
    , IReadonlyRepository<Publisher> pubRepo
    , GamePriceService gamePriceService
    , GameSpecificationFactories specificationFactories)
  {
    this.gameRepo = gameRepo;
    this.pubRepo = pubRepo;
    this.gamePriceService = gamePriceService;
    this.specificationFactories = specificationFactories;
  }

  public async ValueTask ChangeGamesPublisherAsync(int gameId, int publisherId)
  {
    ISpecification<Game> gameSpec = this.specificationFactories.For<Game>().WithId(gameId)
                                           .Include(game => game.Publisher);

    Game? g = await this.gameRepo.SingleAsync(gameSpec);
    if (g != null)
    {
      ISpecification<Publisher>? spec =
        this.specificationFactories.For<Publisher>().WithId(publisherId);
      Publisher? pub = await this.pubRepo.SingleAsync(spec);
      g.ChangePublisher(pub!);
    }
  }

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0401:Possible allocation of reference type enumerator", Justification = "<Pending>")]
  public async IAsyncEnumerable<(Game, Money)> GamesAsync()
  {
    ISpecification<Game> spec =
      this.specificationFactories.For<Game>().All().Include(game => game.Publisher);
    IEnumerable<Game> games =
      await this.gameRepo.ListAsync(spec);
    await foreach ((Game game, Money amount) in this.gamePriceService.ConvertToLocalizedAndRoundedPrices(games))
    {
      yield return (game, amount);
    }
  }

  public async ValueTask<Game?> GameWithIdAsync(int gameId)
    => await this.gameRepo.SingleAsync(this.specificationFactories.For<Game>().WithId(gameId));

  public async ValueTask<IEnumerable<Publisher>> PublishersAsync()
    => await this.pubRepo.ListAsync(this.specificationFactories.For<Publisher>().All());

  public async ValueTask<Publisher?> PublisherWithIdAsync(int id)
    => await this.pubRepo.SingleAsync(this.specificationFactories.For<Publisher>().WithId(id));

  public async ValueTask InsertGameAsync(Game game)
  {
    await this.gameRepo.InsertAsync(game);
    await this.gameRepo.SaveChangesAsync();
  }
}
