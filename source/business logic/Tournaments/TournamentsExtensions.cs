
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Tournaments;

internal static class TournamentsExtensions
{
    public static IServiceCollection AddTournamentsModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddSingleton<IValidator<Models.Tournament>, Add.Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IBusinessLogic>(provider =>
            new Retrieve.BusinessLogicDecorator(
                provider.GetRequiredService<Retrieve.BusinessLogic>(),
                provider.GetRequiredService<ILogger<Retrieve.BusinessLogicDecorator>>()));
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddSingleton<Results.ICalculator, Results.Calculator>();
        services.AddTransient<Results.IBusinessLogic, Results.BusinessLogic>();

        services.AddSingleton<Seeding.ICalculator, Seeding.Calculator>();
        services.AddTransient<Seeding.IBusinessLogic, Seeding.BusinessLogic>();

        return services;
    }
}
