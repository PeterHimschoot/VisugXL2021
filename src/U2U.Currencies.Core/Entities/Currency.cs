namespace U2U.Currencies.Core.Entities;

/// <summary>
/// Currency with its current exchange rate in EUR
/// </summary>
[DebuggerDisplay("{Name,nq} = {ValueInEuro}EUR")]
public class Currency : EntityBase, IAggregateRoot
{
  public Currency(int id, CurrencyName name, decimal valueInEuro) : base(id)
  {
    Name = name;
    ValueInEuro = valueInEuro;
  }

  public CurrencyName Name { get; }

  public decimal ValueInEuro { get; }

  public static CurrencyName Parse(string currencyAsString)
    => CurrencyName.Parse<CurrencyName>(currencyAsString);
}
