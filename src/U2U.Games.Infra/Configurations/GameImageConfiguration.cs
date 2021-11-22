namespace U2U.Games.Infra;

internal class GameImageConfiguration : IEntityTypeConfiguration<GameImage>
{
  public void Configure(EntityTypeBuilder<GameImage> builder)
  {
    builder.HasKey(gi => gi.Id);

    builder.Property(gi => gi.ImageLocation)
      .HasMaxLength(1024)
      .IsRequired();

    //builder.HasData(
    //  new
    //  {
    //    Id = 1,
    //    Name = "Qwirkle",
    //    ImageLocation = "https://u2ublogimages.blob.core.windows.net/cleanarchitecture/GamesStore_Qwirkle.png"
    //  },
    //  new
    //  {
    //    Id = 2,
    //    Name = "Rummikub",
    //    ImageLocation = "https://u2ublogimages.blob.core.windows.net/cleanarchitecture/GamesStore_Rummikub.jpg"
    //  }
    //);
  }
}

