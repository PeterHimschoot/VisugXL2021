using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using U2U.Currencies.Core.Services;
using U2U.Currencies.Core.ValueObjects;
using U2U.Games.Core.Entities;
using U2U.Games.Core.Services;
using U2U.GameStore.Web.Sessions;
using U2U.GameStore.Web.ViewModels.ShoppingBasket;

namespace U2U.GameStore.Web.Controllers
{
  public class ShoppingBasketController : Controller
  {
    public const string ControllerName = "ShoppingBasket";

    private readonly ILogger<ShoppingBasketController> logger;
    private readonly IShoppingBasketService sbService;
    private readonly ISessionService sessionService;
    private readonly GamePriceService priceService;

    public ShoppingBasketController(IShoppingBasketService sbService, ISessionService sessionService, GamePriceService priceService, ILogger<ShoppingBasketController> logger)
    {
      this.sbService = sbService;
      this.sessionService = sessionService;
      this.priceService = priceService;
      this.logger = logger;
    }

    [HttpGet("/SB/Add/{gameId:int}")]
    public async Task<IActionResult> AddToBasket(int gameId)
    {
      int shoppingBasketId = this.sessionService.GetShoppingBasketId();
      shoppingBasketId = await this.sbService.AddGameToShoppingBasketWithId(shoppingBasketId, gameId);
      this.sessionService.SetShoppingBasketId(shoppingBasketId);
      return RedirectToAction(nameof(Index));
    }

    [HttpGet("ShoppingBasket")]
    public async Task<IActionResult> Index()
    {
      int shoppingBasketId = this.sessionService.GetShoppingBasketId();
      ShoppingBasket? basket = await this.sbService.GetShoppingBasketAsync(shoppingBasketId);
      if (basket != null)
      {
        IAsyncEnumerable<(Game, Money)> gamesInBasket = priceService.ConvertToLocalizedAndRoundedPrices(basket.Games);
        return View(new ShoppingBasketIndexViewModel(gamesInBasket));
      }
      else
      {
        return RedirectToAction(nameof(GamesController.Index), controllerName: GamesController.ControllerName);
      }
    }
  }
}
