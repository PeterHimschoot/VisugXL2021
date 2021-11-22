namespace U2U.Games.Infra;

internal class ShoppingBasketConfiguration : IEntityTypeConfiguration<ShoppingBasket>
{
  public void Configure(EntityTypeBuilder<ShoppingBasket> builder)
  {
    builder.HasKey(sb => sb.Id);
    builder.Ignore(sb => sb.Games);

    Microsoft.EntityFrameworkCore.Metadata.IMutableNavigation? gamesInBasketNav =
      builder.Metadata.FindNavigation(nameof(ShoppingBasket.GamesInBasket));
    gamesInBasketNav!.SetPropertyAccessMode(PropertyAccessMode.Field);
    gamesInBasketNav!.SetField("gamesInBasket");
  }
}

