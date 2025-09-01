using BowlingMegabucks.TournamentManager.App.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.App.Tournaments.Portal;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.App.Tournaments;
internal static class TournamentExtensions
{
    public static IServiceCollection AddTournaments(this IServiceCollection services)
    {
        services.AddTransient<IGetTournamentsFormFactory, GetTournamentsFormFactory>();
        services.AddTransient<ITournamentPortalFormFactory, TournamentPortalFormFactory>();

        return services;
    }
}
