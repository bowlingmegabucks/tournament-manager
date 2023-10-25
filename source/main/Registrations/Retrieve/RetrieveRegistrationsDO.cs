
using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Registrations.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IConfiguration config)
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

    async Task<IEnumerable<Models.Registration>> IDataLayer.ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(registration => new Models.Registration(registration));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Registration>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}