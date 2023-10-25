using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.LaneAssignments.Retrieve;
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

    async Task<IEnumerable<Models.LaneAssignment>> IDataLayer.ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(squadId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(squadRegistration => new Models.LaneAssignment(squadRegistration));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.LaneAssignment>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}