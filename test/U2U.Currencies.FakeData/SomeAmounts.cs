namespace U2U.Currencies.FakeData;

public class SomeAmounts : List<object[]>
{
  public SomeAmounts()
  {
    Add(new Money(100, CurrencyName.EUR));
    Add(new Money(10, CurrencyName.USD));
    Add(new Money(20, CurrencyName.JPY));
  }

  public void Add(Money m)
    => base.Add(new object[] { m });
}

