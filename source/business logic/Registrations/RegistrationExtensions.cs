
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Registrations.Create;
using BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Registrations;

internal static class RegistrationExtensions
{
    public static IServiceCollection AddRegistrationModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddSingleton<IValidator<Models.Registration>, Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<CreateRegistrationCommandHandler>();
        services.AddTransient<ICommandHandler<CreateRegistrationCommand, RegistrationId>>(provider =>
            new CreateRegistrationCommandHandlerTelemetryDecorator(
                provider.GetRequiredService<CreateRegistrationCommandHandler>(),
                provider.GetRequiredService<ILogger<CreateRegistrationCommandHandlerTelemetryDecorator>>()));

        services.AddTransient<Delete.IBusinessLogic, Delete.BusinessLogic>();
        services.AddTransient<Delete.IDataLayer, Delete.DataLayer>();

        services.AddTransient<GetRegistrationByIdQueryHandler>();
        services.AddTransient<IQueryHandler<GetRegistrationByIdQuery, Models.Registration?>>(provider =>
            new GetRegistrationByIdQueryHandlerTelemetryDecorator(
                provider.GetRequiredService<GetRegistrationByIdQueryHandler>(),
                provider.GetRequiredService<ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator>>()));
        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddSingleton<IValidator<Update.UpdateRegistrationModel>, Update.Validator>();
        services.AddTransient<Update.IBusinessLogic, Update.BusinessLogic>();
        services.AddTransient<Update.IDataLayer, Update.DataLayer>();

        return services;
    }
}
