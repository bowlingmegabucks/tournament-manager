using BowlingMegabucks.TournamentManager.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MariaDb;

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

/// <summary>
/// 
/// </summary>
public class ApiTestFixture
    : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MariaDbContainer _mariaDbContainer = new MariaDbBuilder()
        .WithImage("mariadb:11.4.7")
        .WithDatabase("bowlingmegabucks")
        .Build();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask InitializeAsync()
    {
        await _mariaDbContainer.StartAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public new async Task DisposeAsync()
    {
        await _mariaDbContainer.DisposeAsync();
    }
}