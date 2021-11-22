namespace U2U.Currencies.Infra;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
  public void Configure(EntityTypeBuilder<Currency> aCurrency)
  {
    aCurrency.HasKey(cur => cur.Id);
    aCurrency.HasIndex(cur => cur.Name)
      .IsUnique();

    // I don't want the enumeration value to be used in the database (0,1,2)
    // Instead we use a converter to go back and forth between readable names
    aCurrency.Property(cur => cur.Name)
           .HasConversion(cur => $"{cur}", s => Currency.Parse(s))
           .HasMaxLength(3);

    aCurrency.Property(cur => cur.ValueInEuro)
      .HasColumnType("decimal(18,2)");
  }
}
