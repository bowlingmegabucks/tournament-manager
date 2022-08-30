using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Bowlers;
internal class Repository : IRepository
{
    private readonly Database.IDataContext _dataContext;
    internal Repository(IConfiguration config)
    {
        _dataContext = new Database.DataContext(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataContext"></param>
    internal Repository(Database.IDataContext mockDataContext)
    {
        _dataContext = mockDataContext;
    }

    IEnumerable<Database.Entities.Bowler> IRepository.Search(Models.BowlerSearchCriteria searchCriteria)
    {
        var bowlers = _dataContext.Bowlers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchCriteria.LastName))
        {
            bowlers = bowlers.Where(b => b.LastName.StartsWith(searchCriteria.LastName));
        }

        if (!string.IsNullOrWhiteSpace(searchCriteria.FirstName))
        {
            bowlers = bowlers.Where(b => b.FirstName.StartsWith(searchCriteria.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(searchCriteria.EmailAddress))
        {
            bowlers = bowlers.Where(b => b.EmailAddress == searchCriteria.EmailAddress);
        }

        return bowlers;
    }
}

internal interface IRepository
{
    IEnumerable<Database.Entities.Bowler> Search(Models.BowlerSearchCriteria searchCriteria);
}