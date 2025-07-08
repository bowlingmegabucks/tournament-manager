
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Scores.Retrieve;
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

    public async Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken)
        => (await _repository.Retrieve([.. squadIds]).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(squadScore => new Models.SquadScore(squadScore));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken);
}