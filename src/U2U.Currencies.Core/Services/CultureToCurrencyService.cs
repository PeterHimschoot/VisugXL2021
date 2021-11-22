namespace U2U.Currencies.Core.Services;

internal class CultureToCurrencyService : ICultureToCurrencyService
{
  public ValueTask<CurrencyName> CurrencyForCultureAsync(CultureInfo cultureInfo)
  {
    RegionInfo? region = new RegionInfo(cultureInfo.Name);
    string currencyNameSymbol = region.ISOCurrencySymbol;
    CurrencyName name = Currency.Parse(currencyNameSymbol);
    return ValueTask.FromResult(name);
  }
}
