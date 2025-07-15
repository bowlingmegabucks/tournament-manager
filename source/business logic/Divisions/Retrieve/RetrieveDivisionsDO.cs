using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Divisions.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Models.Division>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(division => new Models.Division(division));

    public async Task<Models.Division> ExecuteAsync(DivisionId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Division>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<Models.Division> ExecuteAsync(DivisionId id, CancellationToken cancellationToken);
}