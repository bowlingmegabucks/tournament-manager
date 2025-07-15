
using Microsoft.EntityFrameworkCore;
using NortheastMegabuck.Squads;

namespace NortheastMegabuck.Scores.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;
    private readonly IHandicapCalculatorInternal _handicapCalculator;

    internal DataLayer(IRepository repository, IHandicapCalculatorInternal handicapCalculator)
    {
        _repository = repository;
        _handicapCalculator = handicapCalculator;
    }

    public async Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken)
        => (await _repository.Retrieve([.. squadIds]).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(squadScore => new Models.SquadScore(squadScore, _handicapCalculator));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken);
}