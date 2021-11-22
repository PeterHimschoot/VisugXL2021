namespace U2U.Games.Core.Specifications;

public class GameSpecificationFactories : SpecificationFactories
{
  // Why use dependency injection to get the factory instances?
  // This way the factories can request their own dependencies
  // Dependency injection all the way! :)
  public GameSpecificationFactories(GameSpecificationFactory gameSpecificationFactory,
    PublisherSpecificationFactory publisherSpecificationFactory,
    ShoppingBasketSpecificationFactory shoppingBasketSpecificationFactory)
  {
    this.Add<Game>(gameSpecificationFactory);
    this.Add<Publisher>(publisherSpecificationFactory);
    this.Add<ShoppingBasket>(shoppingBasketSpecificationFactory);
  }
}

