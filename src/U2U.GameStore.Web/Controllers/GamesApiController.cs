using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using U2U.EntityFrameworkCore;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.Games.Core.Entities;
using U2U.Games.Core.Specifications;
using U2U.GameStore.Web.ViewModels.GamesAPI;

namespace U2U.GameStore.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GamesApiController : ControllerBase
  {
    private readonly IRepository<Game> gameRepo;
    private readonly IReadonlyRepository<Publisher> pubRepo;
    private readonly GameSpecificationFactories gameSpecificationFactories;

    public GamesApiController(IRepository<Game> gameRepo,
      IReadonlyRepository<Publisher> pubRepo,
      GameSpecificationFactories gameSpecificationFactories)
    {
      this.gameRepo = gameRepo;
      this.pubRepo = pubRepo;
      this.gameSpecificationFactories = gameSpecificationFactories;
    }

    // GET: api/GamesApi
    [HttpGet]
    public async Task<IEnumerable<Game>> GetGames()
       => await this.gameRepo.ListAsync(gameSpecificationFactories.For<Game>().All());

    // GET: api/GamesApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(int id)
    {
      var spec = gameSpecificationFactories.For<Game>()
        .WithId(id).Including(game => game.Publisher);
      Game? game = await this.gameRepo.SingleAsync(spec);
      if (game == null)
      {
        return NotFound();
      }
      return Ok(game);
    }

    // PUT: api/GamesApi/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, UpdateGameViewModel game)
    {
      if (id != game.Id)
      {
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var spec = gameSpecificationFactories.For<Game>()
        .WithId(id).Including(game => game.Publisher);
      Game? gameEntity = await this.gameRepo.SingleAsync(spec);
      if (gameEntity == null)
      {
        return NotFound();
      }

      gameEntity.SetPrice(new Currencies.Core.ValueObjects.Money(game.Amount, game.Currency));

      await this.gameRepo.UpdateAsync(gameEntity);
      await this.gameRepo.SaveChangesAsync();

      return NoContent();
    }

    // POST: api/GamesApi
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Game>> PostGame(InsertGameViewModel game)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var spec = gameSpecificationFactories.For<Publisher>()
        .WithId(game.PublisherId);
      Publisher? pub = await pubRepo.SingleAsync(spec);
      if (pub == null)
      {
        return BadRequest();
      }
      Game gameEntity = pub.CreateGame(game.Name);
      gameEntity.SetPrice(new Currencies.Core.ValueObjects.Money(game.Amount, game.Currency));
      await this.gameRepo.InsertAsync(gameEntity);
      await this.gameRepo.SaveChangesAsync();

      return CreatedAtAction("GetGame", new { id = gameEntity.Id }, game);
    }

    // DELETE: api/GamesApi/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Game>> DeleteGame(int id)
    {
      var spec = gameSpecificationFactories.For<Game>()
        .WithId(id).Include(game => game.Publisher);
      var gameEntity = await this.gameRepo.SingleAsync(spec);
      if (gameEntity == null)
      {
        return NotFound();
      }
      await this.gameRepo.DeleteAsync(gameEntity);
      await this.gameRepo.SaveChangesAsync();
      return gameEntity;
    }
  }
}
