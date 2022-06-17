using Microsoft.EntityFrameworkCore;

namespace NewEnglandClassic.Tournaments;
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
    
    IEnumerable<Database.Entities.Tournament> IRepository.RetrieveAll()
        => _dataContext.Tournaments.AsNoTracking().AsEnumerable();

    Database.Entities.Tournament IRepository.Retrieve(Guid id)
        => _dataContext.Tournaments.Single(tournament=> tournament.Id == id);

    Guid IRepository.Add(Database.Entities.Tournament tournament)
    {   
        _dataContext.Tournaments.Add(tournament);
        _dataContext.SaveChanges();
        
        return tournament.Id;
    }
}

internal interface IRepository
{
    IEnumerable<Database.Entities.Tournament> RetrieveAll();

    Database.Entities.Tournament Retrieve(Guid id);

    Guid Add(Database.Entities.Tournament tournament);
}