using BowlingMegabucks.TournamentManager.Database;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

public abstract class BaseIntegrationTests
    : IClassFixture<ApiFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    internal readonly DataContext _dbContext;

    protected HttpClient HttpClient { get; }

    protected Func<Task> ResetDatabaseAsync { get; }

    protected BaseIntegrationTests(ApiFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<DataContext>();
        HttpClient = factory.HttpClient;

        ResetDatabaseAsync = factory.ResetDatabaseAsync;
    }

    public ValueTask InitializeAsync() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        _scope.Dispose();
        await _dbContext.DisposeAsync();
        
        GC.SuppressFinalize(this);
        await ResetDatabaseAsync();
    }
}