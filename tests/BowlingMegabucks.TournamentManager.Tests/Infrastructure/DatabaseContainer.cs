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

    public async ValueTask InitializeAsync()
        => await _mariaDbContainer.StartAsync();

    public async ValueTask DisposeAsync()
    {
        await _mariaDbContainer.StopAsync();
        await _mariaDbContainer.DisposeAsync();
    }
}
