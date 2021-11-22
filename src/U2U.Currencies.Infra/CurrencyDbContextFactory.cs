namespace U2U.Currencies.Infra;

public class CurrencyDbContextFactory : IDesignTimeDbContextFactory<CurrencyDb>
{
  public CurrencyDb CreateDbContext(string[] args)
  {
    // This project should use the same user secrets key as Clean.Architecture.Web.csproj !!!
    ConfigurationBuilder? cb = new ConfigurationBuilder();
    cb.AddUserSecrets<CurrencyDbContextFactory>();

    IConfigurationRoot configuration = cb.Build();
    string connectionString = configuration.GetConnectionString("CurrencyDb");

    DbContextOptionsBuilder<CurrencyDb>? builder = new DbContextOptionsBuilder<CurrencyDb>();
    builder.UseSqlServer(connectionString);
    return new CurrencyDb(builder.Options);
  }
}

