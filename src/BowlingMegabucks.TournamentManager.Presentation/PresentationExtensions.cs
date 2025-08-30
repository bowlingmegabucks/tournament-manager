using BowlingMegabucks.TournamentManager.Presentation.Services;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace BowlingMegabucks.TournamentManager.Presentation;

public static class PresentationExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddRefitClient<ITournamentManagerApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["TournamentManagerApi:BaseUrl"] ?? throw new InvalidOperationException("BaseUrl is not configured.")));

        services
            .AddTournamentsPresentation();

        return services;
    }

    private static IServiceCollection AddTournamentsPresentation(this IServiceCollection services)
    {
        services.AddSingleton<IGetTournamentsAdapter, GetTournamentsAdapter>();

        return services;
    }
}
