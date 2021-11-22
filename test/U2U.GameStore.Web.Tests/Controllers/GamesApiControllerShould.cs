using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using U2U.Currencies.Core.Entities;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.EntityFrameworkCore.Testing;
using U2U.Games.Core.Entities;
using U2U.Games.Core.Specifications;
using U2U.Games.FakeData;
using U2U.GameStore.Web.Controllers;
using U2U.GameStore.Web.ViewModels.GamesAPI;
using Xunit;

namespace U2U.GameStore.Web.Tests.Controllers
{
  public class GamesApiControllerShould 
    : IClassFixture<TestableGameSpecificationFactories>
  {
    private readonly TestableGameSpecificationFactories gameSpecificationFactories;

    public GamesApiControllerShould(
      TestableGameSpecificationFactories gameSpecificationFactories)
    {
      this.gameSpecificationFactories = gameSpecificationFactories;
    }

    private GamesApiController CreateAPIController(IRepository<Game> fakeGameRepo, IRepository<Publisher> fakePubRepo)
      => new GamesApiController(fakeGameRepo, fakePubRepo, gameSpecificationFactories);

    private GamesApiController CreateAPIController()
    {
      var gameRepoFactory = new FakeRepositoryFactory<Game>();
      IRepository<Game> fakeGameRepo = gameRepoFactory.FakeRepoWithData(Fake.Games);
      var pubRepoFactory = new FakeRepositoryFactory<Publisher>();
      IRepository<Publisher> fakePubRepo = pubRepoFactory.FakeRepoWithData(Fake.Publishers);
      return CreateAPIController(fakeGameRepo, fakePubRepo);
    }

    [Fact]
    public async Task ListAllGames()
    {
      var gameApiController = CreateAPIController();
      IEnumerable<Game> games = await gameApiController.GetGames();
      Assert.NotNull(games);
      Assert.All(games, game => Fake.Games.Any(g => g.Name == game.Name));
    }

    [Fact]
    public async Task ReturnExistingGame()
    {
      var gameApiController = CreateAPIController();

      int gameId = Fake.Games.First().Id;
      ActionResult<Game> result = await gameApiController.GetGame(gameId);
      ActionResult<Game> actionResult = Assert.IsType<ActionResult<Game>>(result);
      OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
      Game game = Assert.IsType<Game>(okResult.Value);

      Assert.Equal(gameId, game.Id);
    }

    [Fact]
    public async Task Return404ForNonExistingGame()
    {
      var gameApiController = CreateAPIController();

      ActionResult<Game> result = await gameApiController.GetGame(1000);
      ActionResult<Game> actionResult = Assert.IsType<ActionResult<Game>>(result);
      Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task InsertNewValidGameOnCreated()
    {
      var gameRepoMock = new Mock<IRepository<Game>>();
      IRepository<Game> fakeGameRepo = gameRepoMock.Object;
      var pubRepoFactory = new FakeRepositoryFactory<Publisher>();
      IRepository<Publisher> fakePubRepo = pubRepoFactory.FakeRepoWithData(Fake.Publishers);
      var gameApiController = CreateAPIController(fakeGameRepo, fakePubRepo);

      var newGame = new InsertGameViewModel() { Name = "Foo", Amount = 10, Currency = CurrencyName.EUR, PublisherId = 1 };
      ActionResult<Game> actionResult = await gameApiController.PostGame(newGame);
      CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
      InsertGameViewModel game = Assert.IsType<InsertGameViewModel>(createdAtActionResult.Value);
      // Verify it was inserted with the repo
      gameRepoMock.Verify(gr => gr.InsertAsync(It.Is<Game>(g => g.Name == newGame.Name)), Times.Once);
    }

    [Fact]
    public async Task ReturnBadRequestForInvalidGame()
    {
      var gameRepoMock = new Mock<IRepository<Game>>();
      IRepository<Game> fakeGameRepo = gameRepoMock.Object;
      var pubRepoFactory = new FakeRepositoryFactory<Publisher>();
      IRepository<Publisher> fakePubRepo = pubRepoFactory.FakeRepoWithData(Fake.Publishers);
      var gameApiController = CreateAPIController(fakeGameRepo, fakePubRepo);

      var newGame = new InsertGameViewModel() { Name = "Foo", Amount = 10, Currency = CurrencyName.EUR, PublisherId = 1 };
      gameApiController.ModelState.AddModelError(nameof(newGame.Name), "Required");

      ActionResult<Game> actionResult = await gameApiController.PostGame(newGame);
      BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
      // Verify it was NOT inserted with the repo
      gameRepoMock.Verify(gr => gr.InsertAsync(It.IsAny<Game>()), Times.Never);
    }
  }
}
