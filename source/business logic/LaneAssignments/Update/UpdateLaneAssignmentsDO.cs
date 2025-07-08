
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.LaneAssignments.Update;
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

    async Task IDataLayer.ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(squadId, bowlerId, position, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken);
}