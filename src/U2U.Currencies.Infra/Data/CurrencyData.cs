namespace U2U.Currencies.Infra.Data;

public class CurrencyData : IEntityTypeData<Currency>
{
  public void HasData(EntityTypeBuilder<Currency> entity)
  {
    entity.HasData(
      new Currency(1, CurrencyName.EUR, 1.0M),
      new Currency(2, CurrencyName.USD, 0.9M),
      new Currency(3, CurrencyName.JPY, 1.5M)
    );
  }
}
