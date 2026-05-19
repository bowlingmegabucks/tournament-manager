using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BowlingMegabucks.TournamentManager.Bowlers.Add;

namespace BowlingMegabucks.TournamentManager.Bowlers;
internal static class BowlersExtensions
{
    public static IServiceCollection AddBowlersModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();
        services.AddSingleton<IValidator<Models.PersonName>, PersonNameValidator>();

        services.AddSingleton<IAddBowlerValidator, Add.Validator>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddTransient<Search.IBusinessLogic, Search.BusinessLogic>();
        services.AddTransient<Search.IDataLayer, Search.DataLayer>();

        services.AddSingleton<Update.IUpdateBowlerValidator, Update.Validator>();
        services.AddTransient<Update.IBusinessLogic, Update.BusinessLogic>();
        services.AddTransient<Update.IDataLayer, Update.DataLayer>();

        return services;
    }
}
