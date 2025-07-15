
using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Squads.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    internal DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Models.Squad>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(squad => new Models.Squad(squad));

    public async Task<Models.Squad> ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Squad>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<Models.Squad> ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}