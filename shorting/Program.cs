using MudBlazor.Services;
using shorting.Api;

var builder = WebApplication.CreateBuilder(args);

var invariant = "Npgsql";
var connectionString =
    "Host=localhost;Port=5432;Database=shorting;Username=postgres;Password=Compaq2009";

// TODO: Scripts needs to ran manually
// https://github.com/dotnet/orleans/blob/main/src/AdoNet/Orleans.Clustering.AdoNet/Migrations/PostgreSQL-Clustering-3.7.0.sql
// https://learn.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/adonet-configuration
builder.Host.UseOrleans((ctx, siloBuilder) =>
{
    /*
    if (builder.Environment.IsDevelopment())
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.AddMemoryGrainStorageAsDefault();
        siloBuilder.AddMemoryGrainStorage("urls");
        siloBuilder.UseInMemoryReminderService();
    }
    else
    {
        */
    siloBuilder.UseAdoNetClustering(options =>
        {
            options.Invariant = invariant;
            options.ConnectionString = connectionString;
        });
    siloBuilder.UseAdoNetReminderService(options =>
        {           
            options.Invariant = invariant;
            options.ConnectionString = connectionString;
        });  
    siloBuilder.AddAdoNetGrainStorage("urls",options =>
        {
            options.Invariant = invariant;
            options.ConnectionString = connectionString;
            //options.UseJsonFormat = true;
        });
        //}
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

// Add Url endpoints
app.MapUrlApi();

app.Run();