
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NortheastMegabuck.Squads;

namespace NortheastMegabuck.Scores.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;
    private readonly IHandicapCalculatorInternal _handicapCalculator;

    public DataLayer(IConfiguration config)
    {
        _repository = new Repository(config);
        _handicapCalculator = new HandicapCalculator();
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

    public async Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken)
        => (await _repository.Retrieve([.. squadIds]).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(squadScore => new Models.SquadScore(squadScore, _handicapCalculator));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken);
}