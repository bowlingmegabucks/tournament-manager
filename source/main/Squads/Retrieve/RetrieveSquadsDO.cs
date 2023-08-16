using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Squads.Retrieve;
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

    public async Task<IEnumerable<Models.Squad>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(squad=> new Models.Squad(squad));

    public Models.Squad Execute(SquadId id)
        => new (_repository.Retrieve(id));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Squad>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Models.Squad Execute(SquadId id);
}