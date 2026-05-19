using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Squads;
internal static class SquadsExtensions
{
    public static IServiceCollection AddSquadsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();

        services.AddTransient<Complete.IAdapter, Complete.Adapter>();

        services.AddTransient<Results.IAdapter, Results.Adapter>();

        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        return services;
    }
}
