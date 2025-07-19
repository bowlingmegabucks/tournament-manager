using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Divisions;
internal static class DivisionsExtensions
{
    internal static IServiceCollection AddDivisionsModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddSingleton<IValidator<Models.Division>, Add.Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        return services;
    }
}
