namespace U2U.Games.Core.DomainEvents;

public class GamePriceHasChanged : IDomainEvent<Game>
{
  public GamePriceHasChanged(Game game)
  {
    Game = game;
  }

  public Game Game { get; }
}

