using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Squads;
internal static class SquadsExtensions
{
    public static IServiceCollection AddSquadsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.Form>();

        services.AddTransient<Portal.Form>();

        services.AddTransient<Results.Form>();

        services.AddTransient<Retrieve.Form>();

        return services;
    }
}
