using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using shorting.Grains;

namespace shorting.Api;

public static class UrlApi
{
    /*
     * app.MapGet("/shtn/{shortenedurl}", async (IGrainFactory grain, string shortenedurl) =>
{
    var shortenedGrain = grain.GetGrain<IUrlShortenerGrain>(shortenedurl);
    var url = await shortenedGrain.GetUrlUnShortedUrl();
    return Results.Redirect(url);
});
     */
    public static RouteGroupBuilder MapUrlApi(this IEndpointRouteBuilder routes)
    {
        var apiGroup = routes.MapGroup("/shorting");
        apiGroup.WithTags("urls");

        apiGroup.MapGet("/{shortenedUrl}", async (IGrainFactory grain, string shortenedUrl) =>
        {
            var shortenedGrain = grain.GetGrain<IUrlShortenerGrain>(shortenedUrl);
            var url = await shortenedGrain.GetUrlUnShortedUrl();
            return Results.Redirect(url);
        });
        
        apiGroup.MapGet("/test", () => Results.Redirect("/hello"));
        
        return apiGroup;
    }
}