using BowlingMegabucks.TournamentManager.Bowlers;
using BowlingMegabucks.TournamentManager.Divisions;
using BowlingMegabucks.TournamentManager.LaneAssignments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager;

/// <summary>
/// 
/// </summary>
public static class PresentationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
    {
        services.AddBusinessLogic(config)
            .AddBowlersModule()
            .AddDivisionModule()
            .AddLaneAssignmentsModule();

        return services;
    }
}
