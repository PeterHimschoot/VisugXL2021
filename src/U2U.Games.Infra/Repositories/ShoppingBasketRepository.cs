namespace U2U.Games.Infra;

public class ShoppingBasketRepository : Repository<ShoppingBasket, GamesDb>
{
  public ShoppingBasketRepository(GamesDb db) : base(db) { }

  protected override IQueryable<ShoppingBasket> Includes(IQueryable<ShoppingBasket> q)
    => q.Include(c => c.Customer)
        .Include(sb => sb.GamesInBasket)
        .ThenInclude(gib => gib.Game);
}

