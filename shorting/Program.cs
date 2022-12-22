using MudBlazor.Services;
using Orleans.Configuration;
using shorting.Api;

var builder = WebApplication.CreateBuilder(args);

var invariant = "Npgsql";
var connectionString =
    "Host=shortingdb;Port=5432;Database=shorting-db;Username=postgres;Password=Compaq2009";

builder.Host.UseOrleans((ctx, siloBuilder) =>
{
    if (builder.Environment.IsDevelopment())
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.AddMemoryGrainStorageAsDefault();
        siloBuilder.AddMemoryGrainStorage("urls");
        siloBuilder.UseInMemoryReminderService();
    }
    else
    {
        siloBuilder.ConfigureEndpoints(11111, 30000);
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
        siloBuilder.AddAdoNetGrainStorage("urls", options =>
        {
            options.Invariant = invariant;
            options.ConnectionString = connectionString;
            //options.UseJsonFormat = true;
        });

        siloBuilder.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "shortingCluster";
            options.ServiceId = "shortingService";
        });
    }
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
        options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    });
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