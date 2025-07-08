using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NortheastMegabuck.Squads;

namespace NortheastMegabuck.LaneAssignments.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;
    private readonly IHandicapCalculatorInternal _handicapCalculator;

    public DataLayer(IConfiguration config, IHandicapCalculatorInternal handicapCalculator)
    {
        _repository = new Repository(config);
        _handicapCalculator = handicapCalculator;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRepository"></param>
    /// <param name="mockHandicapCalculator"></param>
    internal DataLayer(IRepository mockRepository, IHandicapCalculatorInternal mockHandicapCalculator)
    {
        _repository = mockRepository;
        _handicapCalculator = mockHandicapCalculator;
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