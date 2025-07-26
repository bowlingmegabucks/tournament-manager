using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;

internal sealed class GetTournamentsQueryHandler
    : IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>>
{
    private readonly IRepository _repository;

    public GetTournamentsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<IEnumerable<Models.Tournament>>> HandleAsync(GetTournamentsQuery query, CancellationToken cancellationToken)
    {
        var tournaments = _repository.RetrieveAll();

        return (await tournaments.ToListAsync(cancellationToken))
                .Select(tournament => new Models.Tournament(tournament))
                .ToErrorOr();
    }
}