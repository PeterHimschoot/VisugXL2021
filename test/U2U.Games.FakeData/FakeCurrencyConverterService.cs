namespace U2U.Games.FakeData;

public static class FakeCurrencyConverterService
{
  public static ICurrencyConverterService Instance;

  static FakeCurrencyConverterService()
  {
    Mock<ICurrencyConverterService>? conversionMock = new Mock<ICurrencyConverterService>();
    conversionMock.Setup(m => m.ConvertAmountFromAsync(It.IsAny<Money>(), It.IsAny<CurrencyName>()))
      .ReturnsAsync((Money from, CurrencyName currency) =>
      {
        if (from.Currency == CurrencyName.EUR)
        {
          return from;
        }
        else
        {
          return new Money(from.Amount * 2.0M);
        }
      });
    Instance = conversionMock.Object;
  }
}
