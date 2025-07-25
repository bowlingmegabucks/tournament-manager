using BowlingMegabucks.TournamentManager.Models;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Tournaments.Retrieve;

internal sealed class BusinessLogicDecorator
    : IBusinessLogic
{
    private readonly IBusinessLogic _decorated;
    private readonly ILogger<BusinessLogicDecorator> _logger;

    public BusinessLogicDecorator(IBusinessLogic decorated, ILogger<BusinessLogicDecorator> logger)
    {
        _decorated = decorated;
        _logger = logger;
    }

    public ErrorDetail? ErrorDetail
        => _decorated.ErrorDetail;

    public async Task<ErrorOr<IEnumerable<Tournament>>> ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.RetrievingTournaments();

        try
        {
            var tournamentResults = await _decorated.ExecuteAsync(cancellationToken);

            if (!tournamentResults.IsError)
            {
                _logger.TournamentsRetrieved(tournamentResults.Value.Count());

                _logger.ExecutedRetrieveTournaments();
                return tournamentResults;
            }

            _logger.ErrorRetrievingTournaments(tournamentResults.Errors);

            return tournamentResults;
        }
        catch (Exception ex)
        {
            _logger.ErrorRetrievingTournaments(ex);

            return Error.Failure("RetrieveTournaments.Exception", "An error occurred while retrieving tournaments.");
        }
        finally
        {
            _logger.ExecutedRetrieveTournaments();
        }
    }
    public Task<Tournament?> ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
        => _decorated.ExecuteAsync(id, cancellationToken);

    public Task<Tournament?> ExecuteAsync(DivisionId id, CancellationToken cancellationToken)
        => _decorated.ExecuteAsync(id, cancellationToken);

    public Task<Tournament?> ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => _decorated.ExecuteAsync(id, cancellationToken);

    public Task<Tournament?> ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
        => _decorated.ExecuteAsync(id, cancellationToken);
}

internal static partial class BusinessLogicErrorMessages
{
    [LoggerMessage(Level = LogLevel.Trace, Message = "Executing business logic for retrieving tournaments.", EventName = "RetrieveTournaments")]
    public static partial void RetrievingTournaments(this ILogger<BusinessLogicDecorator> logger);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournaments: {@Errors}", EventName = "ErrorRetrievingTournaments")]
    public static partial void ErrorRetrievingTournaments(this ILogger<BusinessLogicDecorator> logger, IEnumerable<Error> errors);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error retrieving tournaments", EventName = "ExceptionRetrievingTournaments")]
    public static partial void ErrorRetrievingTournaments(this ILogger<BusinessLogicDecorator> logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Information, Message = "Retrieved {Count} tournaments successfully.", EventName = "TournamentsRetrieved")]
    public static partial void TournamentsRetrieved(this ILogger<BusinessLogicDecorator> logger, int count);

    [LoggerMessage(Level = LogLevel.Trace, Message = "Executed business logic for retrieving tournaments.", EventName = "ExecutedRetrieveTournaments")]
    public static partial void ExecutedRetrieveTournaments(this ILogger<BusinessLogicDecorator> logger);
}