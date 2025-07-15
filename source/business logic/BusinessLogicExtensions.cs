using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NortheastMegabuck.Bowlers;
using NortheastMegabuck.Database;
using NortheastMegabuck.Divisions;
using NortheastMegabuck.LaneAssignments;
using NortheastMegabuck.Registrations;

namespace NortheastMegabuck;

/// <summary>
/// 
/// </summary>
public static class BusinessLogicExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration config)
    {
        services.AddDatabase(config)
            .AddBowlersModule()
            .AddDivisionsModule()
            .AddLaneAssignmentModule()
            .AddRegistrationModule();

        return services;
    }
}
