using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using BowlingMegabucks.TournamentManager.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Tests.Fixtures;

public abstract class QueryTestFixture
    : IClassFixture<DatabaseContainer>, IAsyncLifetime
{
    private readonly DatabaseContainer _databaseContainer;
    internal ApplicationDbContext _applicationDbContext = null!;

    protected QueryTestFixture(DatabaseContainer databaseContainer)
    {
        ArgumentNullException.ThrowIfNull(databaseContainer);

        _databaseContainer = databaseContainer;
    }

    public async ValueTask InitializeAsync()
    {
        await _databaseContainer.InitializeAsync();

        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseMySql(
                _databaseContainer.DatabaseConnectionString,
                new MySqlServerVersion(new Version(11, 4, 7)))
                .Options;

        _applicationDbContext = new ApplicationDbContext(options);

        await _applicationDbContext.Database.MigrateAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _applicationDbContext.DisposeAsync();
        await _databaseContainer.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}
