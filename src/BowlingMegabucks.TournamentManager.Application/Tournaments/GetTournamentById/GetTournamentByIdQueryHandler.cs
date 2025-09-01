using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;

internal sealed class GetTournamentByIdQueryHandler
    : IQueryHandler<GetTournamentByIdQuery, TournamentDetailDto?>
{
    private readonly ITournamentQueries _tournamentQueries;

    public GetTournamentByIdQueryHandler(ITournamentQueries tournamentQueries)
    {
        _tournamentQueries = tournamentQueries;
    }

    public async Task<ErrorOr<TournamentDetailDto?>> HandleAsync(GetTournamentByIdQuery query, CancellationToken cancellationToken)
    {
        TournamentDetailDto? tournament = await _tournamentQueries.GetTournamentAsync(query.Id, cancellationToken);

        return tournament?.ToErrorOr<TournamentDetailDto?>()
            ?? TournamentErrors.TournamentNotFound(query.Id);
    }
}
