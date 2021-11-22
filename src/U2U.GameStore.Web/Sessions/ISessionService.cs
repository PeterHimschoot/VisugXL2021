namespace U2U.GameStore.Web.Sessions
{
  public interface ISessionService
  {
    int GetShoppingBasketId();

    void SetShoppingBasketId(int id);
  }
}
