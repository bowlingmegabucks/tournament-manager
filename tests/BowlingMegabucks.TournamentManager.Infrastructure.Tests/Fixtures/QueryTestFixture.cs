using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Tests.Fixtures;

/// <summary>
/// Shared test fixture for database-dependent query tests.
/// Provides optimized database setup with connection pooling and resource reuse.
/// </summary>
public sealed class QueryTestFixture
    : IAsyncLifetime
{
    private readonly DatabaseContainer _databaseContainer;

    public QueryTestFixture(DatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }

    internal ApplicationDbContext ApplicationDbContext { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        // Verify database container is ready
        if (string.IsNullOrEmpty(_databaseContainer.DatabaseConnectionString))
        {
            throw new InvalidOperationException("Database container is not initialized");
        }

        // Create optimized DbContext with connection pooling
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseMySql(
                _databaseContainer.DatabaseConnectionString,
                new MySqlServerVersion(new Version(11, 4, 7)),
                mySqlOptions => mySqlOptions
                    .EnableRetryOnFailure(maxRetryCount: 3)) // Resilience optimization
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .Options;

        ApplicationDbContext = new ApplicationDbContext(options);

        // Apply migrations efficiently - only once per test class
        await ApplicationDbContext.Database.MigrateAsync();

        // Verify database connectivity
        if (!await ApplicationDbContext.Database.CanConnectAsync())
        {
            throw new InvalidOperationException("Cannot connect to the test database after migration");
        }

        await _databaseContainer.InitializeRespawnerAsync();
    }

    public async Task ResetDatabaseAsync()
        => await _databaseContainer.ResetDatabaseAsync();

    public async ValueTask DisposeAsync()
    {
        if (ApplicationDbContext is not null)
        {
            await ApplicationDbContext.DisposeAsync();
        }

        // DatabaseContainer disposal handled by xUnit
    }
}
