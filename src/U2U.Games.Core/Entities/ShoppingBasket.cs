namespace U2U.Games.Core.Entities;

public class ShoppingBasket : EntityBase, IAggregateRoot
{
  public ShoppingBasket(int id) : base(id) { }

  public Customer Customer { get; private set; } = default!;

  public int? CustomerId { get; set; }

  public void AssignCustomer(string firstName, string lastName, string street, string city)
  {
    Customer = new Customer(0, firstName, lastName);
    Address address = new Address(street, city);
    Customer.MoveToNewAddress(address);
  }

  public void CheckOut()
  {
    this.RegisterDomainEvent(new ShoppingBasketHasCheckedOut(this));
  }

  public void AddGame(Game game)
  {
    GetBasket().Add(new GameInBasket() { Game = game });
  }

  private ICollection<GameInBasket> GetBasket()
    => gamesInBasket ??= new List<GameInBasket>();

  public void Remove(Game game)
  {
    if (gamesInBasket != null)
    {
      gamesInBasket = new List<GameInBasket>(gamesInBasket.Where(gb => gb.Game != game));
    }
  }

  public IEnumerable<Game> Games
    => gamesInBasket?.Select(gib => gib.Game) ?? Enumerable.Empty<Game>();

  public IEnumerable<GameInBasket> GamesInBasket
    => gamesInBasket ?? Enumerable.Empty<GameInBasket>();

  private ICollection<GameInBasket> gamesInBasket = default!;
}
