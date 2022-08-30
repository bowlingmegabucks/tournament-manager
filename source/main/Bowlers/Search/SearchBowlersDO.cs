
namespace NortheastMegabuck.Bowlers.Search;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    internal DataLayer(IConfiguration config)
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

    IEnumerable<Models.Bowler> IDataLayer.Execute(Models.BowlerSearchCriteria searchCriteria)
        => _repository.Search(searchCriteria).Select(bowler => new Models.Bowler(bowler));
}

internal interface IDataLayer
{
    IEnumerable<Models.Bowler> Execute(Models.BowlerSearchCriteria searchCriteria);
}
