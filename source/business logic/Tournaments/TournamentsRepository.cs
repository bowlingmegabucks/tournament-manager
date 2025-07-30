using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Tournaments;
internal class Repository : IRepository
{
    private readonly Database.IDataContext _dataContext;

    public Repository(Database.IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    async Task<IEnumerable<Database.Entities.Tournament>> IRepository.RetrieveAllAsync(CancellationToken cancellationToken)
        => await _dataContext.Tournaments.AsNoTracking().ToListAsync(cancellationToken);

    async Task<Database.Entities.Tournament> IRepository.RetrieveAsync(TournamentId id, CancellationToken cancellationToken)
        => await _dataContext.Tournaments.AsNoTrackingWithIdentityResolution()
            .Include(tournament => tournament.Sweepers)
            .Include(tournament => tournament.Squads)
            .Include(tournament => tournament.Divisions)
        .FirstAsync(tournament => tournament.Id == id, cancellationToken).ConfigureAwait(false);

    async Task<Database.Entities.Tournament> IRepository.RetrieveAsync(DivisionId divisionId, CancellationToken cancellationToken)
        => await _dataContext.Tournaments.Include(tournament => tournament.Divisions)
                                         .Include(tournament => tournament.Sweepers)
                                            .ThenInclude(sweeper => sweeper.Divisions).AsNoTrackingWithIdentityResolution()
                    .FirstAsync(tournament => tournament.Divisions.Any(division => division.Id == divisionId), cancellationToken).ConfigureAwait(false);

    async Task<Database.Entities.Tournament> IRepository.RetrieveAsync(SquadId squadId, CancellationToken cancellationToken)
        => await _dataContext.Tournaments.Include(tournament => tournament.Squads)
                                         .Include(tournament => tournament.Sweepers).AsNoTrackingWithIdentityResolution()
                    .FirstAsync(tournament => tournament.Squads.Any(squad => squad.Id == squadId) || tournament.Sweepers.Any(sweeper => sweeper.Id == squadId), cancellationToken).ConfigureAwait(false);

    async Task<Database.Entities.Tournament> IRepository.RetrieveAsync(RegistrationId registrationId, CancellationToken cancellationToken)
        => await _dataContext.Tournaments.Include(tournament => tournament.Squads)
                                         .Include(tournament => tournament.Sweepers).AsNoTrackingWithIdentityResolution()
                                         .FirstAsync(tournament => tournament.Squads.Any(squad => squad.Registrations.Any(registration => registration.RegistrationId == registrationId)) || tournament.Sweepers.Any(sweeper => sweeper.Registrations.Any(registration => registration.RegistrationId == registrationId)), cancellationToken).ConfigureAwait(false);

    async Task<TournamentId> IRepository.AddAsync(Database.Entities.Tournament tournament, CancellationToken cancellationToken)
    {
        await _dataContext.Tournaments.AddAsync(tournament, cancellationToken).ConfigureAwait(false);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return tournament.Id;
    }
}

internal interface IRepository
{
    Task<IEnumerable<Database.Entities.Tournament>> RetrieveAllAsync(CancellationToken cancellationToken);

    Task<Database.Entities.Tournament> RetrieveAsync(TournamentId id, CancellationToken cancellationToken);

    Task<Database.Entities.Tournament> RetrieveAsync(DivisionId divisionId, CancellationToken cancellationToken);

    Task<Database.Entities.Tournament> RetrieveAsync(SquadId squadId, CancellationToken cancellationToken);

    Task<Database.Entities.Tournament> RetrieveAsync(RegistrationId registrationId, CancellationToken cancellationToken);

    Task<TournamentId> AddAsync(Database.Entities.Tournament tournament, CancellationToken cancellationToken);
}