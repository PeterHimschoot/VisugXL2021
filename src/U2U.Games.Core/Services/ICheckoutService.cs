namespace U2U.Games.Core.Services;

public interface ICheckoutService
{
  ValueTask CheckoutBasket(ShoppingBasket basket);
  ValueTask<ShoppingBasket?> GetShoppingBasketAsync(int shoppingBasketId);
}
