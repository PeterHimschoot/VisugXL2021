namespace U2U.Games.Core.Specifications;

public class PublisherSpecificationFactory : SpecificationFactory<Publisher>
{
  public ISpecification<Publisher> ForGame(Game game)
    => new PublisherForGame(game);

  private class PublisherForGame : Specification<Publisher>
  {
    public PublisherForGame(Game game)
      : base(publisher => publisher.Games.Contains(game)) { }
  }
}
