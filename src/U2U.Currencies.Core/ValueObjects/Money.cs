namespace U2U.Currencies.Core.ValueObjects;

[DebuggerDisplay("Money: {Amount} {Currency}")]
public class Money : IEquatable<Money>
{
  public static Money Zero { get; } = new Money(0, CurrencyName.EUR);

  public static Money Eur(decimal amount) => new Money(amount, CurrencyName.EUR);

  public decimal Amount { get; }

  public CurrencyName Currency { get; }

  public Money(decimal amount, CurrencyName currency)
  {
    Amount = amount;
    Currency = currency;
  }

  public Money(decimal amount) : this(amount, CurrencyName.EUR) { }

  public Money(decimal amount, string currency)
  : this(amount, (CurrencyName)Enum.Parse(typeof(CurrencyName), currency)) { }

  public static bool operator ==(Money left, Money right)
    => ValueObjectComparers.ValueObjectComparer<Money>.Instance.Equals(left, right);

  public static bool operator !=(Money left, Money right)
    => !(left == right);

  public override bool Equals(object obj)
    => ValueObjectComparers.ValueObjectComparer<Money>.Instance.Equals(this, obj);

  public bool Equals(Money other)
    => ValueObjectComparers.ValueObjectComparer<Money>.Instance.Equals(this, other);

  public override int GetHashCode()
    => ValueObjectComparers.ValueObjectComparer<Money>.Instance.GetHashCode(this);

  [Ignore]
  public Money Rounded
  {
    get
    {
      decimal amount = Amount * 100 + 49;
      int rounded = (int)amount;
      rounded = rounded - (rounded % 50);
      amount = (decimal)(rounded - 1) / 100;
      return new Money(amount, Currency);
    }
  }

  public override string ToString()
  => string.Format(FormatStringFor(Currency), Amount.ToString());

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0601:Value type to reference type conversion causing boxing allocation", Justification = "<Pending>")]
  private string FormatStringFor(CurrencyName currency)
  {
    switch (currency)
    {
      case CurrencyName.USD:
        return "${0}";
      case CurrencyName.EUR:
        return "{0}€";
      case CurrencyName.JPY:
        return "{0}¥";
      default:
        return $"{{0}} {currency}";
    }
  }

  // Simplified version that only adds two amounts with the same currency
  public static Money operator +(Money m1, Money m2)
  {
    Debug.Assert(m1.Currency == m2.Currency);
    return new Money(m1.Amount + m2.Amount, m1.Currency);
  }

  public static Money operator -(Money m1, Money m2)
  {
    Debug.Assert(m1.Currency == m2.Currency);
    return new Money(m1.Amount - m2.Amount, m1.Currency);
  }
}

