namespace U2U.Games.Core.Entities;

public class GameInBasket
{
  public ShoppingBasket ShoppingBasket { get; set; } = default!;

  public int ShoppingBasketId { get; set; }

  public Game Game { get; set; } = default!;

  public int GameId { get; set; }
}
