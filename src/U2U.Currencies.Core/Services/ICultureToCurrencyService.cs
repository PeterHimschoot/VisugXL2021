namespace U2U.Currencies.Core.Services;

public interface ICultureToCurrencyService
{
  ValueTask<CurrencyName> CurrencyForCultureAsync(CultureInfo cultureInfo);
}
