namespace U2U.Currencies.Core.Services;

public class CurrencyConverterService : ICurrencyConverterService
{
  private readonly IReadonlyRepository<Currency> repo;
  private readonly CurrencySpecificationFactory specFactory;

  public CurrencyConverterService(IReadonlyRepository<Currency> repo, CurrencySpecificationFactory specFactory)
  {
    this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
    this.specFactory = specFactory;
  }

  private async Task<decimal> ValueForCurrency(CurrencyName currencyName)
      => (await this.repo.SingleAsync(
            specFactory.WithName(currencyName).AsCached(TimeSpan.FromDays(1), currencyName))
        )!.ValueInEuro;

  public async ValueTask<Money> ConvertAmountFromAsync(Money from, CurrencyName toCurrency)
  {
    if (from.Currency == toCurrency)
    {
      // Amount is already in expected currency
      return from;
    }
    decimal fromValueInEur = await ValueForCurrency(from.Currency);
    decimal toValueInEur = await ValueForCurrency(toCurrency);
    decimal fromInEur = from.Amount * fromValueInEur;
    decimal fromInCurrency = fromInEur / toValueInEur;
    Money? m = new Money(fromInCurrency, toCurrency);
    return m;
  }
}

