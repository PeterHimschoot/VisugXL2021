namespace U2U.Games.Infra;

internal class GameInBasketConfiguration : IEntityTypeConfiguration<GameInBasket>
{
  public void Configure(EntityTypeBuilder<GameInBasket> builder)
  {
    builder
      .HasKey(gib => new { gib.ShoppingBasketId, gib.GameId });

    builder
      .HasOne(gib => gib.ShoppingBasket)
      .WithMany(sb => sb.GamesInBasket)
      .HasForeignKey(gib => gib.ShoppingBasketId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);

    builder
      .HasOne(gib => gib.Game)
      .WithMany()
      .HasForeignKey(gib => gib.GameId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);
  }
}

