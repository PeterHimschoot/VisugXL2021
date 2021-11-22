namespace U2U.Games.Infra;

public class GamesRepository : Repository<Game, GamesDb>
{
  public GamesRepository(GamesDb db)
    : base(db)
  { }

  protected override IQueryable<Game> Includes(IQueryable<Game> q)
    => q.Include(g => g.Image);
}

