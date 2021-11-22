namespace U2U.Currencies.Core.Tests;

public class CultureToCurrencyServiceShould
{
  private ICultureToCurrencyService CreateSut() => new CultureToCurrencyService();

  [Theory]
  [InlineData("nl-BE", CurrencyName.EUR)]
  [InlineData("fr-BE", CurrencyName.EUR)]
  [InlineData("en-US", CurrencyName.USD)]
  [InlineData("ja-JP", CurrencyName.JPY)]
  public async Task ReturnProperCurrencyNameForCulture(string cultureName, CurrencyName expected)
  {
    ICultureToCurrencyService? sut = CreateSut();
    CurrencyName result = await sut.CurrencyForCultureAsync(new CultureInfo(cultureName));
    Assert.Equal(expected, result);
  }

  [Theory]
  [ClassData(typeof(SomeCultures))]
  public async Task SupportTheSupportedCultures(string cultureName)
  {
    ICultureToCurrencyService? sut = CreateSut();
    CurrencyName result = await sut.CurrencyForCultureAsync(new CultureInfo(cultureName));
    Assert.IsType<CurrencyName>(result); // will always automatically succeed if everything worked out fine!
  }
}

