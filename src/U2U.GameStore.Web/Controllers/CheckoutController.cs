using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using U2U.Currencies.Core.ValueObjects;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.Games.Core.Entities;
using U2U.Games.Core.Services;
using U2U.GameStore.Web.Sessions;
using U2U.GameStore.Web.ViewModels.Checkout;

namespace U2U.GameStore.Web.Controllers
{
  public class CheckoutController : Controller
  {
    public const string ControllerName = "Checkout";

    private readonly ICheckoutService checkoutService;
    private readonly ISessionService sessionService;
    private readonly GamePriceService priceService;

    public CheckoutController(ICheckoutService checkoutService, ISessionService sessionService, GamePriceService priceService)
    {
      this.checkoutService = checkoutService;
      this.sessionService = sessionService;
      this.priceService = priceService;
    }

    [HttpGet("/Checkout")]
    public async Task<IActionResult> Create()
    {
      int shoppingBasketId = this.sessionService.GetShoppingBasketId();
      ShoppingBasket? basket = await this.checkoutService.GetShoppingBasketAsync(shoppingBasketId);
      if (basket != null)
      {
        IAsyncEnumerable<(Game, Money)> gamesInBasket = priceService.ConvertToLocalizedAndRoundedPrices(basket.Games);
        var vm = new CheckoutCreateViewModel() { Games = gamesInBasket };
        return View(vm);
      }
      else
      {
        return RedirectToAction(nameof(GamesController.Index), controllerName: GamesController.ControllerName);
      }
    }

    [HttpPost("/Checkout")]
    public async Task<IActionResult> Create(CheckoutCreateViewModel vm)
    {
      if (ModelState.IsValid)
      {
        int shoppingBasketId = this.sessionService.GetShoppingBasketId();
        ShoppingBasket? basket = await this.checkoutService.GetShoppingBasketAsync(shoppingBasketId);
        basket!.AssignCustomer(vm.FirstName, vm.LastName, vm.Street, vm.City);
        await checkoutService.CheckoutBasket(basket);
        return RedirectToAction(nameof(GamesController.Index), controllerName: GamesController.ControllerName);
      }
      else
      {
        return View();
      }
    }
  }
}
