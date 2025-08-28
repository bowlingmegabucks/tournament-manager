using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BowlingMegabucks.TournamentManager.Tests.Infrastructure;

public sealed class TournamentManagerWebAppFactory<TMarker>
    : WebApplicationFactory<TMarker>, IAsyncLifetime
    where TMarker : class
{
    private readonly DatabaseContainer _databaseContainer = new();

    public string DatabaseConnectionString
        => _databaseContainer.DatabaseConnectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.UseSetting("ConnectionStrings:TournamentManager", DatabaseConnectionString);
    }

    public async ValueTask InitializeAsync()
        => await _databaseContainer.InitializeAsync();

    public new async ValueTask DisposeAsync()
    {
        await _databaseContainer.DisposeAsync();
        await base.DisposeAsync();
    }
}
