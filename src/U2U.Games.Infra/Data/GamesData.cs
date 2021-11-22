namespace U2U.Games.Infra.Data;

public class GameData : IEntityTypeData<Game>
{
  public void HasData(EntityTypeBuilder<Game> game)
  {
    game.HasData(new
    {
      Id = 1,
      Name = "Qwirkle",
      PublisherId = Publishers._999Games.Id,
      ImageId = 1,
      UtcCreated = DateTime.Now,
      UtcModified = DateTime.Now
    },
    new // Game(1)
      {
      Id = 2,
      Name = "Rummikub",
      PublisherId = Publishers._Goliath.Id,
      ImageId = 2,
      UtcCreated = DateTime.Now,
      UtcModified = DateTime.Now
    },
    new // Game(1)
      {
      Id = 3,
      Name = "Ticket To Ride",
      PublisherId = Publishers._DaysOfWonder.Id,
      UtcCreated = DateTime.Now,
      UtcModified = DateTime.Now
    });

    OwnedNavigationBuilder<Game, Money>? priceNavBuilder =
      game.OwnsOne(g => g.Price);

    // Use priceNameBuilder to set the price data for the game rows
    priceNavBuilder.HasData(new
    {
      GameId = 1,
      Currency = CurrencyName.EUR,
      Amount = 29.95M
    }, new
    {
      GameId = 2,
      Currency = CurrencyName.USD,
      Amount = 28.95M
    }, new
    {
      GameId = 3,
      Currency = CurrencyName.EUR,
      Amount = 34.95M
    });
  }
}
