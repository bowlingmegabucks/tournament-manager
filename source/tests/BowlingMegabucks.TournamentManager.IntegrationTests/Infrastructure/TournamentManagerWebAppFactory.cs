using BowlingMegabucks.TournamentManager.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MariaDb;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;

public sealed class TournamentManagerWebAppFactory
    : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    internal const string _apiKey = "Integration";

    private readonly MariaDbContainer _mariaDbContainer = new MariaDbBuilder()
        .WithImage("mariadb:11.4.7")
        .WithDatabase("bowlingmegabucks")
        .WithUsername("root")
        .WithPassword("password")
        .Build();

    internal string DatabaseConnectionString
        => _mariaDbContainer.GetConnectionString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Default", _mariaDbContainer.GetConnectionString());
        builder.UseSetting("Authentication:ApiKey", _apiKey);
    }

    public async ValueTask InitializeAsync()
        => await _mariaDbContainer.StartAsync();

    public new async ValueTask DisposeAsync()
    { 
        await _mariaDbContainer.StopAsync();
        await _mariaDbContainer.DisposeAsync();

        await base.DisposeAsync();
    }
}