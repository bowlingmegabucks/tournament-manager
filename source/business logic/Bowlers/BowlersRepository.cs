using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Bowlers;
internal class Repository : IRepository
{
    private readonly Database.IDataContext _dataContext;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataContext"></param>
    internal Repository(Database.IDataContext dataContext)
    {
        _dataContext = dataContext;
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
            var excludeIds = new List<BowlerId>();

            foreach (var bowler in bowlers)
            {
                var squadIds = bowler.Registrations.SelectMany(registration => registration.Squads).Select(squad => squad.SquadId);
                var alreadyOnSquad = squadIds.Intersect(searchCriteria.WithoutRegistrationOnSquads).Any();

                if (alreadyOnSquad)
                {
                    excludeIds.Add(bowler.Id);
                }
            }

            bowlers = bowlers.Where(bowler => !excludeIds.Contains(bowler.Id));
        }

        return bowlers;
    }

    async Task IRepository.UpdateAsync(BowlerId id, string firstName, string middleInitial, string lastName, string suffix, CancellationToken cancellationToken)
    {
        var bowler = await _dataContext.Bowlers.SingleAsync(b => b.Id == id, cancellationToken).ConfigureAwait(false);

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

    async Task<Database.Entities.Bowler> IRepository.RetrieveAsync(RegistrationId registrationId, CancellationToken cancellationToken)
    {
        var bowlerId = await _dataContext.Registrations.AsNoTracking().Where(registration => registration.Id == registrationId)
            .Select(registration => registration.BowlerId).SingleAsync(cancellationToken).ConfigureAwait(false);

        return await _dataContext.Bowlers.AsNoTracking().FirstAsync(bowler => bowler.Id == bowlerId, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IRepository
{
    IQueryable<Database.Entities.Bowler> Search(Models.BowlerSearchCriteria searchCriteria);

    Task UpdateAsync(BowlerId id, string firstName, string middleInitial, string lastName, string suffix, CancellationToken cancellationToken);

    Task UpdateAsync(Database.Entities.Bowler bowler, CancellationToken cancellationToken);

    Task<Database.Entities.Bowler> RetrieveAsync(BowlerId id, CancellationToken cancellationToken);

    Task<Database.Entities.Bowler> RetrieveAsync(RegistrationId registrationId, CancellationToken cancellationToken);
}