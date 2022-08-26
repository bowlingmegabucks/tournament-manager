using Microsoft.EntityFrameworkCore;

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

    public SquadId Add(Database.Entities.SweeperSquad sweeper)
    {
        sweeper.Id = SquadId.New();

        _dataContext.Sweepers.Add(sweeper);
        _dataContext.SaveChanges();

        return sweeper.Id;
    }

    public IEnumerable<Database.Entities.SweeperSquad> Retrieve(TournamentId tournamentId)
        => _dataContext.Sweepers.Include(sweeper=> sweeper.Divisions).AsNoTracking().Where(squad => squad.TournamentId == tournamentId).AsEnumerable();
}

internal interface IRepository
{
    SquadId Add(Database.Entities.SweeperSquad sweeper);

    IEnumerable<Database.Entities.SweeperSquad> Retrieve(TournamentId tournamentId);
}
