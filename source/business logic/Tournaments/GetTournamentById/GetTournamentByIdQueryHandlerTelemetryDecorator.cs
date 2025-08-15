using System.Diagnostics;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

internal sealed class GetTournamentByIdQueryHandlerTelemetryDecorator
    : IQueryHandler<GetTournamentByIdQuery, Models.Tournament?>
{
    private readonly IQueryHandler<GetTournamentByIdQuery, Models.Tournament?> _innerHandler;
    private readonly ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> _logger;

    public GetTournamentByIdQueryHandlerTelemetryDecorator(IQueryHandler<GetTournamentByIdQuery, Models.Tournament?> innerHandler, ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<Models.Tournament?>> HandleAsync(GetTournamentByIdQuery query, CancellationToken cancellationToken)
    {
        using var activity = TournamentsTelemetry._activity.StartActivity("Retrieve Tournament by ID", ActivityKind.Internal);

        _logger.RetrievingTournament(query.Id);

        try
        {
            activity?.SetTag("tournament.id", query.Id.Value);

            var result = await _innerHandler.HandleAsync(query, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorRetrievingTournament(result.Errors);

                activity?.SetStatus(ActivityStatusCode.Error);
                activity?.SetTag("error", true);
                activity?.SetTag("error.message", string.Join(", ", result.Errors.Select(e => e.Description)));

                return result;
            }

            _logger.TournamentRetrieved();
            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorRetrievingTournament(ex);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("exception", ex.ToString());

            return Error.Failure("GetTournamentByIdQueryHandler.Exception", ex.Message);
        }
        finally
        {
            _logger.ExecutedRetrieveTournamentById();
        }
    }
}

internal static partial class GetTournamentByIdLogMessages
{
    [LoggerMessage(Level = LogLevel.Debug, Message = "Executing business logic for retrieving tournament {Id}.", EventName = "RetrieveTournamentById")]
    public static partial void RetrievingTournament(this ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> logger, TournamentId id);

    // need to bring in serilog
    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournament: {@Errors}", EventName = "ErrorRetrievingTournament")]
    public static partial void ErrorRetrievingTournament(this ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> logger, IEnumerable<Error> errors);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournament", EventName = "ExceptionRetrievingTournament")]
    public static partial void ErrorRetrievingTournament(this ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Information, Message = "Retrieved tournament successfully.", EventName = "TournamentRetrieved")]
    public static partial void TournamentRetrieved(this ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> logger);

    [LoggerMessage(Level = LogLevel.Warning, Message = "No tournament found for ID {Id}.", EventName = "TournamentNotFound")]
    public static partial void TournamentNotFound(this ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> logger, TournamentId id);

    [LoggerMessage(Level = LogLevel.Trace, Message = "Executed business logic for retrieving tournament by id.", EventName = "ExecutedRetrieveTournamentById")]
    public static partial void ExecutedRetrieveTournamentById(this ILogger<GetTournamentByIdQueryHandlerTelemetryDecorator> logger);
}