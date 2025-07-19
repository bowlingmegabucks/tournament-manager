using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Squads;
internal static class SquadsExtensionMethods
{
    public static IServiceCollection AddSquadsModule(this IServiceCollection services)
    {
        services.AddSingleton<IHandicapCalculator, HandicapCalculator>();
        services.AddSingleton<IHandicapCalculatorInternal, HandicapCalculator>();

        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddSingleton<IValidator<Models.Squad>, Add.Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<Complete.IBusinessLogic, Complete.BusinessLogic>();
        services.AddTransient<Complete.IDataLayer, Complete.DataLayer>();

        services.AddSingleton<Results.ICalculator, Results.Calculator>();
        services.AddTransient<Results.IBusinessLogic, Results.BusinessLogic>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        return services;
    }
}
