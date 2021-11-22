namespace U2U.Games.Core;

// Each library knows about its own classes which become dependencies for others.
// This way we can keep knowledge about this library away from outsiders
[AutoConfig]
public static class DependencyInjection
{
  [AutoConfig]
  public static IServiceCollection AddGameService(this IServiceCollection services)
    => services
    .AddScoped<IGameService, GameService>()
    .AddSingleton<GameSpecificationFactory>()
    .AddSingleton<PublisherSpecificationFactory>()
    .AddSingleton<GameSpecificationFactories>()
    .AddTransient<GamePriceService>();

  [AutoConfig]
  public static IServiceCollection AddShoppingBasket(this IServiceCollection services)
  => services
    .AddScoped<IShoppingBasketService, ShoppingBasketService>()
    .AddScoped<ICheckoutService, CheckoutService>()
    .AddSingleton<ShoppingBasketSpecificationFactory>();

  [AutoConfig]
  public static IServiceCollection AddEventHandlers(this IServiceCollection services)
  {
    services.AddScoped<IDomainEventHandler<GamePriceHasChanged>, GamePriceHasChangedHandler>();
    services.AddScoped<IDomainEventHandler<ShoppingBasketHasCheckedOut>, ShoppingBasketHasCheckedOutShippingHandler>();
    services.AddScoped<IDomainEventHandler<ShoppingBasketHasCheckedOut>, ShoppingBasketHasCheckedOutBillingHandler>();
    return services;
  }
}
