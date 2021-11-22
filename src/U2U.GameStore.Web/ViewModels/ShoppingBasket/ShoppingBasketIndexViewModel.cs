using System.Collections.Generic;
using U2U.Currencies.Core.ValueObjects;
using U2U.Games.Core.Entities;

namespace U2U.GameStore.Web.ViewModels.ShoppingBasket
{
  public class ShoppingBasketIndexViewModel
  {
    public ShoppingBasketIndexViewModel(IAsyncEnumerable<(Game game, Money localizedPrice)> games)
      => this.Games = games;

    public IAsyncEnumerable<(Game game, Money localizedPrice)> Games { get; }

  }
}
