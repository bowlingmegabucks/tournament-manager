using System.Diagnostics;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Registrations.CreateRegistration;

internal sealed class CreateRegistrationCommandHandlerTelemetryDecorator
    : ICommandHandler<CreateRegistrationCommand, RegistrationId>
{
    private readonly ICommandHandler<CreateRegistrationCommand, RegistrationId> _innerHandler;
    private readonly ILogger<CreateRegistrationCommandHandlerTelemetryDecorator> _logger;

    public CreateRegistrationCommandHandlerTelemetryDecorator(ICommandHandler<CreateRegistrationCommand, RegistrationId> innerHandler, ILogger<CreateRegistrationCommandHandlerTelemetryDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<RegistrationId>> HandleAsync(CreateRegistrationCommand command, CancellationToken cancellationToken)
    {
        using var activity = RegistrationsTelemetry._activity.StartActivity("Create Registration", ActivityKind.Internal);

        _logger.CreatingRegistration(command.Bowler.USBCId, command.TournamentId);

        try
        {
            activity?.SetTag("tournament.id", command.TournamentId.Value);
            activity?.SetTag("bowler.usbcId", command.Bowler.USBCId);

            var result = await _innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorCreatingRegistration(result.Errors);

                activity?.SetStatus(ActivityStatusCode.Error);
                activity?.SetTag("error", true);
                activity?.SetTag("error.message", string.Join(", ", result.Errors.Select(e => e.Description)));

                return result;
            }

            _logger.RegistrationCreated(result.Value);
            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorCreatingRegistration(ex);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("exception", ex.ToString());

            return Error.Failure("CreateRegistrationCommandHandler.Exception", ex.Message);
        }
        finally
        {
            _logger.ExecutedCreateRegistration(command.Bowler.USBCId, command.TournamentId);
        }
    }
}

internal static partial class CreateRegistrationLogMessages
{
    [LoggerMessage(LogLevel.Information, "Creating registration for bowler with UsbcId {UsbcId} in tournament {TournamentId}")]
    public static partial void CreatingRegistration(this ILogger logger, string usbcId, TournamentId tournamentId);

    [LoggerMessage(LogLevel.Error, "Error creating registration: {@Errors}")]
    public static partial void ErrorCreatingRegistration(this ILogger logger, IEnumerable<Error> errors);

    [LoggerMessage(LogLevel.Error, "Exception creating registration")]
    public static partial void ErrorCreatingRegistration(this ILogger logger, Exception ex);

    [LoggerMessage(LogLevel.Information, "Registration created successfully: {RegistrationId}")]
    public static partial void RegistrationCreated(this ILogger logger, RegistrationId registrationId);

    [LoggerMessage(LogLevel.Information, "Executed create registration for bowler {UsbcId} in tournament {TournamentId}")]
    public static partial void ExecutedCreateRegistration(this ILogger logger, string usbcId, TournamentId tournamentId);
}