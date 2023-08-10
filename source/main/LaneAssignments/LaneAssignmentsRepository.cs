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

    IQueryable<Database.Entities.SquadRegistration> IRepository.Retrieve(SquadId squadId)
    {
        return _dataContext.Squads.Any(squad => squad.Id == squadId)
            ? _dataContext.Squads
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Division)
                .Include(squad=> squad.Registrations).ThenInclude(squadRegistration=> squadRegistration.Squad)
                .AsNoTracking()
                .Where(squad => squad.Id == squadId)
                .SelectMany(squad => squad.Registrations)
            : _dataContext.Sweepers
                .Include(sweeper => sweeper.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
                .Include(sweeper => sweeper.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Division)
                .Include(sweeper => sweeper.Registrations).ThenInclude(squadRegistration => squadRegistration.Squad).ThenInclude(sweeper => (sweeper as Database.Entities.SweeperSquad)!.Divisions)
                .AsNoTracking()
                .Where(sweeper => sweeper.Id == squadId)
                .SelectMany(sweeper => sweeper.Registrations);
    }

    async Task IRepository.UpdateAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken)
    {
        var registration = await _dataContext.Registrations.Where(registration => registration.BowlerId == bowlerId)
            .SelectMany(registration => registration.Squads)
            .FirstAsync(squadRegistration => squadRegistration.SquadId == squadId, cancellationToken).ConfigureAwait(false);

        registration.LaneAssignment = position;

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}

internal interface IRepository
{
    IQueryable<Database.Entities.SquadRegistration> Retrieve(SquadId squadId);

    Task UpdateAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken);
}