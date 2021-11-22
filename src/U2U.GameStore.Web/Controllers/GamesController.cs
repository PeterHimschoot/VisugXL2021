using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using U2U.Currencies.Core.ValueObjects;
using U2U.Games.Core.Entities;
using U2U.Games.Core.Services;
using U2U.GameStore.Web.ViewModels.Games;

namespace U2U.GameStore.Web.Controllers
{
  public class GamesController : Controller
  {
    public const string ControllerName = "Games";

    private readonly ILogger<GamesController> logger;
    private readonly IGameService gameService;

    public GamesController(IGameService gameService, ILogger<GamesController> logger)
    {
      this.gameService = gameService;
      this.logger = logger;
    }

    [Route("/")]
    [Route("/Games")]
    public IActionResult Index()
    {
      IAsyncEnumerable<(Game game, Money localizedPrice)> gamesWithLocalizedPrices
        = this.gameService.GamesAsync();

      var vm = new GamesIndexViewModel(gamesWithLocalizedPrices);
      return View(model: vm);
    }

    [Route("/Create")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
      IEnumerable<Publisher>? publishers = await this.gameService.PublishersAsync();
      return View(new GamesCreateViewModel
      {
        Publishers = publishers
      });
    }

    [Route("/Create")]
    [HttpPost]
    public async Task<IActionResult> Create(GamesCreateViewModel vm)
    {
      if (!ModelState.IsValid)
      {
        vm.Publishers = await this.gameService.PublishersAsync();
        return View(vm);
      }

      Publisher? pub = await this.gameService.PublisherWithIdAsync(vm.PublisherId);
      Game? game = pub!.CreateGame(vm.Name);
      await this.gameService.InsertGameAsync(game);

      return RedirectToAction(nameof(Index));
    }
  }
}
