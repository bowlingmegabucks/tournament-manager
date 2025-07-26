using BowlingMegabucks.TournamentManager.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Testcontainers.MariaDb;

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

public class ApiTestFixture
    : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MariaDbContainer _mariaDbContainer = new MariaDbBuilder()
        .WithImage("mariadb:11.4.7")
        .WithDatabase("bowlingmegabucks")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => Environment.SetEnvironmentVariable("ConnectionStrings:Default", _mariaDbContainer.GetConnectionString());

    public async ValueTask InitializeAsync()
        => await _mariaDbContainer.StartAsync();

    public new async Task DisposeAsync()
    {
        await _mariaDbContainer.DisposeAsync();

        await base.DisposeAsync();
    }
}