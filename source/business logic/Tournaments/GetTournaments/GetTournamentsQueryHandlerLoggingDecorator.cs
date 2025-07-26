using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;

internal sealed class GetTournamentsQueryHandlerLoggingDecorator
    : IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>>
{
    private readonly IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> _innerHandler;
    private readonly ILogger<GetTournamentsQueryHandlerLoggingDecorator> _logger;

    public GetTournamentsQueryHandlerLoggingDecorator(
        IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> innerHandler,
        ILogger<GetTournamentsQueryHandlerLoggingDecorator> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<ErrorOr<IEnumerable<Models.Tournament>>> HandleAsync(GetTournamentsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            _logger.RetrievingTournaments();

            var result = await _innerHandler.HandleAsync(query, cancellationToken);

            if (result.IsError)
            {
                _logger.ErrorRetrievingTournaments(result.Errors);
            }
            else
            {
                _logger.TournamentsRetrieved(result.Value.Count());
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.ErrorRetrievingTournaments(ex);

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
    public static partial void RetrievingTournaments(this ILogger<GetTournamentsQueryHandlerLoggingDecorator> logger);

    // need to bring in serilog
    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournaments: {@Errors}", EventName = "ErrorRetrievingTournaments")]
    public static partial void ErrorRetrievingTournaments(this ILogger<GetTournamentsQueryHandlerLoggingDecorator> logger, IEnumerable<Error> errors);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournaments", EventName = "ExceptionRetrievingTournaments")]
    public static partial void ErrorRetrievingTournaments(this ILogger<GetTournamentsQueryHandlerLoggingDecorator> logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Information, Message = "Retrieved {Count} tournaments successfully.", EventName = "TournamentsRetrieved")]
    public static partial void TournamentsRetrieved(this ILogger<GetTournamentsQueryHandlerLoggingDecorator> logger, int count);

    [LoggerMessage(Level = LogLevel.Trace, Message = "Executed business logic for retrieving tournaments.", EventName = "ExecutedRetrieveTournaments")]
    public static partial void ExecutedRetrieveTournaments(this ILogger<GetTournamentsQueryHandlerLoggingDecorator> logger);
}