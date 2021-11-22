using U2U.EntityFrameworkCore.Abstractions;

[assembly: InternalsVisibleTo("U2U.Games.FakeData")]
[assembly: InternalsVisibleTo("U2U.Games.Infra.Tests")]

namespace U2U.Games.Infra
{
  // Each library knows about its own classes which become dependencies for others.
  // This way we can keep knowledge about this library away from outsiders
  [AutoConfig]
  public static class DependencyInjection
  {
    [AutoConfig("GamesDb")]
    public static IServiceCollection AddGamesDb(this IServiceCollection services, string connectionString)
      => services.AddDbContext<GamesDb>((serviceProvider, optionsBuilder) =>
           optionsBuilder.UseSqlServer(connectionString)
                         .UseInternalServiceProvider(serviceProvider));

    [AutoConfig]
    public static IServiceCollection AddGameRepository(this IServiceCollection services)
      => services.AddScoped<GamesRepository>()
                 .AddScoped<IReadonlyRepository<Game>>(sp => sp.GetRequiredService<GamesRepository>())
                 .AddScoped<IRepository<Game>>(sp => sp.GetRequiredService<GamesRepository>());

    [AutoConfig]
    public static IServiceCollection AddPublisherRepository(this IServiceCollection services)
      => services.AddScoped<IReadonlyRepository<Publisher>, ReadonlyRepository<Publisher, GamesDb>>();

    [AutoConfig]
    public static IServiceCollection AddShoppingBasketRepository(this IServiceCollection services)
      => services.AddScoped<IRepository<ShoppingBasket>, ShoppingBasketRepository>();

    [AutoConfig]
    public static IServiceCollection AddMaintenance(this IServiceCollection services)
      => services.AddMaintenanceInspectors<Game>();

  }
}
