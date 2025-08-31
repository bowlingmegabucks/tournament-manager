using System;
using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournaments;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Application;

/// <summary>
/// Extension methods for registering application services.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    /// Registers application services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IOffsetPaginationQueryHandler<GetTournamentsQuery, TournamentSummaryDto>, GetTournamentsQueryHandler>();
        services.AddScoped<IQueryHandler<GetTournamentByIdQuery, TournamentDetailDto?>, GetTournamentByIdQueryHandler>();

        return services;
    }
}
