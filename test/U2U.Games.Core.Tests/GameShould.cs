using FluentAssertions;
using System;
using U2U.Currencies.Core.Entities;
using U2U.Currencies.Core.ValueObjects;
using U2U.Games.Core.Entities;
using Xunit;

namespace U2U.Games.Core.Tests
{
  public class GameShould
  {
    [Fact]
    public void KeepItsPriceWhenSet()
    {
      var oldPub = new Publisher(1, "old");
      var game = oldPub.CreateGame("test");
      var price = new Money(100, CurrencyName.EUR);
      game.SetPrice(price);
      Assert.Equal(price, game.Price);
    }

    [Fact]
    public void AddItselfToPublisherGamesWhenCreated()
    {
      var oldPub = new Publisher(1, "old");
      var game = oldPub.CreateGame("test");
      Assert.Contains(oldPub.Games, g => g == game);
    }

    [Fact]
    public void BeRemovedFromOldPublisherWhenMovedToNewPublisher()
    {
      var oldPub = new Publisher(1, "old");
      var game = oldPub.CreateGame("test");
      var newPub = new Publisher(2, "new");

      game.ChangePublisher(newPub);
      Assert.Equal(newPub, game.Publisher);
    }

    [Fact]
    public void UpdateExistingImageWhenChanged()
    {
      var oldPub = new Publisher(1, "old");
      var game = oldPub.CreateGame("test");
      game.SetImage("image");
      game.Image.Should().NotBeNull();
      var img = game.Image;
      string newUrl = "other";

      game.SetImage(newUrl);
      game.Image.Should().Be(img);
      game.ImageURL.Should().Be(newUrl);
    }

    [Fact]
    public void CreateNewImageWhenNonExistedYet()
    {
      var oldPub = new Publisher(1, "old");
      var game = oldPub.CreateGame("test");
      game.Image.Should().BeNull();
      string newUrl = "other";

      game.SetImage(newUrl);
      game.Image.Should().NotBeNull();
      game.ImageURL.Should().Be(newUrl);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ThrowWhenRenamingToEmptyName(string badName)
    {
      var pub = new Publisher(1, "pub");
      var game = pub.CreateGame("test");
      Assert.Throws<ArgumentException>(() => game.Rename(badName));
    }

    [Fact]
    public void KeepNewNameAfterRename()
    {
      var pub = new Publisher(1, "pub");
      var game = pub.CreateGame("test");
      game.Name.Should().Be("test");
      game.Rename("other");
      game.Name.Should().Be("other");
    }

    [Fact(Skip = "No need to check for null with nullable ref types")]
    public void ThrowWhenPriceIsNull()
    {
      var pub = new Publisher(1, "pub");
      var game = pub.CreateGame("test");
      Assert.Throws<ArgumentNullException>(() => game.SetPrice(null));
    }

    [Fact]
    public void ThrowWhenPriceIsZero()
    {
      var pub = new Publisher(1, "pub");
      var game = pub.CreateGame("test");
      Assert.Throws<ArgumentException>(() => game.SetPrice(Money.Zero));
    }

    [Fact]
    public void ThrowWhenPriceIsNegative()
    {
      var pub = new Publisher(1, "pub");
      var game = pub.CreateGame("test");
      Assert.Throws<ArgumentException>(() => game.SetPrice(Money.Eur(-1)));
    }
  }
}
