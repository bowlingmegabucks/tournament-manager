namespace NewEnglandClassic.Sweepers;

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

    public Guid Add(Database.Entities.SweeperSquad sweeper)
    {
        _dataContext.Sweepers.Add(sweeper);
        _dataContext.SaveChanges();

        return sweeper.Id;
    }

    public IEnumerable<Database.Entities.SweeperSquad> ForTournament(Guid tournamentId)
        => _dataContext.Sweepers.Where(squad => squad.TournamentId == tournamentId).AsEnumerable();
}

internal interface IRepository
{
    Guid Add(Database.Entities.SweeperSquad sweeper);

    IEnumerable<Database.Entities.SweeperSquad> ForTournament(Guid tournamentId);
}
