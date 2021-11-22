using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using U2U.Currencies.Core.Entities;
using U2U.Games.Core.Entities;
using U2U.EntityFrameworkCore.Sql;

namespace U2U.Games.Infra;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
  public void Configure(EntityTypeBuilder<Game> builder)
  {
    builder.HasKey(g => g.Id);

    builder.HasIndex(g => g.Name)
      .IsUnique();
    builder.Property(g => g.Name)
      .HasMaxLength(128)
      .IsRequired();

    builder
      .HasOne<GameImage>(g => g.Image)
      .WithOne()    // No need for inverse property
      .HasForeignKey<GameImage>(gi => gi.Id);

    builder
      .HasOne(g => g.Publisher)
      .WithMany(p => p.Games);

    builder.Property<int>("PublisherId")
      .IsRequired();

    var priceNavBuilder = builder.OwnsOne(g => g.Price);
    priceNavBuilder.Property(p => p.Amount)
      .HasColumnType("decimal(4,2)")
      .IsRequired();
    priceNavBuilder.Property(p => p.Currency)
      .HasConversion<string>()
      .HasMaxLength(3)
      .IsRequired();

    builder.HasMaintenance();

    //builder.HasData(new
    //{
    //  Id = 1,
    //  Name = "Qwirkle",
    //  PublisherId = Publishers._999Games.Id,
    //  ImageId = 1,
    //  UtcCreated = DateTime.Now,
    //  UtcModified = DateTime.Now
    //},
    //new // Game(1)
    //{
    //  Id = 2,
    //  Name = "Rummikub",
    //  PublisherId = Publishers._Goliath.Id,
    //  ImageId = 2,
    //  UtcCreated = DateTime.Now,
    //  UtcModified = DateTime.Now
    //},
    //new // Game(1)
    //{
    //  Id = 3,
    //  Name = "Ticket To Ride",
    //  PublisherId = Publishers._DaysOfWonder.Id,
    //  UtcCreated = DateTime.Now,
    //  UtcModified = DateTime.Now
    //});

    //// Use priceNameBuilder to set the price data for the game rows
    //priceNavBuilder.HasData(new
    //{
    //  GameId = 1,
    //  Currency = CurrencyName.EUR,
    //  Amount = 29.95M
    //}, new
    //{
    //  GameId = 2,
    //  Currency = CurrencyName.USD,
    //  Amount = 28.95M
    //}, new
    //{
    //  GameId = 3,
    //  Currency = CurrencyName.EUR,
    //  Amount = 34.95M
    //});
  }
}
