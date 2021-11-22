namespace U2U.Games.Core.Services;

public interface IShoppingBasketService
{
  /// <summary>
  /// Upsert shoppingbasket with new game
  /// </summary>
  /// <param name="shoppingBasketId">id of shopping basket, 0 if new</param>
  /// <param name="game">game</param>
  /// <returns>shoppingbasket id</returns>
  ValueTask<int> AddGameToShoppingBasketWithId(int shoppingBasketId, int gameId);

  ValueTask<ShoppingBasket?> GetShoppingBasketAsync(int shoppingBasketId);
}
