using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.LaneAssignments;
internal class Repository : IRepository
{
    private readonly Database.IDataContext _dataContext;

    public Repository(Database.IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    IQueryable<Database.Entities.SquadRegistration> IRepository.Retrieve(SquadId squadId)
    {
        return _dataContext.Squads.Any(squad => squad.Id == squadId)
            ? _dataContext.Squads
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Division)
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Squad)
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

    async Task<Database.Entities.SquadRegistration> IRepository.RetrieveAsync(SquadId squadId, BowlerId bowlerId, CancellationToken cancellationToken)
    {
        var isSquad = await _dataContext.Squads.AnyAsync(squad => squad.Id == squadId, cancellationToken).ConfigureAwait(false);

        var registrations = isSquad
            ? _dataContext.Registrations
                .Include(registration => registration.Squads)
                    .ThenInclude(squad => squad.Squad)
                .Include(registration => registration.Division)
                .Where(registration => registration.Squads.Select(squad => squad.SquadId).Contains(squadId))
            : _dataContext.Registrations
                .Include(registration => registration.Squads)
                    .ThenInclude(squad => squad.Squad)
                        .ThenInclude(sweeper => (sweeper as Database.Entities.SweeperSquad)!.Divisions)
                .Include(registration => registration.Division)
            .Where(registration => registration.Squads.Select(squad => squad.SquadId).Contains(squadId));

        return (await registrations
            .Include(registration => registration.Bowler)
            .SingleAsync(registration => registration.BowlerId == bowlerId, cancellationToken).ConfigureAwait(false)).Squads.Single(squad => squad.SquadId == squadId);
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

    Task<Database.Entities.SquadRegistration> RetrieveAsync(SquadId squadId, BowlerId bowlerId, CancellationToken cancellationToken);

    Task UpdateAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken);
}