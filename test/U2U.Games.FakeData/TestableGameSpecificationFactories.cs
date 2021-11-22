namespace U2U.Games.FakeData;

public class TestableGameSpecificationFactories : GameSpecificationFactories
{
  public TestableGameSpecificationFactories()
    : base(new GameSpecificationFactory(FakeCurrencyConverterService.Instance),
        new PublisherSpecificationFactory(),
        new ShoppingBasketSpecificationFactory())
  { }
}

