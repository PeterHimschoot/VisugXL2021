namespace U2U.Currencies.FakeData;

public class SomeCurrencyNames : List<object[]>
{
  public SomeCurrencyNames()
  {
    Add(CurrencyName.USD);
    Add(CurrencyName.EUR);
    Add(CurrencyName.JPY);
  }

  private void Add(CurrencyName name)
    => Add(new object[] { name });
}
