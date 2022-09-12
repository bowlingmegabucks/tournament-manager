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

    IEnumerable<Database.Entities.Bowler> IRepository.Search(Models.BowlerSearchCriteria searchCriteria)
    {
        var bowlers = searchCriteria.WithoutRegistrationOnSquads.Any() && searchCriteria.RegisteredInTournament.HasValue
            ? _dataContext.Bowlers.Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Squads)
                                  .Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Division).AsNoTracking()
            : searchCriteria.RegisteredInTournament.HasValue
                ? _dataContext.Bowlers.Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Division).AsNoTracking()
                : searchCriteria.WithoutRegistrationOnSquads.Any()
                            ? _dataContext.Bowlers.Include(bowler => bowler.Registrations).ThenInclude(registration => registration.Squads)
                            : _dataContext.Bowlers.AsNoTracking();

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

        if (searchCriteria.WithoutRegistrationOnSquads.Any())
        {
            bowlers = bowlers.Where(bowler => !bowler.Registrations.SelectMany(registration => registration.Squads).Select(squad => squad.SquadId).Intersect(searchCriteria.WithoutRegistrationOnSquads).Any());
        }

        if (searchCriteria.RegisteredInTournament.HasValue)
        {
            bowlers = bowlers.Where(bowler => bowler.Registrations.Any(registration => registration.Division.TournamentId == searchCriteria.RegisteredInTournament.Value));
        }

        return bowlers;
    }
}

internal interface IRepository
{
    IEnumerable<Database.Entities.Bowler> Search(Models.BowlerSearchCriteria searchCriteria);
}