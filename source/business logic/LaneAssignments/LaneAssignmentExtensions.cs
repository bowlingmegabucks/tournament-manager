
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.LaneAssignments;

internal static class LaneAssignmentExtensions
{
    internal static IServiceCollection AddLaneAssignmentModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddTransient<Update.IBusinessLogic, Update.BusinessLogic>();
        services.AddTransient<Update.IDataLayer, Update.DataLayer>();

        return services;
    }
}
