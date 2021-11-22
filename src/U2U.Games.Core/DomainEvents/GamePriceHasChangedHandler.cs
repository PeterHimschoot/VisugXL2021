namespace U2U.Games.Core.DomainEvents;

public class GamePriceHasChangedHandler : IDomainEventHandler<GamePriceHasChanged>
{
  private readonly IReadonlyRepository<Publisher> pubRepo;
  private readonly GameSpecificationFactories specificationFactories;

  public GamePriceHasChangedHandler(IReadonlyRepository<Publisher> pubRepo, GameSpecificationFactories specificationFactories)
  {
    this.pubRepo = pubRepo;
    this.specificationFactories = specificationFactories;
  }

  public async ValueTask Handle(object @event)
  {
    GamePriceHasChanged gp = (GamePriceHasChanged)@event;
    PublisherSpecificationFactory pubSF =
      (this.specificationFactories.For<Publisher>() as PublisherSpecificationFactory)!;
    ISpecification<Publisher>? spec = pubSF.ForGame(gp.Game);
    Publisher? pub = await this.pubRepo.SingleAsync(spec);
    // ... notify publisher of price change
  }
}

