namespace U2U.Games.Core.Entities;

public class Customer : EntityBase
{
  public Customer(int id, string firstName, string lastName)
    : base(id)
  {
    FirstName = firstName;
    LastName = lastName;
  }

  public string FirstName { get; private set; }

  public string LastName { get; private set; }

  public Address? Address { get; private set; }

  public ShoppingBasket? ShoppingBasket { get; set; }

  public void MoveToNewAddress(Address address)
    => Address = address;

  public void AddGameToShoppingBasket(Game game)
  {
    ShoppingBasket ??= new ShoppingBasket(0);
    ShoppingBasket.AddGame(game);
  }
}

