namespace U2U.Games.Core.DomainEvents;

public class ShoppingBasketHasCheckedOutShippingHandler : IDomainEventHandler<ShoppingBasketHasCheckedOut>
{
  public ValueTask Handle(object @event) =>
    // .. logic for shopping the shoppingbasket to the customer
    new ValueTask();
}
