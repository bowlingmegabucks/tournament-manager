namespace NewEnglandClassic.Squads;

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

    public Guid Add(Database.Entities.TournamentSquad squad)
    {
        _dataContext.Squads.Add(squad);
        _dataContext.SaveChanges();

        return squad.Id;
    }
}

internal interface IRepository
{
    Guid Add(Database.Entities.TournamentSquad squad);
}
