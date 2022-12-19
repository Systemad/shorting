using MudBlazor.Services;
using shorting.Grains;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorageAsDefault();
    siloBuilder.AddMemoryGrainStorage("urls");
    //siloBuilder UseInMemoryReminderService();
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapGet("/shtn/{shortenedurl}", async (IGrainFactory grain, string shortenedurl) =>
{
    var shortenedGrain = grain.GetGrain<IUrlShortenerGrain>(shortenedurl);
    var url = await shortenedGrain.GetUrlUnShortedUrl();
    return Results.Redirect(url);
});

app.Run();