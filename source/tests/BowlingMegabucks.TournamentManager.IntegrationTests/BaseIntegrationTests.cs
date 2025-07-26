using BowlingMegabucks.TournamentManager.Database;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

public abstract class BaseIntegrationTests
    : IClassFixture<ApiTestFixture>
{
    private readonly IServiceScope _scope;
    internal readonly DataContext _dbContext;
    
    protected BaseIntegrationTests(ApiTestFixture fixture)
    {
        _scope = fixture.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<DataContext>();
    }
}