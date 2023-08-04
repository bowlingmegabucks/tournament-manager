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

        return searchCriteria.WithoutRegistrationOnSquads.Any()
            ? bowlers.Where(bowler => !bowler.Registrations.SelectMany(registration => registration.Squads).Select(squad => squad.SquadId).Intersect(searchCriteria.WithoutRegistrationOnSquads).Any())
            : bowlers;
    }

    void IRepository.Update(BowlerId id, string firstName, string middleInitial, string lastName, string suffix)
    {
        var bowler = _dataContext.Bowlers.Single(b=> b.Id == id);

        bowler.FirstName = firstName;
        bowler.MiddleInitial = middleInitial;
        bowler.LastName = lastName;
        bowler.Suffix = suffix;

        _dataContext.SaveChanges();
    }

    Database.Entities.Bowler IRepository.Retrieve(BowlerId id)
        => _dataContext.Bowlers.AsNoTracking().Single(bowler => bowler.Id == id);
}

internal interface IRepository
{
    IQueryable<Database.Entities.Bowler> Search(Models.BowlerSearchCriteria searchCriteria);

    void Update(BowlerId id, string firstName, string middleInitial, string lastName, string suffix);

    Database.Entities.Bowler Retrieve(BowlerId id);
}