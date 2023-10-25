using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Tournaments.Retrieve;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    internal DataLayer(IConfiguration config)
    {
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRepository"></param>
    internal DataLayer(IRepository mockRepository)
    {
        _repository = mockRepository;
    }

    async Task<IEnumerable<Models.Tournament>> IDataLayer.ExecuteAsync(CancellationToken cancellationToken)
        => (await _repository.RetrieveAll().ToListAsync(cancellationToken).ConfigureAwait(false)).Select(tournament => new Models.Tournament(tournament));

    async Task<Models.Tournament> IDataLayer.ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));

    async Task<Models.Tournament> IDataLayer.ExecuteAsync(DivisionId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));

    async Task<Models.Tournament> IDataLayer.ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Tournament>> ExecuteAsync(CancellationToken cancellationToken);

    Task<Models.Tournament> ExecuteAsync(TournamentId id, CancellationToken cancellationToken);

    Task<Models.Tournament> ExecuteAsync(DivisionId id, CancellationToken cancellationToken);

    Task<Models.Tournament> ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}