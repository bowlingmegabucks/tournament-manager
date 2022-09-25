using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.LaneAssignments;
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

    IEnumerable<Database.Entities.SquadRegistration> IRepository.Retrieve(SquadId squadId)
    { 
        if (_dataContext.Squads.Any(squad => squad.Id == squadId))
        {
            return _dataContext.Squads
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Division)
                .Include(squad=> squad.Registrations).ThenInclude(squadRegistration=> squadRegistration.Squad)
                .AsNoTracking()
                .Where(squad => squad.Id == squadId)
                .SelectMany(squad => squad.Registrations);
        }

        //todo: verify this works when bringing in sweepers
        return _dataContext.Sweepers
            .Include(sweeper => sweeper.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
            .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Division)
            .Include(sweeper => sweeper.Registrations).ThenInclude(squadRegistration => squadRegistration.Squad).ThenInclude(sweeper => (sweeper as Database.Entities.SweeperSquad)!.Divisions)
            .AsNoTracking()
            .Where(sweeper => sweeper.Id == squadId)
            .SelectMany(sweeper => sweeper.Registrations);
    }

    void IRepository.Update(SquadId squadId, BowlerId bowlerId, string position)
    {
        var registration = _dataContext.Registrations.Where(registration => registration.BowlerId == bowlerId).SelectMany(registration => registration.Squads).Single(squadRegistration => squadRegistration.SquadId == squadId);

        registration.LaneAssignment = position;

        _dataContext.SaveChanges();
    }
}

internal interface IRepository
{
    IEnumerable<Database.Entities.SquadRegistration> Retrieve(SquadId squadId);

    void Update(SquadId squadId, BowlerId bowlerId, string position);
}