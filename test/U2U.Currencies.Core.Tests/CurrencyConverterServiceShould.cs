namespace U2U.Currencies.Core.Tests;

public class CurrencyConverterServiceShould : IClassFixture<HardCodedCurrenciesFixture>
{
  private readonly HardCodedCurrenciesFixture currenciesFixture;

  public CurrencyConverterServiceShould(HardCodedCurrenciesFixture currenciesFixture)
    => this.currenciesFixture = currenciesFixture ?? throw new ArgumentNullException(nameof(currenciesFixture));

  private CurrencyConverterService CreateConverterService(IReadonlyRepository<Currency> repo)
    => new CurrencyConverterService(repo, new Specifications.CurrencySpecificationFactory());

  [Theory()]
  [ClassData(typeof(SomeAmounts))]
  public async Task ConvertAmountToSameCurrencyNameReturnsSameAmount(Money m)
  {
    IReadonlyRepository<Currency> fakeRepo = this.currenciesFixture.FakeRepo();
    CurrencyConverterService sut = CreateConverterService(fakeRepo);
    Money result = await sut.ConvertAmountFromAsync(m, m.Currency);
    Assert.Equal<Money>(m, result);
  }

  [Theory()]
  [ClassData(typeof(SomeAmounts))]
  public async Task ConvertAmountToOtherCurrencyNameAndBackReturnsSameAmount(Money m)
  {
    IReadonlyRepository<Currency> fakeRepo =
      this.currenciesFixture.FakeRepoWithData(this.currenciesFixture.Doubles);
    CurrencyConverterService sut = CreateConverterService(fakeRepo);
    foreach (CurrencyName CurrencyName in Enum.GetValues(typeof(CurrencyName)))
    {
      Money converted = await sut.ConvertAmountFromAsync(m, CurrencyName);
      Money backAgain = await sut.ConvertAmountFromAsync(converted, m.Currency);
      Assert.Equal<Money>(m, backAgain);
    }
  }

  [Theory()]
  [ClassData(typeof(SomeAmounts))]
  public async Task ConvertAmountToCurrencyNameShouldReturnThatCurrencyName(Money m)
  {
    IReadonlyRepository<Currency> fakeRepo = this.currenciesFixture.FakeRepoWithData(this.currenciesFixture.Doubles);
    CurrencyConverterService sut = CreateConverterService(fakeRepo);
    foreach (CurrencyName CurrencyName in Enum.GetValues(typeof(CurrencyName)))
    {
      Money result = await sut.ConvertAmountFromAsync(m, CurrencyName);
      Assert.Equal(result.Currency, CurrencyName);
    }
  }

  [Theory()]
  [ClassData(typeof(SomeEuroAmounts))]
  public async Task ConvertEuroToOtherCurrencyShouldReturnHalfTheAmount(Money euros)
  {
    IReadonlyRepository<Currency> fakeRepo = this.currenciesFixture.FakeRepoWithData(this.currenciesFixture.Doubles);
    CurrencyConverterService sut = CreateConverterService(fakeRepo);
    Money result = await sut.ConvertAmountFromAsync(euros, CurrencyName.USD);
    Assert.Equal(CurrencyName.USD, result.Currency);
    result.Amount.Should().Be(euros.Amount / 2);
  }

  [Theory()]
  [ClassData(typeof(SomeOtherAmounts))]
  public async Task ConvertOtherCurrencyToEuroShouldReturnDoubleAmount(Money otherCurrency)
  {
    IReadonlyRepository<Currency> fakeRepo = this.currenciesFixture.FakeRepoWithData(this.currenciesFixture.Doubles);
    CurrencyConverterService sut = CreateConverterService(fakeRepo);
    Money result = await sut.ConvertAmountFromAsync(otherCurrency, CurrencyName.EUR);
    Assert.Equal(CurrencyName.EUR, result.Currency);
    result.Amount.Should().Be(otherCurrency.Amount * 2);
  }
}

