using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Sweepers;

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
        _dataContext.Sweepers.Add(sweeper);
        _dataContext.SaveChanges();

        return sweeper.Id;
    }

    public IEnumerable<Database.Entities.SweeperSquad> Retrieve(TournamentId tournamentId)
        => _dataContext.Sweepers.Include(sweeper=> sweeper.Divisions).AsNoTracking().Where(squad => squad.TournamentId == tournamentId).AsEnumerable();

    public Database.Entities.SweeperSquad Retrieve(SquadId id)
        => _dataContext.Sweepers.AsNoTracking().Single(sweeper => sweeper.Id == id);

    public void Complete(SquadId id)
    {
        var sweeper = _dataContext.Sweepers.Single(sweeper => sweeper.Id == id);
        sweeper.Complete = true;

        _dataContext.SaveChanges();
    }
    public IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId)
        => _dataContext.Registrations.AsNoTrackingWithIdentityResolution().Include(registration => registration.Division).ThenInclude(division => division.Tournament)
            .Where(registration => registration.Division.TournamentId == tournamentId && registration.SuperSweeper).Select(registration => registration.BowlerId);
}

internal interface IRepository
{
    SquadId Add(Database.Entities.SweeperSquad sweeper);

    IEnumerable<Database.Entities.SweeperSquad> Retrieve(TournamentId tournamentId);

    Database.Entities.SweeperSquad Retrieve(SquadId id);

    void Complete(SquadId id);

    IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId);
}
