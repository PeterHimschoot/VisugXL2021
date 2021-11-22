namespace U2U.Games.Infra;

public class GamesDb : DbContext
{
  public GamesDb() : base() { }

  public GamesDb(DbContextOptions<GamesDb> options)
      : base(options) { }

  // Make these virtual to allow MOQ
  public virtual DbSet<Game> Games { get; set; } = default!;
  public virtual DbSet<GameImage> GameImages { get; set; } = default!;
  public virtual DbSet<Publisher> Publishers { get; set; } = default!;
  public virtual DbSet<Customer> Customers { get; set; } = default!;
  public virtual DbSet<ShoppingBasket> ShoppingBaskets { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfiguration(new GameImageConfiguration());
    modelBuilder.ApplyConfiguration(new PublisherConfiguration());
    modelBuilder.ApplyConfiguration(new GameConfiguration());
    modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    modelBuilder.ApplyConfiguration(new ShoppingBasketConfiguration());
    modelBuilder.ApplyConfiguration(new GameInBasketConfiguration());

    modelBuilder.HasData(new GameData());
    modelBuilder.HasData(new GameImageData());
    modelBuilder.HasData(new PublisherData());
  }

}