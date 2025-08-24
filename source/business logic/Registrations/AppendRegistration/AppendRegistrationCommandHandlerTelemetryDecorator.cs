using System.Diagnostics;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Registrations.AppendRegistration;

internal sealed class AppendRegistrationCommandHandlerTelemetryDecorator
    : ICommandHandler<AppendRegistrationCommand, RegistrationId>
{
    private readonly ICommandHandler<AppendRegistrationCommand, RegistrationId> _innerHandler;
    private readonly ILogger<AppendRegistrationCommandHandlerTelemetryDecorator> _logger;

    public AppendRegistrationCommandHandlerTelemetryDecorator(ICommandHandler<AppendRegistrationCommand, RegistrationId> innerHandler, ILogger<AppendRegistrationCommandHandlerTelemetryDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<RegistrationId>> HandleAsync(AppendRegistrationCommand command, CancellationToken cancellationToken)
    {
        using var activity = RegistrationsTelemetry._activity.StartActivity("Append Registration", ActivityKind.Internal);

        _logger.AppendingRegistration(command.Bowler.USBCId, command.TournamentId);

        try
        {
            activity?.SetTag("tournament.id", command.TournamentId.Value);
            activity?.SetTag("bowler.usbcId", command.Bowler.USBCId);

            var result = await _innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorAppendingRegistration(result.Errors);

                activity?.SetStatus(ActivityStatusCode.Error);
                activity?.SetTag("error", true);
                activity?.SetTag("error.message", string.Join(", ", result.Errors.Select(e => e.Description)));

                return result;
            }

            _logger.RegistrationAppended(result.Value);
            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorAppendingRegistration(ex);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("exception", ex.ToString());

            return Error.Failure("AppendRegistrationCommandHandler.Exception", ex.Message);
        }
        finally
        {
            _logger.ExecutedAppendRegistration(command.Bowler.USBCId, command.TournamentId);
        }
    }
}

internal static partial class AppendRegistrationLogMessages
{
    [LoggerMessage(LogLevel.Information, "Appending registration for bowler with UsbcId {UsbcId} in tournament {TournamentId}")]
    public static partial void AppendingRegistration(this ILogger logger, string usbcId, TournamentId tournamentId);

    [LoggerMessage(LogLevel.Information, "Error appending registration: {@Errors}")]
    public static partial void ErrorAppendingRegistration(this ILogger logger, IEnumerable<Error> errors);

    [LoggerMessage(LogLevel.Error, "Exception appending registration")]
    public static partial void ErrorAppendingRegistration(this ILogger logger, Exception ex);

    [LoggerMessage(LogLevel.Information, "Registration appended successfully: {RegistrationId}")]
    public static partial void RegistrationAppended(this ILogger logger, RegistrationId registrationId);

    [LoggerMessage(LogLevel.Information, "Executed append registration for bowler {UsbcId} in tournament {TournamentId}")]
    public static partial void ExecutedAppendRegistration(this ILogger logger, string usbcId, TournamentId tournamentId);
}