using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Divisions.Retrieve;
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

    public async Task<IEnumerable<Models.Division>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(division=> new Models.Division(division));

    public Models.Division? Execute(NortheastMegabuck.DivisionId id)
        => new(_repository.Retrieve(id));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Division>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Models.Division? Execute(DivisionId id);
}