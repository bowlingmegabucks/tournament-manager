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

    IEnumerable<Database.Entities.Registration> IRepository.Retrieve(TournamentId tournamentId)
        => _dataContext.Registrations.Include(registration => registration.Division)
            .Include(registration => registration.Squads).ThenInclude(squadRegistration=> squadRegistration.Squad)
            .Include(registration => registration.Bowler)
            .AsNoTracking()
            .Where(registration => registration.Division.TournamentId == tournamentId);

    IEnumerable<Database.Entities.SquadRegistration> IRepository.Retrieve(SquadId squadId)
        => _dataContext.SquadRegistrations
            .Include(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
            .Include(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Division)
            .AsNoTracking()
            .Where(squadRegistration => squadRegistration.SquadId == squadId);
}

internal interface IRepository
{
    RegistrationId Add(Database.Entities.Registration registration);

    IEnumerable<Database.Entities.Registration> Retrieve(TournamentId tournamentId);

    IEnumerable<Database.Entities.SquadRegistration> Retrieve(SquadId squadId);
}
