using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest
    : IClassFixture<TournamentManagerWebAppFactory<IApiAssemblyMarker>>, IAsyncLifetime
{
    private readonly TournamentManagerWebAppFactory<IApiAssemblyMarker> _factory;
    private readonly IServiceScope _serviceScope;
    private bool _disposed;

    protected BaseFunctionalTest(TournamentManagerWebAppFactory<IApiAssemblyMarker> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _serviceScope = factory.Services.CreateScope();
        ApplicationDbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    internal ApplicationDbContext ApplicationDbContext { get; }

    protected HttpClient HttpClient
        => _factory.CreateClient();

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
