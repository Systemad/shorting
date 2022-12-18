using Microsoft.AspNetCore.Components;
using MudBlazor;
using shorting.Grains;
using shorting.Helpers;
using shorting.Models;

namespace shorting.Pages;

public partial class Index
{
    [Inject] private IGrainFactory _grainFactory { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    
    private string URL;
    private string customUrlName;
    private string? ShortenedUrl;

    private static Dictionary<int, string> ExpirationOptions;
    private CustomUrlOptions CustomUrlOptions { get; set; } = new();
    private bool customOptions = false;
    private bool _processing = false;

    protected override void OnInitialized()
    {
        ExpirationOptions = SettingsHelper.GetExpirationOptions();
    }

    private async Task ShortenUrl()
    {
        if (string.IsNullOrEmpty(URL) || string.IsNullOrWhiteSpace(URL))
        {
            Snackbar.Add("Error: enter a valid URL");
            return;
        }
        _processing = true;
        var shortenedSegment = Guid.NewGuid().GetHashCode().ToString("X");
        var shortGrain = _grainFactory.GetGrain<IUrlShortenerGrain>(shortenedSegment);
        if (customOptions)
        {
            ShortenedUrl = await shortGrain.CreateUrl(
                URL, 
                NavigationManager.ToAbsoluteUri(NavigationManager.Uri).AbsoluteUri,
                CustomUrlOptions);
        }
        else
        {
            ShortenedUrl = await shortGrain.CreateUrl(URL, NavigationManager.ToAbsoluteUri(NavigationManager.Uri).AbsoluteUri);   
        }
        _processing = false;
    }
}