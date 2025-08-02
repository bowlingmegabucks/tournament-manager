using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers;
internal static class SweepersExtensions
{
    public static IServiceCollection AddSweepersModule(this IServiceCollection services)
    {
        services.AddTransient<Add.Form>();

        services.AddTransient<Portal.Form>();

        services.AddTransient<Results.Form>();

        services.AddTransient<Retrieve.Form>();

        return services;
    }
}
