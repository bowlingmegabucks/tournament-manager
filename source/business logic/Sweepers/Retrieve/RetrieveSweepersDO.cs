using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Sweepers.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    internal DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Models.Sweeper>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(sweeper => new Models.Sweeper(sweeper));

    public async Task<Models.Sweeper> ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));

    public IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId)
        => _repository.SuperSweeperBowlers(tournamentId);
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Sweeper>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<Models.Sweeper> ExecuteAsync(SquadId id, CancellationToken cancellationToken);

    IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId);
}