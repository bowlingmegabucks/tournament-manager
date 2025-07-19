using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BowlingMegabucks.TournamentManager.Bowlers;
using BowlingMegabucks.TournamentManager.Database;
using BowlingMegabucks.TournamentManager.Divisions;
using BowlingMegabucks.TournamentManager.LaneAssignments;
using BowlingMegabucks.TournamentManager.Registrations;
using BowlingMegabucks.TournamentManager.Scores;
using BowlingMegabucks.TournamentManager.Squads;
using BowlingMegabucks.TournamentManager.Sweepers;
using BowlingMegabucks.TournamentManager.Tournaments;

namespace BowlingMegabucks.TournamentManager;

/// <summary>
/// 
/// </summary>
public static class BusinessLogicExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration config)
    {
        services.AddDatabase(config)
            .AddBowlersModule()
            .AddDivisionsModule()
            .AddLaneAssignmentModule()
            .AddRegistrationModule()
            .AddScoresModule()
            .AddSquadsModule()
            .AddSweepersModule()
            .AddTournamentsModule();

        return services;
    }
}
