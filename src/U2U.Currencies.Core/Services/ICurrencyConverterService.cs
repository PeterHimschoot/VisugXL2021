namespace U2U.Currencies.Core.Services;

public interface ICurrencyConverterService
{
  ValueTask<Money> ConvertAmountFromAsync(Money fromCurrency, CurrencyName toCurrency);
}

