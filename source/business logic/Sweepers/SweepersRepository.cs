using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public async Task<SquadId> AddAsync(Database.Entities.SweeperSquad sweeper, CancellationToken cancellationToken)
    {
        await _dataContext.Sweepers.AddAsync(sweeper, cancellationToken).ConfigureAwait(false);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return sweeper.Id;
    }

    public IQueryable<Database.Entities.SweeperSquad> Retrieve(TournamentId tournamentId)
        => _dataContext.Sweepers.Include(sweeper => sweeper.Divisions).AsNoTracking().Where(squad => squad.TournamentId == tournamentId);

    public async Task<Database.Entities.SweeperSquad> RetrieveAsync(SquadId id, CancellationToken cancellationToken)
        => await _dataContext.Sweepers.AsNoTracking().FirstAsync(sweeper => sweeper.Id == id, cancellationToken).ConfigureAwait(false);

    public async Task CompleteAsync(SquadId id, CancellationToken cancellationToken)
    {
        var sweeper = await _dataContext.Sweepers.FirstAsync(sweeper => sweeper.Id == id, cancellationToken).ConfigureAwait(false);
        sweeper.Complete = true;

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    public IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId)
        => _dataContext.Registrations.AsNoTrackingWithIdentityResolution().Include(registration => registration.Division).ThenInclude(division => division.Tournament)
            .Where(registration => registration.Division.TournamentId == tournamentId && registration.SuperSweeper).Select(registration => registration.BowlerId);
}

internal interface IRepository
{
    Task<SquadId> AddAsync(Database.Entities.SweeperSquad sweeper, CancellationToken cancellationToken);

    IQueryable<Database.Entities.SweeperSquad> Retrieve(TournamentId tournamentId);

    Task<Database.Entities.SweeperSquad> RetrieveAsync(SquadId id, CancellationToken cancellationToken);

    Task CompleteAsync(SquadId id, CancellationToken cancellationToken);

    IQueryable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId);
}
