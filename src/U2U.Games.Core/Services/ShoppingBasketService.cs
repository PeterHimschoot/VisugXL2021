namespace U2U.Games.Core.Services;

public class ShoppingBasketService : IShoppingBasketService
{
  private readonly IReadonlyRepository<Game> gameRepo;
  private readonly IRepository<ShoppingBasket> sbRepo;
  private readonly GameSpecificationFactories specificationFactories;

  public ShoppingBasketService(IReadonlyRepository<Game> gameRepo, IRepository<ShoppingBasket> sbRepo, GameSpecificationFactories specificationFactories)
  {
    this.gameRepo = gameRepo;
    this.sbRepo = sbRepo;
    this.specificationFactories = specificationFactories;
  }

  public async ValueTask<int> AddGameToShoppingBasketWithId(int shoppingBasketId, int gameId)
  {
    ISpecification<Game>? gameSpec = this.specificationFactories.For<Game>().WithId(gameId);
    Game? game = await this.gameRepo.SingleAsync(gameSpec);

    if (game == null)
    {
      throw new InvalidProgramException("Game not found");
    }

    ShoppingBasket sb;
    if (shoppingBasketId == 0)
    {
      sb = new ShoppingBasket(shoppingBasketId);
      await this.sbRepo.InsertAsync(sb);
    }
    else
    {
      sb = (await GetShoppingBasketAsync(shoppingBasketId))!;
    }
    sb.AddGame(game);
    await this.sbRepo.SaveChangesAsync();
    return sb.Id;
  }

  public async ValueTask<ShoppingBasket?> GetShoppingBasketAsync(int shoppingBasketId)
  {
    ISpecification<ShoppingBasket>? spec = this.specificationFactories.For<ShoppingBasket>().WithId(shoppingBasketId);
    ShoppingBasket? sb = await this.sbRepo.SingleAsync(spec);
    if (sb != null)
    {
      foreach (Game? game in sb.Games)
      {
        ISpecification<Game>? gameSpec = this.specificationFactories.For<Game>().WithId(game.Id);
        _ = await this.gameRepo.SingleAsync(gameSpec);
      }
    }

    return sb;
  }
}
