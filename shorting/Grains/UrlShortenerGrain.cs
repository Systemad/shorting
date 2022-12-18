using Orleans.Runtime;
using shorting.Models;

namespace shorting.Grains;

public class UrlShortenerGrain : Grain, IUrlShortenerGrain
{
    private readonly IPersistentState<UrlShortenerGrainState> _state;

    public UrlShortenerGrain(
        [PersistentState(stateName:"url", storageName:"urls")]
        IPersistentState<UrlShortenerGrainState> state)
    {
        _state = state;
    }

    private string GrainKey => this.GetPrimaryKeyString();

    public async Task<string> CreateUrl(string urlToShort, string currentUrl)
    {
        CheckValidation();
        var formattedUrl = new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter + urlToShort);
        _state.State._cache = new KeyValuePair<string, string>(GrainKey, formattedUrl.AbsoluteUri);
        _state.State.IsCreated = true;
        await _state.WriteStateAsync();
        
        var resultBuilder = new UriBuilder(currentUrl)
        {
            Path = $"/shtn/{GrainKey}"
        };
        return resultBuilder.ToString();
    }
    public async Task<string> CreateUrl(string urlToShort, string currentUrl, CustomUrlOptions options)
    {
        CheckValidation();
        var formattedUrl = new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter + urlToShort);
        _state.State._cache = new KeyValuePair<string, string>(GrainKey, formattedUrl.AbsoluteUri);
        //if (options.Expiration is not null)
        //    _state.State.Expiration = options.Expiration;
        
        _state.State.IsCreated = true;
        await _state.WriteStateAsync();
        var resultBuilder = new UriBuilder(currentUrl)
        {
            Path = $"/shtn/{GrainKey}"
        };
        return resultBuilder.ToString();
    }

    public async Task<string> GetUrl()
    {
        _state.State.TimesAccessed++;
        await _state.WriteStateAsync();
        return _state.State._cache.Value;
    }

    private void CheckValidation()
    {
        if (_state.State.IsCreated) throw new InvalidOperationException("Url already exist!");
        if (_state.State.ExpirationSet)
        {
            if (_state.State.Expired is true)
            {
                throw new InvalidOperationException("Url has expired");
            }
        }
    }
    private void SetExpiration(){}
}