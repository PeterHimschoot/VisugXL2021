namespace U2U.Games.Infra.Data;

public class GameImageData : IEntityTypeData<GameImage>
{
  public void HasData(EntityTypeBuilder<GameImage> gameImage) => gameImage.HasData(
      new
      {
        Id = 1,
        Name = "Qwirkle",
        ImageLocation = "https://u2ublogimages.blob.core.windows.net/cleanarchitecture/GamesStore_Qwirkle.png"
      },
      new
      {
        Id = 2,
        Name = "Rummikub",
        ImageLocation = "https://u2ublogimages.blob.core.windows.net/cleanarchitecture/GamesStore_Rummikub.jpg"
      }
    );
}
