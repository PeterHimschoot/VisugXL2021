namespace U2U.Games.Core.Services;

public class CheckoutService : ICheckoutService
{
  private readonly IRepository<ShoppingBasket> shoppingBasketRepo;
  private readonly GameSpecificationFactories specificationFactories;

  public CheckoutService(IRepository<ShoppingBasket> shoppingBasketRepo, GameSpecificationFactories specificationFactories)
  {
    this.shoppingBasketRepo = shoppingBasketRepo;
    this.specificationFactories = specificationFactories;
  }

  public async ValueTask<ShoppingBasket?> GetShoppingBasketAsync(int shoppingBasketId)
  {
    ISpecification<ShoppingBasket>? spec = this.specificationFactories.For<ShoppingBasket>().WithId(shoppingBasketId);
    ShoppingBasket? sb = await this.shoppingBasketRepo.SingleAsync(spec);
    return sb;
  }

  public async ValueTask CheckoutBasket(ShoppingBasket basket)
  {
    basket.CheckOut();
    await shoppingBasketRepo.UpdateAsync(basket);
    await shoppingBasketRepo.SaveChangesAsync();
  }
}