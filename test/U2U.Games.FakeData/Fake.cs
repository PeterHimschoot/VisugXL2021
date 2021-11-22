namespace U2U.Games.FakeData;

public static class Fake
{
  static Fake()
  {
    _999Games = new Publisher(1, "999 Games");
    Goliath = new Publisher(2, "Goliath");
    DaysOfWonder = new Publisher(3, "Days of Wonder");

    Quirkle = _999Games.CreateGame("Quirkle", 1);
    Quirkle.SetPrice(new Money(10));

    Rummikub = Goliath.CreateGame("Rummikub", 2);
    Rummikub.SetPrice(new Money(11));

    Ticket_To_Ride = DaysOfWonder.CreateGame("Ticket_To_Ride", 3);
    Ticket_To_Ride.SetPrice(new Money(12));

    Games = new List<Game> { Quirkle, Rummikub, Ticket_To_Ride }.AsQueryable();
    Publishers = new List<Publisher> { _999Games, Goliath, DaysOfWonder }.AsQueryable();
  }

  public static Publisher _999Games;
  public static Publisher Goliath;
  public static Publisher DaysOfWonder;

  public static Game Quirkle;
  public static Game Rummikub;
  public static Game Ticket_To_Ride;

  public static IQueryable<Publisher> Publishers { get; }
  public static IQueryable<Game> Games { get; }
}
