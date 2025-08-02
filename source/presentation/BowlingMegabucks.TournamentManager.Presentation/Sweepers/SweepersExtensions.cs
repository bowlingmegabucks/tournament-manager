using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers;
internal static class SweepersExtensions
{
    public static IServiceCollection AddSweepersModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();

        services.AddTransient<Complete.IAdapter, Complete.Adapter>();

        services.AddTransient<Results.IAdapter, Results.Adapter>();

        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        return services;
    }
}
