namespace U2U.Currencies.Core.Services;

public class CurrencyFacade
{
  private readonly ICurrencyConverterService converterService;
  private readonly ICultureToCurrencyService cultureService;

  public CurrencyFacade(ICurrencyConverterService converterService
    , ICultureToCurrencyService cultureService)
  {
    this.converterService = converterService;
    this.cultureService = cultureService;
  }

  public async ValueTask<Money> ConvertToRoundedAmountFromAsync(Money fromCurrency)
  {
    CurrencyName localizedCurrency = await this.cultureService.CurrencyForCultureAsync(CultureInfo.CurrentUICulture);
    Money localizedPrice = await this.converterService.ConvertAmountFromAsync(fromCurrency, localizedCurrency);
    return localizedPrice.Rounded;
  }
}

