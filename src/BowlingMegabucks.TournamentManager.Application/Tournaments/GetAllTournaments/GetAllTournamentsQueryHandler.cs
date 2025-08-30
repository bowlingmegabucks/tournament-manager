using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Tournaments.GetAllTournaments;


[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class GetAllTournamentsQueryHandler
    : IOffsetPaginationQueryHandler<GetAllTournamentsQuery, TournamentSummaryDto>
{
    private readonly ITournamentQueries _tournamentQueries;

    public GetAllTournamentsQueryHandler(ITournamentQueries tournamentQueries)
    {
        _tournamentQueries = tournamentQueries;
    }

    public async Task<ErrorOr<OffsetPaginationQueryResponse<TournamentSummaryDto>>> HandleAsync(
        GetAllTournamentsQuery query,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<TournamentSummaryDto> results = await _tournamentQueries.GetAllTournamentsAsync(query, cancellationToken);
        int count = await _tournamentQueries.GetTotalTournamentCountAsync(cancellationToken);

        return new OffsetPaginationQueryResponse<TournamentSummaryDto>
        {
            TotalItems = count,
            TotalPages = (int)Math.Ceiling(count / (double)query.PageSize),
            CurrentPage = query.Page,
            PageSize = query.PageSize,
            Items = results,
        };
    }
}
