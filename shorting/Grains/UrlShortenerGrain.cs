using Orleans.Runtime;
using shorting.Helpers;
using shorting.Models;

namespace shorting.Grains;

public class UrlShortenerGrain : Grain, IUrlShortenerGrain //, IRemindable
{
    private readonly IPersistentState<UrlShortenerGrainState> _state;
    

    public UrlShortenerGrain(
        [PersistentState(stateName:"url", storageName:"urls")]
        IPersistentState<UrlShortenerGrainState> state)
    {
        _state = state;
    }

    private string GrainKey => this.GetPrimaryKeyString();
    
    public async Task<string> CreateUrl(string urlToShort, string currentUrl, CustomUrlOptions? options = null)
    {
        if (_state.State.IsCreated) throw new InvalidOperationException("Url already exist!");
        if (options is not null)
        {
            if (options.ExpirationMinutes != 0)
            {
                _state.State.ExpirationSet = true;
                _state.State.Expiration = TimeSpan.FromSeconds(options.ExpirationMinutes);
            }
            if (options.AccessAmount != 0)
            {
                _state.State.AccessLimitSet = true;
                _state.State.AccessLimit = options.AccessAmount;    
            }
        }
        var formattedUrl = new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter + urlToShort);
        _state.State._cache = new KeyValuePair<string, string>(GrainKey, formattedUrl.AbsoluteUri);
        _state.State.IsCreated = true;
        await _state.WriteStateAsync();
        var resultBuilder = new UriBuilder(currentUrl)
        {
            Path = $"/shorting/{GrainKey}"
        };
        return resultBuilder.ToString();
    }
    
    public async Task<string> GetUrlUnShortedUrl()
    {
        ReturnValidation();
        _state.State.TimesAccessed++;
        await _state.WriteStateAsync();
        return _state.State._cache.Value;
    }
    private void ReturnValidation()
    {
        if(!_state.State.IsCreated) throw new InvalidOperationException("url doesn't exist");
        if (_state.State.AccessLimitSet)
        {
            if (!(_state.State.TimesAccessed < _state.State.AccessLimit))
            {
                _state.State.Expired = true;
                throw new InvalidOperationException("Access amount already met!");
            }   
        }
        if (_state.State is { ExpirationSet: true, Expired: true })
        {
            throw new InvalidOperationException("Url has expired");
        }
    }

    /*
    public async Task RegisterExpiration(int time)
    {
        IGrainReminder reminder;
        
        reminder = await this.RegisterOrUpdateReminder()
    }
    
    public Task ReceiveReminder(string reminderName, TickStatus status)
    {
        w
        throw new NotImplementedException();
    }
    */
}