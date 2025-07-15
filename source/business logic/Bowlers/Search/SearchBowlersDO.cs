using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Bowlers.Search;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    async Task<IEnumerable<Models.Bowler>> IDataLayer.ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken)
    {
        var bowlers = await _repository.Search(searchCriteria).Distinct().ToListAsync(cancellationToken).ConfigureAwait(false);

        return bowlers.Select(bowler => new Models.Bowler(bowler));
    }
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Bowler>> ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken);
}
