using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;

internal sealed class GetTournamentsQueryHandlerTelemetryDecorator
    : IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>>
{
    private readonly IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> _innerHandler;
    private readonly ILogger<GetTournamentsQueryHandlerTelemetryDecorator> _logger;

    public GetTournamentsQueryHandlerTelemetryDecorator(
        IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> innerHandler,
        ILogger<GetTournamentsQueryHandlerTelemetryDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<IEnumerable<Models.Tournament>>> HandleAsync(GetTournamentsQuery query, CancellationToken cancellationToken)
    {
        using var activity = TournamentsTelemetry._activity.StartActivity("RetrieveTournaments", ActivityKind.Internal);

        try
        {
            _logger.RetrievingTournaments();

            var result = await _innerHandler.HandleAsync(query, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorRetrievingTournaments(result.Errors);
                activity?.SetStatus(ActivityStatusCode.Error);
                activity?.SetTag("error", true);
                activity?.SetTag("error.message", string.Join(",", result.Errors.Select(e => e.Description)));
            }
            else
            {
                var tournamentCount = result.Value.Count();

                _logger.TournamentsRetrieved(tournamentCount);
                activity?.SetTag("tournament.count", tournamentCount);

            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorRetrievingTournaments(ex);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("exception", ex.ToString());

            return Error.Failure("GetTournamentsQueryHandler.Exception", ex.Message);
        }
        finally
        {
            _logger.ExecutedRetrieveTournaments();
        }
    }
}

internal static partial class GetTournamentsLogMessages
{
    [LoggerMessage(Level = LogLevel.Trace, Message = "Executing business logic for retrieving tournaments.", EventName = "RetrieveTournaments")]
    public static partial void RetrievingTournaments(this ILogger<GetTournamentsQueryHandlerTelemetryDecorator> logger);

    // need to bring in serilog
    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournaments: {@Errors}", EventName = "ErrorRetrievingTournaments")]
    public static partial void ErrorRetrievingTournaments(this ILogger<GetTournamentsQueryHandlerTelemetryDecorator> logger, IEnumerable<Error> errors);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournaments", EventName = "ExceptionRetrievingTournaments")]
    public static partial void ErrorRetrievingTournaments(this ILogger<GetTournamentsQueryHandlerTelemetryDecorator> logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Information, Message = "Retrieved {Count} tournaments successfully.", EventName = "TournamentsRetrieved")]
    public static partial void TournamentsRetrieved(this ILogger<GetTournamentsQueryHandlerTelemetryDecorator> logger, int count);

    [LoggerMessage(Level = LogLevel.Trace, Message = "Executed business logic for retrieving tournaments.", EventName = "ExecutedRetrieveTournaments")]
    public static partial void ExecutedRetrieveTournaments(this ILogger<GetTournamentsQueryHandlerTelemetryDecorator> logger);
}