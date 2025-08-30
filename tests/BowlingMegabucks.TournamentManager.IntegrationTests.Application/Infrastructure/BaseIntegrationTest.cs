using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Application.Infrastructure;

public abstract class BaseIntegrationTest
    : IClassFixture<TournamentManagerWebAppFactory<IApiAssemblyMarker>>, IAsyncLifetime
{
    private readonly TournamentManagerWebAppFactory<IApiAssemblyMarker> _factory;
    private readonly IServiceScope _serviceScope;
    private bool _disposed;

    protected BaseIntegrationTest(TournamentManagerWebAppFactory<IApiAssemblyMarker> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _serviceScope = factory.Services.CreateScope();
        ApplicationDbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    internal ApplicationDbContext ApplicationDbContext { get; }

    protected TService GetRequiredService<TService>()
        where TService : notnull
        => _serviceScope.ServiceProvider.GetRequiredService<TService>();

    public async ValueTask InitializeAsync()
    {
        // Reset the database state before each test
        await _factory.ResetDatabaseAsync();
    }

    public ValueTask DisposeAsync()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                ApplicationDbContext?.Dispose();
                _serviceScope?.Dispose();
            }

            _disposed = true;
        }
    }
}
