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
        ArgumentNullException.ThrowIfNull(urlToShort);
        var formattedUrl = new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter + urlToShort);
        _state.State._cache = new KeyValuePair<string, string>(GrainKey, formattedUrl.AbsoluteUri);
        _state.State.IsCreated = true;
        await _state.WriteStateAsync();
        return BuildAndReturnUrl(currentUrl);
    }
    public async Task<string> CreateUrl(string urlToShort, string currentUrl, CustomUrlOptions options)
    {
        ArgumentNullException.ThrowIfNull(urlToShort);
        if (options.Expiration is not null)
        {
            _state.State.ExpirationSet = true;
            _state.State.Expiration = options.Expiration;
        }
        if (options.AccessAmount is not null)
        {
            _state.State.AccessLimitSet = true;
            _state.State.AccessLimit = options.AccessAmount;
        }
        var url = await CreateUrl(urlToShort, currentUrl);
        return url;
    }

    private string BuildAndReturnUrl(string currentUrl)
    {
        var resultBuilder = new UriBuilder(currentUrl)
        {
            Path = $"/shtn/{GrainKey}"
        };
        return resultBuilder.ToString();
    }
    public async Task<string> GetUrlUnShortedUrl()
    {
        CheckValidation();
        _state.State.TimesAccessed++;
        await _state.WriteStateAsync();
        return _state.State._cache.Value;
    }

    private void CheckValidation()
    {
        if (_state.State.IsCreated) throw new InvalidOperationException("Url already exist!");
        
        if (_state.State is { ExpirationSet: true, Expired: true })
        {
            throw new InvalidOperationException("Url has expired");
        }

        if (_state.State.AccessLimitSet)
        {
            if (!(_state.State.TimesAccessed < _state.State.AccessLimit))
            {
                _state.State.Expired = true;
                throw new InvalidOperationException("Access amount already met!");
            }   
        }
    }
    private void SetExpiration(){}
}