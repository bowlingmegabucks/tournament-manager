using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Divisions;
internal static class DivisionExtensions
{
    public static IServiceCollection AddDivisionModule(this IServiceCollection services)
    {
        services.AddTransient<Add.Form>();

        services.AddTransient<Retrieve.Form>();

        return services;
    }
}
