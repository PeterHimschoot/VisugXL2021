namespace U2U.Currencies.FakeData;

public class SomeCultures : List<object[]>
{
  public SomeCultures()
  {
    foreach (System.Globalization.CultureInfo culture in SupportedCultures.Instance)
    {
      Add(new[] { culture.Name });
    }
  }
}

