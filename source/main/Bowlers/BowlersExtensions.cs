using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Bowlers;
internal static class BowlersExtensions
{
    internal static IServiceCollection AddBowlersModule(this IServiceCollection services)
    {
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        services.AddTransient<Search.IAdapter, Search.Adapter>();
        services.AddTransient<Search.Dialog>();

        services.AddTransient<Update.IAdapter, Update.Adapter>();
        services.AddTransient<Update.NameForm>();
        services.AddTransient<Update.UpdateForm>();

        return services;
    }
}
