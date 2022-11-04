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

    RegistrationId IRepository.Add(Database.Entities.Registration registration)
    {
        _dataContext.Registrations.Add(registration);
        _dataContext.SaveChanges();

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

    IEnumerable<Database.Entities.Registration> IRepository.Retrieve(TournamentId tournamentId)
        => _dataContext.Registrations.Include(registration => registration.Division)
            .Include(registration => registration.Squads).ThenInclude(squadRegistration=> squadRegistration.Squad)
            .Include(registration => registration.Bowler)
            .AsNoTracking()
            .Where(registration => registration.Division.TournamentId == tournamentId);

    IEnumerable<Database.Entities.SquadRegistration> IRepository.RetrieveForSquad(SquadId squadId)
        => Enumerable.Empty<Database.Entities.SquadRegistration>();

    void IRepository.Delete(RegistrationId registrationId, SquadId squadId)
    {
        var registration = _dataContext.Registrations.Include(registration=> registration.Squads).Single(registration=> registration.Id == registrationId);
        registration.Squads.Remove(registration.Squads.Single(squad => squad.SquadId == squadId));

        _dataContext.SaveChanges();
    }
}

internal interface IRepository
{
    RegistrationId Add(Database.Entities.Registration registration);

    Database.Entities.Registration AddSquad(BowlerId bowlerId, SquadId squadId);

    IEnumerable<Database.Entities.Registration> Retrieve(TournamentId tournamentId);

    IEnumerable<Database.Entities.SquadRegistration> RetrieveForSquad(SquadId squadId);

    void Delete(RegistrationId registrationId, SquadId squadId);
}
