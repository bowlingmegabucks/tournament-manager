using System.Diagnostics;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;

internal sealed class GetRegistrationByIdQueryHandlerTelemetryDecorator
    : IQueryHandler<GetRegistrationByIdQuery, Models.Registration?>
{
    private readonly IQueryHandler<GetRegistrationByIdQuery, Models.Registration?> _innerHandler;
    private readonly ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> _logger;

    public GetRegistrationByIdQueryHandlerTelemetryDecorator(IQueryHandler<GetRegistrationByIdQuery, Models.Registration?> innerHandler, ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<Models.Registration?>> HandleAsync(GetRegistrationByIdQuery query, CancellationToken cancellationToken)
    {
        using var activity = RegistrationsTelemetry._activity.StartActivity("Retrieve Registration by ID", ActivityKind.Internal);

        _logger.RetrievingRegistration(query.Id);

        try
        {
            activity?.SetTag("registration.id", query.Id.Value);

            var result = await _innerHandler.HandleAsync(query, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorRetrievingRegistration(result.Errors);

                activity?.SetStatus(ActivityStatusCode.Error);
                activity?.SetTag("error", true);
                activity?.SetTag("error.message", string.Join(", ", result.Errors.Select(e => e.Description)));

                return result;
            }

            _logger.RegistrationRetrieved();
            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorRetrievingRegistration(ex);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("exception", ex.ToString());

            return Error.Failure("GetRegistrationByIdQueryHandler.Exception", ex.Message);
        }
        finally
        {
            _logger.ExecutedRetrieveRegistrationById();
        }
    }
}

internal static partial class GetRegistrationByIdLogMessages
{
    [LoggerMessage(Level = LogLevel.Debug, Message = "Executing business logic for retrieving registration {Id}.", EventName = "RetrieveRegistrationById")]
    public static partial void RetrievingRegistration(this ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> logger, RegistrationId id);

    [LoggerMessage(Level = LogLevel.Information, Message = "Error retrieving Registration: {@Errors}", EventName = "ErrorRetrievingRegistration")]
    public static partial void ErrorRetrievingRegistration(this ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> logger, IEnumerable<Error> errors);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving registration", EventName = "ExceptionRetrievingRegistration")]
    public static partial void ErrorRetrievingRegistration(this ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Information, Message = "Retrieved registration successfully.", EventName = "RegistrationRetrieved")]
    public static partial void RegistrationRetrieved(this ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> logger);

    [LoggerMessage(Level = LogLevel.Warning, Message = "No registration found for ID {Id}.", EventName = "RegistrationNotFound")]
    public static partial void RegistrationNotFound(this ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> logger, RegistrationId id);

    [LoggerMessage(Level = LogLevel.Trace, Message = "Executed business logic for retrieving registration by id.", EventName = "ExecutedRetrieveRegistrationById")]
    public static partial void ExecutedRetrieveRegistrationById(this ILogger<GetRegistrationByIdQueryHandlerTelemetryDecorator> logger);
}