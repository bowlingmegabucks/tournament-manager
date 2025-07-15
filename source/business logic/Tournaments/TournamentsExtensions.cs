
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Tournaments;

internal static class TournamentsExtensions
{
    public static IServiceCollection AddTournamentsModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddSingleton<IValidator<Models.Tournament>, Add.Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddSingleton<Results.ICalculator, Results.Calculator>();
        services.AddTransient<Results.IBusinessLogic, Results.BusinessLogic>();

        services.AddSingleton<Seeding.ICalculator, Seeding.Calculator>();
        services.AddTransient<Seeding.IBusinessLogic, Seeding.BusinessLogic>();

        return services;
    }
}
