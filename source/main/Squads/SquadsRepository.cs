using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Squads;

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

    public SquadId Add(Database.Entities.TournamentSquad squad)
    {
        _dataContext.Squads.Add(squad);
        _dataContext.SaveChanges();

        return squad.Id;
    }

    public IEnumerable<Database.Entities.TournamentSquad> Retrieve(TournamentId tournamentId)
        => _dataContext.Squads.AsNoTracking().Where(squad => squad.TournamentId == tournamentId).AsEnumerable();

    public Database.Entities.TournamentSquad Retrieve(SquadId id)
        => _dataContext.Squads.AsNoTracking().Single(squad => squad.Id == id);

    public void Complete(SquadId id)
    {
        var sweeper = _dataContext.Sweepers.Single(sweeper => sweeper.Id == id);
        sweeper.Complete = true;

        _dataContext.SaveChanges();
    }
}

internal interface IRepository
{
    SquadId Add(Database.Entities.TournamentSquad squad);

    IEnumerable<Database.Entities.TournamentSquad> Retrieve(TournamentId tournamentId);

    Database.Entities.TournamentSquad Retrieve(SquadId id);

    void Complete(SquadId id);
}
