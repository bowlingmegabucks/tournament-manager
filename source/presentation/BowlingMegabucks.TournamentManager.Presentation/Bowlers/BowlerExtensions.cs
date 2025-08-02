using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers;

internal static class BowlerExtensions
{
    public static IServiceCollection AddBowlersModule(this IServiceCollection services)
    {
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();
        services.AddTransient<Search.IAdapter, Search.Adapter>();
        services.AddTransient<Update.IAdapter, Update.Adapter>();

        return services;
    }
}
