using BowlingMegabucks.TournamentManager.IntegrationTests;
using Testcontainers.MariaDb;

[assembly: AssemblyFixture(typeof(DatabaseFixture))]

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

public sealed class DatabaseFixture
    : IAsyncLifetime
{
    private readonly MariaDbContainer _mariaDbContainer = new MariaDbBuilder()
        .WithImage("mariadb:11.4.7")
        .WithDatabase("bowlingmegabucks")
        .Build();

    public string ConnectionString
        => _mariaDbContainer.GetConnectionString();

    public async ValueTask InitializeAsync()
        => await _mariaDbContainer.StartAsync();

    public async ValueTask DisposeAsync()
        => await _mariaDbContainer.DisposeAsync();
}