using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Registrations;

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

    async Task<RegistrationId> IRepository.AddAsync(Database.Entities.Registration registration, CancellationToken cancellationToken)
    {
        await _dataContext.Registrations.AddAsync(registration, cancellationToken).ConfigureAwait(false);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return registration.Id;
    }

    Database.Entities.Registration IRepository.AddSquad(BowlerId bowlerId, SquadId squadId)
    {
        var tournamentId = _dataContext.Tournaments.Include(tournament=> tournament.Squads).Include(tournament=> tournament.Sweepers).Single(tournament => tournament.Squads.Select(squad => squad.Id).Contains(squadId) || tournament.Sweepers.Select(sweeper => sweeper.Id).Contains(squadId)).Id;
        var registration = _dataContext.Registrations.Include(registration => registration.Division)
                                                     .Include(registration=> registration.Squads)
                                                     .Include(registration=> registration.Bowler)
                                                     .Include(registration=> registration.Division)
                                                     .Single(registration => registration.BowlerId == bowlerId && registration.Division.TournamentId == tournamentId);

        registration.Squads.Add(new Database.Entities.SquadRegistration { RegistrationId = registration.Id, SquadId = squadId });

        _dataContext.SaveChanges();

        return registration;
    }

    IQueryable<Database.Entities.Registration> IRepository.Retrieve(TournamentId tournamentId)
        => _dataContext.Registrations.Include(registration => registration.Division)
            .Include(registration => registration.Squads).ThenInclude(squadRegistration=> squadRegistration.Squad)
            .Include(registration => registration.Bowler)
            .AsNoTracking()
            .Where(registration => registration.Division.TournamentId == tournamentId);

    void IRepository.Delete(BowlerId bowlerId, SquadId squadId)
    {
        if (_dataContext.SquadScores.Any(squadScore => squadScore.BowlerId == bowlerId && squadScore.SquadId == squadId))
        {
            throw new InvalidOperationException("Cannot remove bowler from squad when scores have been recorded");
        }

        var registration = _dataContext.Registrations.Include(registration=> registration.Squads).Single(registration=> registration.BowlerId == bowlerId && registration.Squads.Count(squad=> squad.SquadId == squadId) == 1);
        var squad = registration.Squads.Single(squad => squad.SquadId == squadId);

        registration.Squads.Remove(squad);

        _dataContext.SaveChanges();
    }

    void IRepository.Delete(RegistrationId id)
    {
        var registration = _dataContext.Registrations.Include(registration=> registration.Squads).Single(registration => registration.Id == id);

        if (_dataContext.SquadScores.Any(squadScore => squadScore.BowlerId == registration.BowlerId && registration.Squads.Select(squad => squad.SquadId).Contains(squadScore.SquadId)))
        {
            throw new InvalidOperationException("Cannot remove bowler from squad when scores have been recorded");
        }

        registration.Squads.Clear();

        _dataContext.Registrations.Remove(registration);

        _dataContext.SaveChanges();
    }
}

internal interface IRepository
{
    Task<RegistrationId> AddAsync(Database.Entities.Registration registration, CancellationToken cancellationToken);

    Database.Entities.Registration AddSquad(BowlerId bowlerId, SquadId squadId);

    IQueryable<Database.Entities.Registration> Retrieve(TournamentId tournamentId);

    void Delete(BowlerId bowlerId, SquadId squadId);

    void Delete(RegistrationId id);
}
