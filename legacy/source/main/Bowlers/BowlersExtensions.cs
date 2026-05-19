using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers;
internal static class BowlersExtensions
{
    public static IServiceCollection AddBowlersModule(this IServiceCollection services)
    {
        services.AddTransient<Search.Dialog>();

        services.AddTransient<Update.NameForm>();
        services.AddTransient<Update.UpdateForm>();

        return services;
    }
}
