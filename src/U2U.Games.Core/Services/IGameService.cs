namespace U2U.Games.Core.Services;

public interface IGameService
{
  ValueTask ChangeGamesPublisherAsync(int gameId, int publisherId);

  IAsyncEnumerable<(Game game, Money localizedPrice)> GamesAsync();

  ValueTask<IEnumerable<Publisher>> PublishersAsync();

  ValueTask<Publisher?> PublisherWithIdAsync(int id);

  ValueTask<Game?> GameWithIdAsync(int id);

  ValueTask InsertGameAsync(Game game);
}
