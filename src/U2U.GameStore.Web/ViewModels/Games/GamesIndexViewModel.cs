using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using U2U.Currencies.Core.ValueObjects;
using U2U.Games.Core.Entities;

namespace U2U.GameStore.Web.ViewModels.Games
{
  public class GamesIndexViewModel
  {
    public IAsyncEnumerable<(Game game, Money localizedPrice)> Games { get; }

    public GamesIndexViewModel(IAsyncEnumerable<(Game game, Money localizedPrice)> games)
      => this.Games = games;
  }
}
