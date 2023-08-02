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

    Models.Tournament IDataLayer.Execute(TournamentId id)
        => new(_repository.Retrieve(id));

    Models.Tournament IDataLayer.Execute(DivisionId id)
        => new(_repository.Retrieve(id));

    Models.Tournament IDataLayer.Execute(SquadId id)
        => new(_repository.Retrieve(id));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Tournament>> ExecuteAsync(CancellationToken cancellationToken);

    Models.Tournament Execute(TournamentId id);

    Models.Tournament Execute(DivisionId id);

    Models.Tournament Execute(SquadId id);
}