using Microsoft.AspNetCore.Http.HttpResults;
using shorting.Grains;

namespace shorting.Api;

public static class UrlApi
{
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
        
        apiGroup.MapPost("/short/{url}", async Task<Results<BadRequest, Created>>(IGrainFactory grain,
            string url,
            string expirationMinutes,
            string limit) =>
        {
            var shortenedGrain = grain.GetGrain<IUrlShortenerGrain>(url);
            var shortedUrl = await shortenedGrain.GetUrlUnShortedUrl();
            return TypedResults.Created("shortedUrl");
        });
        
        apiGroup.MapGet("/test", () => Results.Redirect("/hello"));
        
        return apiGroup;
    }
}