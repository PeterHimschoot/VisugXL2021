namespace U2U.Games.Core.Services;

public class GamePriceService
{
  private readonly CurrencyFacade facade;

  public GamePriceService(CurrencyFacade facade) => this.facade = facade;

  public async IAsyncEnumerable<(Game, Money)> ConvertToLocalizedAndRoundedPrices(IEnumerable<Game> games)
  {
    foreach (Game game in games)
    {
      Money localizedPrice = await this.facade.ConvertToRoundedAmountFromAsync(game.Price);
      localizedPrice = localizedPrice.Rounded;
      yield return (game, localizedPrice);
    };
  }
}

