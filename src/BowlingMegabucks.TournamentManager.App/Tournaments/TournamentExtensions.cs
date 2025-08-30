using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.App.Tournaments;
internal static class TournamentExtensions
{
    public static IServiceCollection AddTournaments(this IServiceCollection services)
    {
        services.AddTransient<RetrieveTournamentsForm>();

        return services;
    }
}
