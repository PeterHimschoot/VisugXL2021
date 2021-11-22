namespace U2U.Games.Core.Entities;

[DebuggerDisplay("Game {Name} - {Price.Amount}.")]
public class Game : EntityBase, IAggregateRoot
{
  private const string DefaultImageURL = "https://u2ublogimages.blob.core.windows.net/cleanarchitecture/GamesStore_BoardGame.jpg";

  private readonly static Money DefaultGamePrice = new Money(50);

  /// <summary>
  /// Ctor for use by EF Core
  /// </summary>
  /// <param name="id">primary key</param>
  /// <param name="name">name</param>
  internal Game(int id, string name) : base(id)
    => this.Name = name;

  internal Game(int id, string name, Publisher publisher) : this(id, name)
  {
    this.SetPrice(DefaultGamePrice);
    this.Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    this.Publisher.AddGame(this);
  }

  public string Name { get; private set; } = default!;

  public Publisher Publisher { get; private set; } = default!;

  public Money Price { get; private set; } = default!;

  public GameImage Image { get; private set; } = default!;

  public string ImageURL
  {
    get
    {
      if (this.Image == null)
      {
        return DefaultImageURL;
      }
      return this.Image.ImageLocation;
    }
  }

  public string PublisherName
    => this.Publisher.Name;

  public void Rename(string name)
  {
    if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));
    this.Name = name;
  }

  public void SetPrice(in Money price)
  {
    if (price.Amount <= 0) throw new ArgumentException(nameof(price));
    this.Price = price;
    this.RegisterDomainEvent(new GamePriceHasChanged(this));
  }

  public void SetImage(string imageUrl)
  {
    if (this.Image == null)
    {
      var image = new GameImage(0, imageUrl);
      this.Image = image;
    }
    else
    {
      this.Image.SetImageUrl(imageUrl);
    }
  }

  public void ChangePublisher(Publisher pub)
  {
    if (this.Publisher == pub)
      return;
    this.Publisher.RemoveGame(this);
    pub.AddGame(this);
    this.Publisher = pub;
  }
}
