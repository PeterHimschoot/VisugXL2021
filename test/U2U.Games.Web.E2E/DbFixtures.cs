namespace U2U.Games.Web.E2E;

public class DbFixtures
{
  public static DbInjectionFixture<GamesDb> InMemGamesDbFixture()
    => new DbInjectionFixture<GamesDb>().ConfigureServices(services
      => services.AddEntityFrameworkInMemoryDatabase()
                 .AddMaintenanceInspectors<Game>());

  public static DbInjectionFixture<CurrencyDb> InMemCurrencyDbFixture()
    => new DbInjectionFixture<CurrencyDb>().ConfigureServices(services
      => services.AddEntityFrameworkInMemoryDatabase());
}
