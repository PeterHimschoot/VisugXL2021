namespace U2U.Currencies.Infra;

// Each library knows about its own classes which become dependencies for others.
// This way we can keep knowledge about this library away from outsiders
[AutoConfig]
public static class DependencyInjection
{
  [AutoConfig("CurrencyDb")]
  public static IServiceCollection AddCurrencyDb(this IServiceCollection services, string connectionString/*, [MigrationAssembly] string migrationAssembly*/)
    => services.AddDbContext<CurrencyDb>(optionsBuilder =>
        optionsBuilder.UseSqlServer(connectionString), ServiceLifetime.Singleton); //,


  // The currency repository is a singleton, optimizing things like caching
  // This does mean that no changes are allowed!
  [AutoConfig]
  public static IServiceCollection AddCurrencyRepository(this IServiceCollection services)
    => services.AddSingleton(
      typeof(IReadonlyRepository<Currency>),
      typeof(CachedRepository<Currency, CurrencyDb>)
      );
}
