using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Bowlers;
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

    IQueryable<Database.Entities.Bowler> IRepository.Search(Models.BowlerSearchCriteria searchCriteria)
    {
        IQueryable<Database.Entities.Bowler> bowlers;

#pragma warning disable IDE0045 // Convert to conditional expression
        if (searchCriteria.WithoutRegistrationOnSquads.Any() && searchCriteria.RegisteredInTournament.HasValue)
        {
            bowlers = _dataContext.Bowlers.Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Squads)
                                  .Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Division).AsNoTracking();
        }
        else if (searchCriteria.RegisteredInTournament.HasValue)
        {
            bowlers = _dataContext.Bowlers.Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Division).AsNoTracking();
        }
        else if (searchCriteria.WithoutRegistrationOnSquads.Any())
        {
            bowlers = _dataContext.Bowlers.Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Squads).AsNoTracking();
        }
        else
        {
            bowlers = _dataContext.Bowlers.AsNoTracking();
        }
#pragma warning restore IDE0045 // Convert to conditional expression

        if (!string.IsNullOrWhiteSpace(searchCriteria.LastName))
        {
            bowlers = bowlers.Where(b => b.LastName.StartsWith(searchCriteria.LastName));
        }

        if (!string.IsNullOrWhiteSpace(searchCriteria.FirstName))
        {
            bowlers = bowlers.Where(b => b.FirstName.StartsWith(searchCriteria.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(searchCriteria.EmailAddress))
        {
            bowlers = bowlers.Where(b => b.EmailAddress == searchCriteria.EmailAddress);
        }

        if (searchCriteria.RegisteredInTournament.HasValue)
        {
            bowlers = bowlers.Where(bowler => bowler.Registrations.Any(registration => registration.Division.TournamentId == searchCriteria.RegisteredInTournament.Value));
        }

        if (searchCriteria.NotRegisteredInTournament.HasValue)
        {
            bowlers = bowlers.Where(bowler => !bowler.Registrations.Any(registration => registration.Division.TournamentId == searchCriteria.NotRegisteredInTournament.Value));
        }

        if (searchCriteria.BowlerId.HasValue)
        {
            bowlers = bowlers.Where(bowler => bowler.Id == searchCriteria.BowlerId.Value);
        }

        if (searchCriteria.WithoutRegistrationOnSquads.Any())
        {
            //look into why bowler comes back once for each registration and squad (can remove distinct later once figured out
            bowlers = from bowler in bowlers
                      from registration in bowler.Registrations
                      from squad in registration.Squads
                      where !searchCriteria.WithoutRegistrationOnSquads.Contains(squad.SquadId)
                      select bowler;
        }

        return bowlers;
    }

    async Task IRepository.UpdateAsync(BowlerId id, string firstName, string middleInitial, string lastName, string suffix, CancellationToken cancellationToken)
    {
        var bowler = _dataContext.Bowlers.Single(b => b.Id == id);

        bowler.FirstName = firstName;
        bowler.MiddleInitial = middleInitial;
        bowler.LastName = lastName;
        bowler.Suffix = suffix;

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task IRepository.UpdateAsync(Database.Entities.Bowler bowler, CancellationToken cancellationToken)
    {
        var current = await _dataContext.Bowlers.FirstAsync(b => b.Id == bowler.Id, cancellationToken).ConfigureAwait(false);

        current.FirstName = bowler.FirstName;
        current.MiddleInitial = bowler.MiddleInitial;
        current.LastName = bowler.LastName;
        current.Suffix = bowler.Suffix;
        current.StreetAddress = bowler.StreetAddress;
        current.CityAddress = bowler.CityAddress;
        current.StateAddress = bowler.StateAddress;
        current.ZipCode = bowler.ZipCode;
        current.EmailAddress = bowler.EmailAddress;
        current.PhoneNumber = bowler.PhoneNumber;
        current.DateOfBirth = bowler.DateOfBirth;
        current.SocialSecurityNumber = bowler.SocialSecurityNumber;
        current.Gender = bowler.Gender;
        current.USBCId = bowler.USBCId;
        
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task<Database.Entities.Bowler> IRepository.RetrieveAsync(BowlerId id, CancellationToken cancellationToken)
        => await _dataContext.Bowlers.AsNoTracking().FirstAsync(bowler => bowler.Id == id, cancellationToken).ConfigureAwait(false);
}

internal interface IRepository
{
    IQueryable<Database.Entities.Bowler> Search(Models.BowlerSearchCriteria searchCriteria);

    Task UpdateAsync(BowlerId id, string firstName, string middleInitial, string lastName, string suffix, CancellationToken cancellationToken);

    Task UpdateAsync(Database.Entities.Bowler bowler, CancellationToken cancellationToken);

    Task<Database.Entities.Bowler> RetrieveAsync(BowlerId id, CancellationToken cancellationToken);
}