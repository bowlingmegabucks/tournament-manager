using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Tournaments;
internal static class TournamentsExtensions
{
    public static IServiceCollection AddTournamentsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.Form>();

        services.AddTransient<Portal.Form>();

        services.AddTransient<Results.AtLarge>();

        services.AddTransient<Retrieve.Form>();

        services.AddTransient<Seeding.Form>();

        return services;
    }
}
