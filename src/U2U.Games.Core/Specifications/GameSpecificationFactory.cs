namespace U2U.Games.Core.Specifications;

public class GameSpecificationFactory : SpecificationFactory<Game>
{
  private readonly ICurrencyConverterService currencyConverter;

  public GameSpecificationFactory(ICurrencyConverterService currencyConverter)
    => this.currencyConverter = currencyConverter;

  public ISpecification<Game> WithPublisher(Publisher pub)
    => new GameWithPublisherSpecification(pub);

  public ISpecification<Game> WithMaxAmount(Money maxAmount)
  {
    maxAmount = this.currencyConverter.ConvertAmountFromAsync(maxAmount, CurrencyName.EUR).GetAwaiter().GetResult();
    return new GamesWithPriceLowerThanInclusive(maxAmount);
  }
  public ISpecification<Game> WithMinAmount(Money minAmount)
  {
    minAmount = this.currencyConverter.ConvertAmountFromAsync(minAmount, CurrencyName.EUR).GetAwaiter().GetResult();
    return new GamesWithPriceHigherThanInclusive(minAmount);
  }

  public ISpecification<Game> InRangeInclusive(Money minAmount, Money maxAmount)
    => new GamesWithPriceHigherThanInclusive(minAmount)
       .And(new GamesWithPriceLowerThanInclusive(maxAmount));

  public ISpecification<Game> GamesForPublisher(int publisherID)
    => new GamesForPublisherSpecification(publisherID);

  private class GameWithPublisherSpecification : Specification<Game>
  {
    public GameWithPublisherSpecification(Publisher pub)
      : base(game => game.Publisher == pub) { }
  }

  private class GamesWithPriceLowerThanInclusive : Specification<Game>
  {
    public GamesWithPriceLowerThanInclusive(Money maxAmount)
      : base(game => game.Price.Amount <= maxAmount.Amount)
    { }
  }

  private class GamesWithPriceHigherThanInclusive : Specification<Game>
  {
    public GamesWithPriceHigherThanInclusive(Money minAmount)
      : base(game => game.Price.Amount >= minAmount.Amount)
    { }
  }

  private class GamesForPublisherSpecification : Specification<Game>
  {
    public GamesForPublisherSpecification(int publisherID)
      : base(game => EF.Property<int>(game, "PublisherId") == publisherID)
    { }
  }
}
