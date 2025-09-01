using Respawn;
using Testcontainers.MariaDb;

namespace BowlingMegabucks.TournamentManager.Tests.Infrastructure;

public sealed class DatabaseContainer
    : IAsyncLifetime
{
    private readonly MariaDbContainer _mariaDbContainer = new MariaDbBuilder()
        .WithImage("mariadb:11.4.7")
        .WithDatabase("tournament_manager")
        .WithUsername("test_user")
        .WithPassword("test_password_123!")
        .WithExposedPort(50518)
        .Build();

    public string DatabaseConnectionString
        => _mariaDbContainer.GetConnectionString();

    private Respawner? _respawner;

    /// <summary>
    /// Resets the database to a clean state by removing all data while preserving schema.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when Respawner is not initialized.</exception>
    public async Task ResetDatabaseAsync()
    {
        if (_respawner is null)
        {
            throw new InvalidOperationException("Respawner is not initialized. Call InitializeAsync first.");
        }

        using var connection = new MySqlConnector.MySqlConnection(DatabaseConnectionString);
        await connection.OpenAsync();
        await _respawner.ResetAsync(connection);
    }

    public async ValueTask InitializeAsync()
        => await _mariaDbContainer.StartAsync();

    public async Task InitializeRespawnerAsync()
    {
        using var connection = new MySqlConnector.MySqlConnection(DatabaseConnectionString);
        await connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.MySql,
            TablesToIgnore = ["__EFMigrationsHistory"], // Preserve EF migration history
            CheckTemporalTables = false,
            WithReseed = true // Reset auto-increment values
        });
    }

    public async ValueTask DisposeAsync()
    {
        await _mariaDbContainer.StopAsync();
        await _mariaDbContainer.DisposeAsync();
    }
}
