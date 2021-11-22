namespace U2U.Currencies.FakeData;

public class SomeOtherAmounts : List<object[]>
{
  public SomeOtherAmounts()
  {
    Add(new Money(100, CurrencyName.USD));
    Add(new Money(10, CurrencyName.USD));
    Add(new Money(20, CurrencyName.JPY));
  }

  public void Add(Money m)
    => base.Add(new object[] { m });
}

