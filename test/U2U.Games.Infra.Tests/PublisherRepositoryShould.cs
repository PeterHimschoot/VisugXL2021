namespace U2U.Games.Infra.Tests;

public class PublisherRepositoryShould
{
  private readonly DbInjectionFixture<GamesDb> fixture;
  private readonly TestableGameSpecificationFactories gameSpecificationFactories;
  private readonly PublisherSpecificationFactory publisherSpecificationFactory;

  public PublisherRepositoryShould(DbInjectionFixture<GamesDb> fixture
                         , TestableGameSpecificationFactories gameSpecificationFactories)
  {
    this.fixture = fixture;
    this.gameSpecificationFactories = gameSpecificationFactories;
    this.publisherSpecificationFactory = gameSpecificationFactories.For<Publisher, PublisherSpecificationFactory>();
    if (this.publisherSpecificationFactory == null)
    {
      throw new ArgumentException($"Missing {nameof(PublisherSpecificationFactory)}");
    }
  }
}

