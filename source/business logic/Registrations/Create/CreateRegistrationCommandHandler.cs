using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using ErrorOr;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Registrations.Create;

internal sealed class CreateRegistrationCommandHandler
    : ICommandHandler<CreateRegistrationCommand, RegistrationId>
{
    private readonly Divisions.Retrieve.IBusinessLogic _getDivision;
    private readonly IQueryHandler<GetTournamentByIdQuery, Tournament?> _getTournament;
    private readonly IValidator<Registration> _validator;
    private readonly Bowlers.Search.IBusinessLogic _searchBowlers;
    private readonly Bowlers.Update.IBusinessLogic _updateBowler;
    private readonly IEntityMapper _entityMapper;
    private readonly IRepository _repository;

    public CreateRegistrationCommandHandler(Divisions.Retrieve.IBusinessLogic getDivision, IQueryHandler<GetTournamentByIdQuery, Tournament?> getTournament,
        IValidator<Registration> validator, Bowlers.Search.IBusinessLogic searchBowlers, Bowlers.Update.IBusinessLogic updateBowler,
        IEntityMapper entityMapper, IRepository repository)
    {
        _getDivision = getDivision;
        _getTournament = getTournament;
        _validator = validator;
        _searchBowlers = searchBowlers;
        _updateBowler = updateBowler;
        _entityMapper = entityMapper;
        _repository = repository;
        _entityMapper = entityMapper;
        _updateBowler = updateBowler;
    }

    public async Task<ErrorOr<RegistrationId>> HandleAsync(CreateRegistrationCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var alreadyRegisteredResult = await IsBowlerAlreadyRegisteredForTournamentAsync(command.Registration.Bowler.USBCId, command.Registration.TournamentId, cancellationToken);

        if (alreadyRegisteredResult.IsError)
        {
            return alreadyRegisteredResult.Errors;
        }

        if (alreadyRegisteredResult.Value)
        {
            return Error.Conflict("Bowler is already registered for this tournament.");
        }

        var division = await _getDivision.ExecuteAsync(command.Registration.DivisionId, cancellationToken);

        if (_getDivision.ErrorDetail is not null)
        {
            return _getDivision.ErrorDetail.ToError();
        }

        var tournamentResult = await _getTournament.HandleAsync(new() { Id = command.Registration.TournamentId }, cancellationToken);

        if (tournamentResult.IsError)
        {
            return tournamentResult.Errors;
        }

        var tournament = tournamentResult.Value!;

        var registration = new Registration
        {
            Bowler = command.Registration.Bowler,
            Division = division!,
            TournamentStartDate = tournament.Start,
            TournamentSweeperCount = tournament.Sweepers.Count(),

            Squads = command.Registration.Squads.Select(squadId => new Models.Squad { Id = squadId }).ToList(),
            Sweepers = command.Registration.Sweepers.Select(sweeperId => new Models.Sweeper { Id = sweeperId }).ToList(),
            SuperSweeper = command.Registration.SuperSweeper,

            Average = command.Registration.Average
        };

        var validatorResults = await _validator.ValidateAsync(registration, cancellationToken);

        if (!validatorResults.IsValid)
        {
            return validatorResults.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage)).ToList();
        }

        var existingBowlerRecordResult = await GetExistingBowlerRecord(command.Registration.Bowler.USBCId, cancellationToken);

        if (existingBowlerRecordResult.IsError)
        {
            return existingBowlerRecordResult.Errors;
        }

        var existingBowlerRecord = existingBowlerRecordResult.Value;

        if (existingBowlerRecord is not null)
        {
            await _updateBowler.ExecuteAsync(command.Registration.Bowler, cancellationToken);

            if (_updateBowler.Errors.Any())
            {
                return _updateBowler.Errors.ToErrors();
            }

            command.Registration.Bowler.Id = existingBowlerRecord.Id;
        }

        var registrationEntity = _entityMapper.Execute(registration);

        if (registrationEntity.BowlerId != BowlerId.Empty)
        {
            registrationEntity.Bowler = null!; // Prevent EF Core from trying to insert a new bowler
        }

        return await _repository.AddAsync(registrationEntity, cancellationToken);
    }

    private async Task<ErrorOr<bool>> IsBowlerAlreadyRegisteredForTournamentAsync(string usbcId, TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var bowlerSearchCriteria = new BowlerSearchCriteria
        {
            UsbcId = usbcId,
            RegisteredInTournament = tournamentId
        };

        var bowlerSearchResult = await _searchBowlers.ExecuteAsync(bowlerSearchCriteria, cancellationToken);

        if (_searchBowlers.ErrorDetail is not null)
        {
            return _searchBowlers.ErrorDetail.ToError();
        }

        return bowlerSearchResult.Any(); // Bowler is already registered in the tournament
    }

    private async Task<ErrorOr<Bowler?>> GetExistingBowlerRecord(string usbcId, CancellationToken cancellationToken)
    {
        var bowlerSearchCriteria = new BowlerSearchCriteria
        {
            UsbcId = usbcId,
        };

        var bowlerSearchResult = await _searchBowlers.ExecuteAsync(bowlerSearchCriteria, cancellationToken);

        return _searchBowlers.ErrorDetail is not null
            ? _searchBowlers.ErrorDetail.ToError()
            : bowlerSearchResult.SingleOrDefault();
    }
}