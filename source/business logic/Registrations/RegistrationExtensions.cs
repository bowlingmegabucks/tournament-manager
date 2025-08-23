
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Registrations.CreateRegistration;
using BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;
using BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;
using ErrorOr;
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
        services.AddSingleton<IPaymentEntityMapper, PaymentEntityMapper>();

        services.AddSingleton<IValidator<Models.Registration>, Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<CreateRegistrationCommandHandler>();
        services.AddTransient<ICommandHandler<CreateRegistrationCommand, RegistrationId>>(provider =>
            new CreateRegistrationCommandHandlerTelemetryDecorator(
                provider.GetRequiredService<CreateRegistrationCommandHandler>(),
                provider.GetRequiredService<ILogger<CreateRegistrationCommandHandlerTelemetryDecorator>>()));

        services.AddTransient<DeleteRegistrationCommandHandler>();
        services.AddTransient<ICommandHandler<DeleteRegistrationCommand, Deleted>>(provider =>
            new DeleteRegistrationCommandHandlerTelemetryDecorator(
                provider.GetRequiredService<DeleteRegistrationCommandHandler>(),
                provider.GetRequiredService<ILogger<DeleteRegistrationCommandHandlerTelemetryDecorator>>()));

        services.AddTransient<GetRegistrationByIdQueryHandler>();
        services.AddTransient<IQueryHandler<GetRegistrationByIdQuery, Models.Registration?>>(provider =>
            new GetRegistrationByIdQueryHandlerTelemetryDecorator(
                provider.GetRequiredService<GetRegistrationByIdQueryHandler>(),
                provider.GetRequiredService<ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator>>()));
        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddSingleton<IValidator<UpdateRegistration.UpdateRegistrationRecord>, UpdateRegistration.Validator>();
        services.AddTransient<UpdateRegistration.UpdateRegistrationCommandHandler>();
        services.AddTransient<ICommandHandler<UpdateRegistration.UpdateRegistrationCommand, Updated>>(provider =>
            new UpdateRegistration.UpdateRegistrationCommandHandlerTelemetryDecorator(
                provider.GetRequiredService<UpdateRegistration.UpdateRegistrationCommandHandler>(),
                provider.GetRequiredService<ILogger<UpdateRegistration.UpdateRegistrationCommandHandlerTelemetryDecorator>>()));

        services.AddTransient<AppendRegistration.AppendRegistrationCommandHandler>();

        services.AddSingleton<IValidator<Update.UpdateRegistrationModel>, Update.Validator>();
        services.AddTransient<Update.IBusinessLogic, Update.BusinessLogic>();
        services.AddTransient<Update.IDataLayer, Update.DataLayer>();

        return services;
    }
}
