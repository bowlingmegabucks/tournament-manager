using BowlingMegabucks.TournamentManager.Database;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.IntegrationTests;

public abstract class BaseIntegrationTests
    : IClassFixture<ApiFactory>
{
    private readonly IServiceScope _scope;
    internal readonly DataContext _dbContext;

    protected BaseIntegrationTests(ApiFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<DataContext>();
    }
}