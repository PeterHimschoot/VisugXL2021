namespace U2U.Games.Infra;

// This class is needed to perform migrations in .NET Core
// We want the migrations to be part of this library, including the Migrations folder
public class GamesDbContextFactory : IDesignTimeDbContextFactory<GamesDb>
{
  public GamesDb CreateDbContext(string[] args)
  {
    // This project should use the same user secrets key as Clean.Architecture.Web.csproj !!!
    ConfigurationBuilder? cb = new ConfigurationBuilder();
    cb.AddUserSecrets<GamesDbContextFactory>();

    IConfigurationRoot configuration = cb.Build();
    string connectionString = configuration.GetConnectionString("GamesDb");

    DbContextOptionsBuilder<GamesDb>? builder = new DbContextOptionsBuilder<GamesDb>();
    builder.UseSqlServer(connectionString);
    builder.EnableSensitiveDataLogging();
    return new GamesDb(builder.Options);
  }
}

