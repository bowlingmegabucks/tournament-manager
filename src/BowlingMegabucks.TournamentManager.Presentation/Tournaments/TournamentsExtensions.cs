using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments;

internal static class TournamentsExtensions
{
    public static IServiceCollection AddTournamentsPresentation(this IServiceCollection services)
    {
        services.AddSingleton<IGetTournamentsAdapter, GetTournamentsAdapter>();
        services.AddTransient<GetTournamentsPresenter>();

        services.AddSingleton<IGetTournamentByIdAdapter, GetTournamentByIdAdapter>();
        services.AddTransient<GetTournamentByIdPresenter>();

        return services;
    }
}
