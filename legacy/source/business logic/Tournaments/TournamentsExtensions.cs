
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;
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

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddSingleton<Results.ICalculator, Results.Calculator>();
        services.AddTransient<Results.IBusinessLogic, Results.BusinessLogic>();

        services.AddSingleton<Seeding.ICalculator, Seeding.Calculator>();
        services.AddTransient<Seeding.IBusinessLogic, Seeding.BusinessLogic>();

        services.AddTransient<GetTournamentsQueryHandler>();
        services.AddTransient<IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>>>(provider =>
            new GetTournamentsQueryHandlerTelemetryDecorator(
                provider.GetRequiredService<GetTournamentsQueryHandler>(),
                provider.GetRequiredService<ILogger<GetTournamentsQueryHandlerTelemetryDecorator>>()));

        services.AddTransient<GetTournamentByIdQueryHandler>();
        services.AddTransient<IQueryHandler<GetTournamentByIdQuery, Models.Tournament?>>(provider =>
            new GetTournamentByIdQueryHandlerTelemetryDecorator(
                provider.GetRequiredService<GetTournamentByIdQueryHandler>(),
                provider.GetRequiredService<ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator>>()));

        return services;
    }
}
