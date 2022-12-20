using shorting.Models;

namespace shorting.Grains;

/* TODO
 * - Add expiration
 * - Add ability to choose own URL
 * 
 */
public interface IUrlShortenerGrain : IGrainWithStringKey
{
    Task<string> CreateUrl(string urlToShort, string currentUrl, CustomUrlOptions options = null);
    Task<string> GetUrlUnShortedUrl();
}