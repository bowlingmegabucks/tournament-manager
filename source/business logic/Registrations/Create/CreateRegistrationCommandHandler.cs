using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using ErrorOr;
using FluentValidation;
using Microsoft.Extensions.Logging;

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
    private readonly ILogger<CreateRegistrationCommandHandler> _logger;

    public CreateRegistrationCommandHandler(Divisions.Retrieve.IBusinessLogic getDivision, IQueryHandler<GetTournamentByIdQuery, Tournament?> getTournament,
        IValidator<Registration> validator, Bowlers.Search.IBusinessLogic searchBowlers, Bowlers.Update.IBusinessLogic updateBowler,
        IEntityMapper entityMapper, IRepository repository, ILogger<CreateRegistrationCommandHandler> logger)
    {
        _getDivision = getDivision;
        _getTournament = getTournament;
        _validator = validator;
        _searchBowlers = searchBowlers;
        _updateBowler = updateBowler;
        _entityMapper = entityMapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<RegistrationId>> HandleAsync(CreateRegistrationCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var alreadyRegisteredResult = await IsBowlerAlreadyRegisteredForTournamentAsync(command.Bowler.USBCId, command.TournamentId, cancellationToken);

        if (alreadyRegisteredResult.IsError)
        {
            return alreadyRegisteredResult.Errors;
        }

        if (alreadyRegisteredResult.Value)
        {
            _logger.BowlerAlreadyRegistered(command.Bowler.USBCId, command.TournamentId);
            return Error.Conflict(description: "Bowler is already registered for this tournament.");
        }

        var division = await _getDivision.ExecuteAsync(command.DivisionId, cancellationToken);

        if (_getDivision.ErrorDetail is not null)
        {
            return _getDivision.ErrorDetail.ToError();
        }

        var tournamentResult = await _getTournament.HandleAsync(new() { Id = command.TournamentId }, cancellationToken);

        if (tournamentResult.IsError)
        {
            return tournamentResult.Errors;
        }

        var tournament = tournamentResult.Value!;

        var registration = new Registration
        {
            Bowler = command.Bowler,
            Division = division ?? throw new InvalidOperationException("Division not found."),
            TournamentStartDate = tournament.Start,
            TournamentSweeperCount = tournament.Sweepers.Count(),

            Squads = command.Squads.Select(squadId => new Models.Squad { Id = squadId }).ToList(),
            Sweepers = command.Sweepers.Select(sweeperId => new Models.Sweeper { Id = sweeperId }).ToList(),
            SuperSweeper = command.SuperSweeper,

            Average = command.Average
        };

        var validatorResults = await _validator.ValidateAsync(registration, cancellationToken);

        if (!validatorResults.IsValid)
        {
            return validatorResults.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage)).ToList();
        }

        var existingBowlerRecordResult = await GetExistingBowlerRecord(command.Bowler.USBCId, cancellationToken);

        if (existingBowlerRecordResult.IsError)
        {
            return existingBowlerRecordResult.Errors;
        }

        var existingBowlerRecord = existingBowlerRecordResult.Value;

        if (existingBowlerRecord is not null)
        {
            command.Bowler.Id = existingBowlerRecord.Id;
            await _updateBowler.ExecuteAsync(command.Bowler, cancellationToken);

            if (_updateBowler.Errors.Any())
            {
                return _updateBowler.Errors.ToErrors();
            }
        }

        var registrationEntity = _entityMapper.Execute(registration);

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

internal static partial class CreateRegistrationCommandHandlerLogMessages
{ 
    [LoggerMessage(LogLevel.Information, "Bowler with USBC ID {UsbcId} is already registered for tournament {TournamentId}.")]
    public static partial void BowlerAlreadyRegistered(this ILogger<CreateRegistrationCommandHandler> logger, string usbcId, TournamentId tournamentId);
}