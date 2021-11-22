namespace U2U.GameStore.Web.Sessions
{
  public class SessionService : ISessionService
  {
    private readonly IHttpContextAccessor httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor) => this.httpContextAccessor = httpContextAccessor;

    public int GetShoppingBasketId()
    {
      int? shoppingBasketId = HttpContext.Session.GetInt32(SessionKeys.ShoppingBasket);
      if (shoppingBasketId == null)
      {
        HttpContext.Session.SetInt32(SessionKeys.ShoppingBasket, 0);
        return 0;
      }
      return shoppingBasketId.Value;
    }

    public void SetShoppingBasketId(int id) => HttpContext.Session.SetInt32(SessionKeys.ShoppingBasket, id);

    private HttpContext HttpContext => this.httpContextAccessor.HttpContext!;
  }
}
