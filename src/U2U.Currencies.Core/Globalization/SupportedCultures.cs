namespace U2U.Currencies.Core.Globalization;

public sealed class SupportedCultures : List<CultureInfo>
{
  private const string belgianDutchCulture = "nl-BE";
  private const string belgianFrenchCulture = "fr-BE";
  private const string netherlandsCulture = "nl-NL";
  private const string defaultCulture = "en-US";

  private SupportedCultures()
  {
    Add(new CultureInfo(belgianDutchCulture));
    Add(new CultureInfo(netherlandsCulture));
    Add(new CultureInfo(defaultCulture));
    Add(new CultureInfo(belgianFrenchCulture));
  }

  public static SupportedCultures Instance { get; }
    = new SupportedCultures();

  public static CultureInfo DefaultCulture { get; }
    = new CultureInfo(defaultCulture);
}
