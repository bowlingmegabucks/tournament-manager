
namespace NortheastMegabuck.LaneAssignments.Update;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    async Task IDataLayer.ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(squadId, bowlerId, position, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken);
}