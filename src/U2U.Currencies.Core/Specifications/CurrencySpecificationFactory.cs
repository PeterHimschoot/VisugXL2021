namespace U2U.Currencies.Core.Specifications;

// After a while you will get bored writing specifications like this,
// it is a lot easier to build specifications using the SpecifictionFactory<T>
// and to easy the number of arguments, you can use the SpecificationFactories
public class CurrencySpecificationFactory : SpecificationFactory<Currency>
{
  public ISpecification<Currency> WithName(CurrencyName name)
    => new CurrencyWithName(name);

  private class CurrencyWithName : Specification<Currency>
  {
    public CurrencyWithName(CurrencyName name)
      : base(c => c.Name == name) { }
  }
}

