using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Divisions;
internal static class DivisionExtensions
{
    public static IServiceCollection AddDivisionModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        return services;
    }
}
