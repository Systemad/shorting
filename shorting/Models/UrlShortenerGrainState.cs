namespace shorting.Models;

public class UrlShortenerGrainState
{
    public bool IsCreated { get; set; }
    public int TimesAccessed { get; set; }
    public KeyValuePair<string, string> _cache { get; set; }
    public bool ExpirationSet { get; set; }
    public TimeSpan? Expiration { get; set; }
    public int? AccessAmount { get; set; }
}