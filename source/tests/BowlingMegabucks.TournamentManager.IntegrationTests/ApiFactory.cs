using BowlingMegabucks.TournamentManager.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using System.Data.Common;
using Respawn;
using MySqlConnector;
using Respawn.Graph;

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

public class ApiFactory
    : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    internal const string _apiKey = "Integration";

    private readonly DatabaseFixture _databaseFixture;

    public ApiFactory(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
        HttpClient = CreateClient();
        _dbConnection = new MySqlConnection(_databaseFixture.ConnectionString);
    }

    private readonly DbConnection _dbConnection;
    private Respawner _respawner = null!;

    public HttpClient HttpClient { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:Default", _databaseFixture.ConnectionString);
        Environment.SetEnvironmentVariable("Authentication:ApiKey", _apiKey);
    }

    public async Task ResetDatabaseAsync()
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

    public new async ValueTask DisposeAsync()
    {
        await _dbConnection.CloseAsync();
        await _dbConnection.DisposeAsync();

        GC.SuppressFinalize(this);
        await base.DisposeAsync();
    }
}