using Microsoft.EntityFrameworkCore;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Registrations;

internal class Repository : IRepository
{
    private readonly Database.IDataContext _dataContext;

    public Repository(Database.IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    async Task<RegistrationId> IRepository.AddAsync(Database.Entities.Registration registration, CancellationToken cancellationToken)
    {
        if (registration.BowlerId != BowlerId.Empty)
        {
            registration.Bowler = null!; // Prevent EF Core from trying to insert a new bowler
        }
        else
        {
            registration.Bowler.PhoneNumber = registration.Bowler.PhoneNumber.NormalizePhoneNumber();
        }

        await _dataContext.Registrations.AddAsync(registration, cancellationToken).ConfigureAwait(false);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return registration.Id;
    }

    async Task<Database.Entities.Registration> IRepository.AddSquadAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
    {
        var tournamentId = (await _dataContext.Tournaments.Include(tournament => tournament.Squads).Include(tournament => tournament.Sweepers).FirstAsync(tournament => tournament.Squads.Select(squad => squad.Id).Contains(squadId) || tournament.Sweepers.Select(sweeper => sweeper.Id).Contains(squadId), cancellationToken).ConfigureAwait(false)).Id;
        var registration = await _dataContext.Registrations.Include(registration => registration.Division)
                                                     .Include(registration => registration.Squads)
                                                     .Include(registration => registration.Bowler)
                                                     .Include(registration => registration.Division)
                                                     .FirstAsync(registration => registration.BowlerId == bowlerId && registration.Division.TournamentId == tournamentId, cancellationToken).ConfigureAwait(false);

        registration.Squads.Add(new Database.Entities.SquadRegistration { RegistrationId = registration.Id, SquadId = squadId });

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return registration;
    }

    IQueryable<Database.Entities.Registration> IRepository.Retrieve(TournamentId tournamentId)
        => _dataContext.Registrations.Include(registration => registration.Division)
            .Include(registration => registration.Squads).ThenInclude(squadRegistration => squadRegistration.Squad)
            .Include(registration => registration.Bowler)
            .AsNoTracking()
            .Where(registration => registration.Division.TournamentId == tournamentId);

    async Task<Database.Entities.Registration?> IRepository.RetrieveAsync(RegistrationId id, CancellationToken cancellationToken)
        => await _dataContext.Registrations
            .Include(registration => registration.Squads).ThenInclude(squad => squad.Squad)
            .Include(registration => registration.Bowler)
            .Include(registration => registration.Division).ThenInclude(division => division.Tournament)
            .Include(registration => registration.Payments)
            .FirstOrDefaultAsync(registration => registration.Id == id, cancellationToken).ConfigureAwait(false);

    async Task<Database.Entities.Registration?> IRepository.RetrieveAsync(string usbcId, TournamentId tournamentId, CancellationToken cancellationToken)
        => await _dataContext.Registrations
            .Include(registration => registration.Squads).ThenInclude(squad => squad.Squad)
            .Include(registration => registration.Bowler)
            .Include(registration => registration.Division).ThenInclude(division => division.Tournament)
            .Include(registration => registration.Payments)
            .FirstOrDefaultAsync(registration => registration.Bowler.USBCId == usbcId && registration.Division.TournamentId == tournamentId, cancellationToken).ConfigureAwait(false);

    async Task IRepository.DeleteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
    {
        if (await _dataContext.SquadScores.AnyAsync(squadScore => squadScore.BowlerId == bowlerId && squadScore.SquadId == squadId, cancellationToken).ConfigureAwait(false))
        {
            throw new InvalidOperationException("Cannot remove bowler from squad when scores have been recorded");
        }

        var registration = await _dataContext.Registrations.Include(registration => registration.Squads).FirstAsync(registration => registration.BowlerId == bowlerId && registration.Squads.Count(squad => squad.SquadId == squadId) == 1, cancellationToken).ConfigureAwait(false);
        var squad = registration.Squads.Single(squad => squad.SquadId == squadId);

        registration.Squads.Remove(squad);

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task IRepository.DeleteAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        var registration = await _dataContext.Registrations.Include(registration => registration.Squads).FirstAsync(registration => registration.Id == id, cancellationToken).ConfigureAwait(false);

        // if (await _dataContext.SquadScores.AnyAsync(squadScore => squadScore.BowlerId == registration.BowlerId && registration.Squads.Select(squad => squad.SquadId).Contains(squadScore.SquadId), cancellationToken).ConfigureAwait(false))
        // {
        //     throw new InvalidOperationException("Cannot remove bowler from squad when scores have been recorded");
        // }

        registration.Squads.Clear();

        _dataContext.Registrations.Remove(registration);

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task IRepository.UpdateAsync(RegistrationId id, bool superSweeper, CancellationToken cancellationToken)
    {
        var registration = await _dataContext.Registrations.FirstAsync(registration => registration.Id == id, cancellationToken).ConfigureAwait(false);

        registration.SuperSweeper = superSweeper;

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task IRepository.UpdateAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string? usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken)
    {
        var registration = await _dataContext.Registrations.FirstAsync(registration => registration.Id == id, cancellationToken).ConfigureAwait(false);
        var bowler = await _dataContext.Bowlers.FirstAsync(bowler => bowler.Id == registration.BowlerId, cancellationToken).ConfigureAwait(false);

        registration.Average = average;
        registration.DivisionId = divisionId;
        bowler.DateOfBirth = dateOfBirth;
        bowler.Gender = gender;
        bowler.USBCId = usbcId ?? string.Empty;

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task IRepository.UpdateAsync(RegistrationId id, int? average, CancellationToken cancellationToken)
        => await _dataContext.Registrations.Where(registration => registration.Id == id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(registration => registration.Average, average), cancellationToken).ConfigureAwait(false);

    async Task IRepository.UpdateAsync(Database.Entities.Registration registration, CancellationToken cancellationToken)
    {
        _dataContext.Registrations.Update(registration);

        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    async Task IRepository.UpdateAsync(
        Database.Entities.Registration existingRegistration,
        Database.Entities.Bowler bowlerEntity,
        DivisionId divisionId,
        int? average,
        IEnumerable<SquadId> squads,
        IEnumerable<SquadId> sweepers,
        bool? superSweeper,
        Database.Entities.Payment? paymentEntity,
        CancellationToken cancellationToken)
    {
        var existingBowler = await _dataContext.Bowlers.FirstAsync(bowler => bowler.Id == existingRegistration.BowlerId, cancellationToken);

        existingBowler.USBCId = bowlerEntity.USBCId;

        existingBowler.FirstName = bowlerEntity.FirstName;
        existingBowler.MiddleInitial = bowlerEntity.MiddleInitial;
        existingBowler.LastName = bowlerEntity.LastName;
        existingBowler.Suffix = bowlerEntity.Suffix;

        existingBowler.StreetAddress = bowlerEntity.StreetAddress;
        existingBowler.CityAddress = bowlerEntity.CityAddress;
        existingBowler.StateAddress = bowlerEntity.StateAddress;
        existingBowler.ZipCode = bowlerEntity.ZipCode;
        existingBowler.EmailAddress = bowlerEntity.EmailAddress;
        existingBowler.PhoneNumber = bowlerEntity.PhoneNumber;
        existingBowler.DateOfBirth = bowlerEntity.DateOfBirth ?? existingBowler.DateOfBirth;
        existingBowler.Gender = bowlerEntity.Gender ?? existingBowler.Gender;

        existingRegistration.DivisionId = divisionId;
        existingRegistration.Average = average ?? existingRegistration.Average;

        foreach (var squad in squads.Union(sweepers))
        {
            existingRegistration.Squads.Add(new Database.Entities.SquadRegistration
            {
                RegistrationId = existingRegistration.Id,
                SquadId = squad
            });
        }

        existingRegistration.SuperSweeper = superSweeper ?? existingRegistration.SuperSweeper;
        if (paymentEntity is not null)
        {
            existingRegistration.Payments.Add(paymentEntity);
        }

        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}

internal interface IRepository
{
    Task<RegistrationId> AddAsync(Database.Entities.Registration registration, CancellationToken cancellationToken);

    Task<Database.Entities.Registration> AddSquadAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    IQueryable<Database.Entities.Registration> Retrieve(TournamentId tournamentId);

    Task<Database.Entities.Registration?> RetrieveAsync(RegistrationId id, CancellationToken cancellationToken);

    Task<Database.Entities.Registration?> RetrieveAsync(string usbcId, TournamentId tournamentId, CancellationToken cancellationToken);

    Task DeleteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    Task DeleteAsync(RegistrationId id, CancellationToken cancellationToken);

    Task UpdateAsync(RegistrationId id, bool superSweeper, CancellationToken cancellationToken);

    Task UpdateAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string? usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken);

    Task UpdateAsync(RegistrationId id, int? average, CancellationToken cancellationToken);

    Task UpdateAsync(Database.Entities.Registration registration, CancellationToken cancellationToken);

    Task UpdateAsync(
        Database.Entities.Registration existingRegistration,
        Database.Entities.Bowler bowlerEntity,
        DivisionId divisionId,
        int? average,
        IEnumerable<SquadId> squads,
        IEnumerable<SquadId> sweepers,
        bool? superSweeper,
        Database.Entities.Payment? paymentEntity,
        CancellationToken cancellationToken);
}
