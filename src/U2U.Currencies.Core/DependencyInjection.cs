[assembly: InternalsVisibleTo("U2U.Currencies.Core.Tests")]

namespace U2U.Currencies.Core;

[AutoConfig]
public static class DependencyInjection
{

  [AutoConfig]
  public static IServiceCollection AddCurrencyServices(this IServiceCollection services)
    => services.AddSingleton<ICurrencyConverterService, CurrencyConverterService>()
               .AddSingleton<CurrencySpecificationFactory>()
               .AddSingleton<ICultureToCurrencyService, CultureToCurrencyService>()
               .AddTransient<CurrencyFacade>();
}
