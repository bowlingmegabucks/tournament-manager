using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Sweepers.Retrieve;
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

    public async Task <IEnumerable<Models.Sweeper>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(sweeper=> new Models.Sweeper(sweeper));

    public Models.Sweeper Execute(SquadId id)
        => new(_repository.Retrieve(id));

    public IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId)
        => _repository.SuperSweeperBowlers(tournamentId);
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Sweeper>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Models.Sweeper Execute(SquadId id);

    IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId);
}