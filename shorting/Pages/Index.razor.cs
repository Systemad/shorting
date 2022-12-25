using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
    [Inject] private IJSRuntime JsRuntime { get; set; }
    
    private string URL;
    private string? _customUrlName;
    private string? ShortenedUrl;

    private static Dictionary<string, int> ExpirationOptions = new();
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
        
        // TODO: catch error if custom url name has already been use and display nicely
        if (customOptions)
        {
            var url = _customUrlName ?? UrlHelper.GetUrl();
            var shortGrain = _grainFactory.GetGrain<IUrlShortenerGrain>(url);
            ShortenedUrl = await shortGrain.CreateUrl(
                URL, 
                NavigationManager.ToAbsoluteUri(NavigationManager.Uri).AbsoluteUri,
                CustomUrlOptions);
        }
        else
        {
            var url = UrlHelper.GetUrl();
            var shortGrain = _grainFactory.GetGrain<IUrlShortenerGrain>(url);
            ShortenedUrl = await shortGrain.CreateUrl(URL, NavigationManager.ToAbsoluteUri(NavigationManager.Uri).AbsoluteUri);   
        }
        _processing = false;
    }

    private void GetExpirationValue(string minutes)
    {
        CustomUrlOptions.ExpirationMinutes = minutes.GetExpirationOption();
        Console.WriteLine(minutes + " " + CustomUrlOptions.ExpirationMinutes);
    }
    
    private async Task CopyTextToClipboard()
    {
        if(ShortenedUrl is not null)
            await JsRuntime.InvokeVoidAsync("clipboardCopy.copyText", ShortenedUrl);
    }
}