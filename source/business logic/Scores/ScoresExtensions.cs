using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Scores;

internal static class ScoresExtensions
{
    public static IServiceCollection AddScoresModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddSingleton<IValidator<IEnumerable<Models.SquadScore>>, Update.Validator>();
        services.AddTransient<Update.IBusinessLogic, Update.BusinessLogic>();
        services.AddTransient<Update.IDataLayer, Update.DataLayer>();

        return services;
    }
}
