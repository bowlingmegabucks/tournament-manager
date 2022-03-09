using Microsoft.Extensions.Configuration;

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
        => _dataContext.Tournaments.AsEnumerable();
}

internal interface IRepository
{
    IEnumerable<Database.Entities.Tournament> RetrieveAll();
}