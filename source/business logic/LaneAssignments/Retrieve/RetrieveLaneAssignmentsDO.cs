using Microsoft.EntityFrameworkCore;
using NortheastMegabuck.Squads;

namespace NortheastMegabuck.LaneAssignments.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;
    private readonly IHandicapCalculatorInternal _handicapCalculator;

    public DataLayer(IRepository repository, IHandicapCalculatorInternal handicapCalculator)
    {
        _repository = repository;
        _handicapCalculator = handicapCalculator;
    }

    async Task<IEnumerable<Models.LaneAssignment>> IDataLayer.ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(squadId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(squadRegistration => new Models.LaneAssignment(squadRegistration, _handicapCalculator));

    async Task<Models.LaneAssignment> IDataLayer.ExecuteAsync(SquadId squadId, BowlerId bowlerId, CancellationToken cancellationToken)
        => new Models.LaneAssignment(await _repository.RetrieveAsync(squadId, bowlerId, cancellationToken).ConfigureAwait(false), _handicapCalculator);
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.LaneAssignment>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);

    Task<Models.LaneAssignment> ExecuteAsync(SquadId squadId, BowlerId bowlerId, CancellationToken cancellationToken);
}