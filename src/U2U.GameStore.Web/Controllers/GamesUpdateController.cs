using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U2U.Currencies.Core.ValueObjects;
using U2U.EntityFrameworkCore;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.Games.Core.Entities;
using U2U.Games.Core.Specifications;

namespace U2U.GameStore.Web.Controllers
{
  [ApiController]
  public class GamesUpdateController : ControllerBase
  {
    private readonly IRepository<Game> gameRepo;
    private readonly GameSpecificationFactories gameSpecificationFactories;

    public GamesUpdateController(IRepository<Game> gameRepo, GameSpecificationFactories gameSpecificationFactories)
    {
      this.gameRepo = gameRepo;
      this.gameSpecificationFactories = gameSpecificationFactories;
    }

    [HttpGet("/change")]
    public async Task<IActionResult> ChangeGamePrice()
    {
      var spec = gameSpecificationFactories.For<Game>().WithId(1);
      var game = await gameRepo.SingleAsync(spec);
      if (game == null)
      {
        return NotFound();
      }
      game.SetPrice(Money.Eur(10));
      await gameRepo.SaveChangesAsync();
      return Ok();
    }

  }
}
