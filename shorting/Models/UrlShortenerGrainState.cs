namespace shorting.Models;

[GenerateSerializer]
public class UrlShortenerGrainState
{
    [Id(0)]
    public bool IsCreated { get; set; }
    [Id(1)]
    public int TimesAccessed { get; set; }
    [Id(2)]
    public KeyValuePair<string, string> _cache { get; set; }
    [Id(3)]
    public int? AccessAmount { get; set; }

    [Id(4)]
    public bool ExpirationSet { get; set; }
    [Id(5)]
    public bool? Expired { get; set; }
    [Id(6)]
    public TimeSpan? Expiration { get; set; }
}