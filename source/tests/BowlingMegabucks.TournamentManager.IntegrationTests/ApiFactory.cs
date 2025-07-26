using BowlingMegabucks.TournamentManager.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

public class ApiFactory
    : WebApplicationFactory<IApiMarker>
{
    internal const string _apiKey = "Integration";

    private readonly DatabaseFixture _databaseFixture;

    public ApiFactory(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:Default", _databaseFixture.ConnectionString);
        Environment.SetEnvironmentVariable("Authentication:ApiKey", _apiKey);
    }
}