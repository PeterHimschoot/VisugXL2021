using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U2U.Currencies.Core.ValueObjects;
using U2U.Games.Core.Entities;

namespace U2U.GameStore.Web.ViewModels.Checkout
{
  public class CheckoutCreateViewModel
  {
    public CheckoutCreateViewModel()
    {
      FirstName = string.Empty;
      LastName = string.Empty;
      Street = string.Empty;
      City = string.Empty;
    }

    public IAsyncEnumerable<(Game game, Money localizedPrice)>? Games { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }
  }
}
