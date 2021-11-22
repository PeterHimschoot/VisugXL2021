namespace U2U.Currencies.Infra.Tests;

public class CurrencyRepositoryShould
    : IClassFixture<DbInjectionFixture<CurrencyDb>>
    , IClassFixture<CurrencySpecificationFactory>
{
  private readonly DbInjectionFixture<CurrencyDb> fixture;
  private readonly CurrencySpecificationFactory specificationFactory;

  public CurrencyRepositoryShould(DbInjectionFixture<CurrencyDb> fixture, CurrencySpecificationFactory specificationFactory)
  {
    this.fixture = fixture;
    this.specificationFactory = specificationFactory;
  }

  [Fact]
  public async ValueTask RetrieveIdentityForEuro()
  {
    CurrencyDb? dbContext = this.fixture.CreateInMemoryDbContext("SimpleCurrencyDb");
    dbContext.Database.EnsureCreated();
    ReadonlyRepository<Currency, CurrencyDb>? repo = new ReadonlyRepository<Currency, CurrencyDb>(dbContext);
    Currency? result = await repo.SingleAsync(this.specificationFactory.WithName(CurrencyName.EUR));
    result.Should().NotBeNull();
    Assert.Equal(1, result!.ValueInEuro);
  }

  [Theory]
  [ClassData(typeof(SomeCurrencyNames))]
  public async ValueTask SupportAtLeastTheseCurrencies(CurrencyName name)
  {
    CurrencyDb? dbContext = this.fixture.CreateInMemoryDbContext("SimpleCurrencyDb");
    dbContext.Database.EnsureCreated();
    ReadonlyRepository<Currency, CurrencyDb>? repo = new ReadonlyRepository<Currency, CurrencyDb>(dbContext);
    Currency? result = await repo.SingleAsync(this.specificationFactory.WithName(name));
    // Getting here means success!
  }
}
