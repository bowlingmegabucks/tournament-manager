
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.LaneAssignments;
internal static class LaneAssignmentsExtensions
{
    public static IServiceCollection AddLaneAssignmentsModule(this IServiceCollection services)
    {
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        services.AddTransient<Update.IAdapter, Update.Adapter>();

        services.AddSingleton<IGenerateCrossFactory, GenerateCrossFactory>();
        services.AddSingleton<ILaneAvailability, LaneAvailability>();

        return services;
    }
}
