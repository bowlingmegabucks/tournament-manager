using BowlingMegabucks.TournamentManager.Bowlers;
using BowlingMegabucks.TournamentManager.Divisions;
using BowlingMegabucks.TournamentManager.LaneAssignments;
using BowlingMegabucks.TournamentManager.Registrations;
using BowlingMegabucks.TournamentManager.Scores;
using BowlingMegabucks.TournamentManager.Squads;
using BowlingMegabucks.TournamentManager.Sweepers;
using BowlingMegabucks.TournamentManager.Tournaments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager;

/// <summary>
/// Provides extension methods for registering presentation layer services in the dependency injection container.
/// </summary>
public static class PresentationExtensions
{
    /// <summary>
    /// Registers all presentation layer modules and their dependencies into the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the presentation modules will be added.</param>
    /// <param name="config">The application configuration used for module setup.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with presentation modules registered.</returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
    {
        services.AddBusinessLogic(config)
            .AddBowlersModule()
            .AddDivisionModule()
            .AddLaneAssignmentsModule()
            .AddRegistrationsModule()
            .AddScoresModule()
            .AddSquadsModule()
            .AddSweepersModule()
            .AddTournamentsModule();

        return services;
    }
}
