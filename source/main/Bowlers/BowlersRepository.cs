using Microsoft.EntityFrameworkCore;

namespace NewEnglandClassic.Bowlers;
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

        if (searchCriteria.LastName != null)
        {
            bowlers = bowlers.Where(b => b.LastName.StartsWith(searchCriteria.LastName));
        }

        if (searchCriteria.FirstName != null)
        {
            bowlers = bowlers.Where(b => b.FirstName.StartsWith(searchCriteria.FirstName));
        }

        if (searchCriteria.EmailAddress != null)
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