using System.Data.Common;
using BowlingMegabucks.TournamentManager.Database;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Respawn;
using Respawn.Graph;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;

public abstract class IntegrationTestFixture
    : IClassFixture<TournamentManagerWebAppFactory>, IAsyncLifetime
{
    private readonly TournamentManagerWebAppFactory _factory;
    private readonly DbConnection _dbConnection;
    private readonly IServiceScope _scope;
    internal readonly DataContext _dbContext;

    private Respawner _respawner = null!;

    protected IntegrationTestFixture(TournamentManagerWebAppFactory factory)
    {
        _factory = factory;

        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<DataContext>();
        _dbConnection = new MySqlConnection(_factory.DatabaseConnectionString);
    }

    protected HttpClient CreateClient()
        => _factory.CreateClient();

    protected HttpClient CreateAuthenticatedClient()
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Add("x-api-key", TournamentManagerWebAppFactory._apiKey);

        return client;
    }

    protected async Task ResetDatabaseAsync()
        => await _respawner.ResetAsync(_dbConnection);

    public async ValueTask InitializeAsync()
    {
        await _dbConnection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.MySql,
            TablesToIgnore = [new Table("__EFMigrationsHistory")],
            TablesToInclude = [new Table("Tournaments")],
            WithReseed = true
        });
    }

    public async ValueTask DisposeAsync()
    {
        await _dbConnection.DisposeAsync();
        await _dbContext.DisposeAsync();

        _scope.Dispose();

        GC.SuppressFinalize(this);
    }
}