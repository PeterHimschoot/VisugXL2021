namespace U2U.Games.Infra;

internal class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
  public void Configure(EntityTypeBuilder<Publisher> builder)
  {
    builder.HasKey(pub => pub.Id);
    builder.HasAlternateKey(p => p.Name);

    builder.HasMany(p => p.Games)
      .WithOne(g => g.Publisher)
      .IsRequired();

    // Use the games field instead of the property, because the property has side-effects
    Microsoft.EntityFrameworkCore.Metadata.IMutableNavigation? gamesNav = builder.Metadata
                          .FindNavigation(nameof(Publisher.Games));
    gamesNav!.SetPropertyAccessMode(PropertyAccessMode.Field);
  }
}

