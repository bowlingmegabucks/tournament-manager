using System.Diagnostics;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;

internal sealed class UpdateRegistrationCommandHandlerTelemetryDecorator
    : ICommandHandler<UpdateRegistrationCommand, Updated>
{
    private readonly ICommandHandler<UpdateRegistrationCommand, Updated> _innerHandler;
    private readonly ILogger<UpdateRegistrationCommandHandlerTelemetryDecorator> _logger;

    public UpdateRegistrationCommandHandlerTelemetryDecorator(ICommandHandler<UpdateRegistrationCommand, Updated> innerHandler, ILogger<UpdateRegistrationCommandHandlerTelemetryDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<Updated>> HandleAsync(UpdateRegistrationCommand command, CancellationToken cancellationToken)
    {
        using var activity = RegistrationsTelemetry._activity.StartActivity("Update Registration", ActivityKind.Internal);

        _logger.UpdatingRegistration(command.Id);

        try
        {
            activity?.SetTag("registration.id", command.Id.Value);

            var result = await _innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorUpdatingRegistration(result.Errors);

                activity?.SetStatus(ActivityStatusCode.Error);
                activity?.SetTag("error", true);
                activity?.SetTag("error.message", string.Join(", ", result.Errors.Select(e => e.Description)));

                return result;
            }

            _logger.RegistrationUpdated(command.Id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorUpdatingRegistration(ex);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("exception", ex.ToString());

            return Error.Failure("UpdateRegistrationCommandHandler.Exception", ex.Message);
        }
        finally
        {
            _logger.ExecutedUpdateRegistration(command.Id);
        }
    }
}

internal static partial class UpdateRegistrationLogMessages
{
    [LoggerMessage(LogLevel.Information, "Updating registration {RegistrationId}")]
    public static partial void UpdatingRegistration(this ILogger logger, RegistrationId registrationId);

    [LoggerMessage(LogLevel.Information, "Error updating registration: {@Errors}")]
    public static partial void ErrorUpdatingRegistration(this ILogger logger, IEnumerable<Error> errors);

    [LoggerMessage(LogLevel.Error, "Exception updating registration")]
    public static partial void ErrorUpdatingRegistration(this ILogger logger, Exception ex);

    [LoggerMessage(LogLevel.Information, "Registration updated successfully: {RegistrationId}")]
    public static partial void RegistrationUpdated(this ILogger logger, RegistrationId registrationId);

    [LoggerMessage(LogLevel.Information, "Executed update registration for {RegistrationId}")]
    public static partial void ExecutedUpdateRegistration(this ILogger logger, RegistrationId registrationId);
}