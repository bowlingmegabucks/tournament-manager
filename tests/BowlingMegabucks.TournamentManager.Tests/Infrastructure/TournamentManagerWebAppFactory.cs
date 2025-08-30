using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Tests.Infrastructure;

public sealed class TournamentManagerWebAppFactory<TMarker>
    : WebApplicationFactory<TMarker>, IAsyncLifetime
    where TMarker : class
{
    private readonly DatabaseContainer _databaseContainer = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.UseSetting("ConnectionStrings:TournamentManager", _databaseContainer.DatabaseConnectionString);
    }

    public async ValueTask InitializeAsync()
    {
        await _databaseContainer.InitializeAsync();

        // Initialize database schema using DbContext from DI
        using IServiceScope scope = Services.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        // Initialize Respawner after schema is created
        await _databaseContainer.InitializeRespawnerAsync();
    }

    public new async ValueTask DisposeAsync()
    {
        await _databaseContainer.DisposeAsync();
        await base.DisposeAsync();
    }

    public async Task ResetDatabaseAsync()
        => await _databaseContainer.ResetDatabaseAsync();
}
