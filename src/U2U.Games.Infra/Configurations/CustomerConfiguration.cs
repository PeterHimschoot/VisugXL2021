namespace U2U.Games.Infra;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.HasKey(c => c.Id);

    builder.Property(c => c.FirstName)
      .HasMaxLength(100)
      .IsRequired();

    builder.Property(c => c.LastName)
      .HasMaxLength(100)
      .IsRequired();

    OwnedNavigationBuilder<Customer, Core.ValueObjects.Address>? addressNavBuilder = builder.OwnsOne(c => c.Address);
    addressNavBuilder.Property(a => a.Street)
      .HasMaxLength(100);
    addressNavBuilder.Property(a => a.City)
      .HasMaxLength(100);

    builder.HasOne(c => c.ShoppingBasket)
           .WithOne(sb => sb.Customer)
           .HasForeignKey<ShoppingBasket>(sb => sb.CustomerId).IsRequired(false);
  }
}

