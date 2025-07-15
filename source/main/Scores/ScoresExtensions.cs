using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Scores;

internal static class ScoresExtensions
{
    public static IServiceCollection AddScoresModule(this IServiceCollection services)
    {
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        services.AddTransient<Update.IAdapter, Update.Adapter>();

        services.AddTransient<RecapSheetForm>();
        services.AddTransient<Form>();

        return services;
    }
}
