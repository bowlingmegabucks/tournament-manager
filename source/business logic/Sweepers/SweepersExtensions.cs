using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Sweepers;

internal static class SweepersExtensions
{
    public static IServiceCollection AddSweepersModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddSingleton<IValidator<Models.Sweeper>, Add.Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<Complete.IBusinessLogic, Complete.BusinessLogic>();
        services.AddTransient<Complete.IDataLayer, Complete.DataLayer>();

        services.AddTransient<Results.IBusinessLogic, Results.BusinessLogic>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        return services;
    }
}
