namespace U2U.Games.Core.DomainEvents;

public class ShoppingBasketHasCheckedOutBillingHandler : IDomainEventHandler<ShoppingBasketHasCheckedOut>
{
  public ValueTask Handle(object @event) =>
    // .. logic for billing the shoppingbasket to the customer
    new ValueTask();

}

