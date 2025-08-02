using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Tournaments;
internal static class TournamentsExtensions
{
    public static IServiceCollection AddTournamentsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();

        services.AddTransient<Results.IAdapter, Results.Adapter>();

        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        services.AddTransient<Seeding.IAdapter, Seeding.Adapter>();

        return services;
    }
}
