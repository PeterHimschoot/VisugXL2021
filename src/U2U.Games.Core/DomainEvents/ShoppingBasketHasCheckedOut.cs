namespace U2U.Games.Core.DomainEvents;

public class ShoppingBasketHasCheckedOut : IDomainEvent<ShoppingBasket>
{
  public ShoppingBasketHasCheckedOut(ShoppingBasket shoppingBasket) => ShoppingBasket = shoppingBasket;

  public ShoppingBasket ShoppingBasket { get; }
}
