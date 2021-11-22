namespace U2U.Currencies.FakeData;

public class SomeEuroAmounts : List<object[]>
{
  public SomeEuroAmounts()
  {
    Add(new Money(100, CurrencyName.EUR));
    Add(new Money(10, CurrencyName.EUR));
    Add(new Money(20, CurrencyName.EUR));
  }

  public void Add(Money m)
    => base.Add(new object[] { m });
}

