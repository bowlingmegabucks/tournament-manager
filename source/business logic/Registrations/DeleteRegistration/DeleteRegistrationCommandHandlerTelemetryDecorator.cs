using System.Diagnostics;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;

internal sealed class DeleteRegistrationCommandHandlerTelemetryDecorator
    : ICommandHandler<DeleteRegistrationCommand, Deleted>
{
    private readonly ICommandHandler<DeleteRegistrationCommand, Deleted> _innerHandler;
    private readonly ILogger<DeleteRegistrationCommandHandlerTelemetryDecorator> _logger;

    public DeleteRegistrationCommandHandlerTelemetryDecorator(
        ICommandHandler<DeleteRegistrationCommand, Deleted> innerHandler,
        ILogger<DeleteRegistrationCommandHandlerTelemetryDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<Deleted>> HandleAsync(DeleteRegistrationCommand command, CancellationToken cancellationToken)
    {
        using var activity = RegistrationsTelemetry._activity.StartActivity("Delete Registration", ActivityKind.Internal);

        _logger.DeletingRegistration(command.Id);

        try
        {
            activity?.SetTag("registration.id", command.Id);

            var result = await _innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorDeletingRegistration(result.Errors);

                activity?.SetStatus(ActivityStatusCode.Error);
                activity?.SetTag("error", true);
                activity?.SetTag("error.message", string.Join(", ", result.Errors.Select(e => e.Description)));

                return result;
            }

            _logger.RegistrationDeleted(command.Id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorDeletingRegistration(ex);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("exception", ex.ToString());

            return Error.Failure("DeleteRegistrationCommandHandler.Exception", ex.Message);
        }
        finally
        {
            _logger.ExecutedDeleteRegistration(command.Id);
        }
    }
}

internal static partial class DeleteRegistrationLogMessages
{
    [LoggerMessage(LogLevel.Information, "Deleting registration with Id {RegistrationId}")]
    public static partial void DeletingRegistration(this ILogger logger, RegistrationId registrationId);

    [LoggerMessage(LogLevel.Error, "Error deleting registration: {@Errors}")]
    public static partial void ErrorDeletingRegistration(this ILogger logger, IEnumerable<Error> errors);

    [LoggerMessage(LogLevel.Error, "Exception deleting registration")]
    public static partial void ErrorDeletingRegistration(this ILogger logger, Exception ex);

    [LoggerMessage(LogLevel.Information, "Registration deleted successfully: {RegistrationId}")]
    public static partial void RegistrationDeleted(this ILogger logger, RegistrationId registrationId);

    [LoggerMessage(LogLevel.Information, "Executed delete registration for {RegistrationId}")]
    public static partial void ExecutedDeleteRegistration(this ILogger logger, RegistrationId registrationId);
}