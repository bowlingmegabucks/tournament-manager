using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Registrations.AppendRegistration;

internal sealed class AppendRegistrationCommandHandler
    : ICommandHandler<AppendRegistrationCommand, RegistrationId>
{
    private readonly IRepository _registrationRepository;
    private readonly Tournaments.IRepository _tournamentRepository;
    private readonly Divisions.IRepository _divisionRepository;
    private readonly Squads.IRepository _squadRepository;
    private readonly Sweepers.IRepository _sweeperRepository;
    private readonly IValidator<Models.Registration> _registrationValidator;
    private readonly Bowlers.IEntityMapper _bowlerEntityMapper;
    private readonly IPaymentEntityMapper _paymentEntityMapper;

    public AppendRegistrationCommandHandler(IRepository registrationRepository,
                                            Tournaments.IRepository tournamentRepository,
                                            Divisions.IRepository divisionRepository,
                                            Squads.IRepository squadRepository,
                                            Sweepers.IRepository sweeperRepository,
                                            IValidator<Models.Registration> registrationValidator,
                                            Bowlers.IEntityMapper bowlerEntityMapper,
                                            IPaymentEntityMapper paymentEntityMapper)
    {
        _registrationRepository = registrationRepository;
        _tournamentRepository = tournamentRepository;
        _registrationValidator = registrationValidator;
        _divisionRepository = divisionRepository;
        _paymentEntityMapper = paymentEntityMapper;
        _squadRepository = squadRepository;
        _sweeperRepository = sweeperRepository;
        _bowlerEntityMapper = bowlerEntityMapper;
    }

    public async Task<ErrorOr<RegistrationId>> HandleAsync(AppendRegistrationCommand command, CancellationToken cancellationToken)
    {
        var existingRegistration = await _registrationRepository.RetrieveAsync(command.Bowler.USBCId, command.TournamentId, cancellationToken);

        if (existingRegistration is null)
        {
            return Error.Unexpected("Registration.NotFound", "The specified registration does not exist.");
        }

        var tournament = await _tournamentRepository.RetrieveAsync(command.TournamentId, cancellationToken);

        var registrationModel = new Models.Registration(existingRegistration)
        {
            Bowler = command.Bowler,
            Average = command.Average ?? existingRegistration.Average,
            SuperSweeper = command.SuperSweeper ?? existingRegistration.SuperSweeper,
            TournamentStartDate = tournament!.Start,
            TournamentSweeperCount = tournament.Sweepers.Count
        };

        if (existingRegistration.DivisionId != command.DivisionId)
        {
            var division = await _divisionRepository.RetrieveAsync(command.DivisionId, cancellationToken);
            if (division is null)
            {
                return Error.NotFound("Division.NotFound", "The specified division does not exist.");
            }

            registrationModel.Division = new Models.Division(division);
        }

        if (command.Squads.Any())
        {
            //if squads on the command exist on the current registration, return a validation error
            var existingSquadIds = existingRegistration.Squads.Select(s => s.SquadId).ToHashSet();
            var commandSquadIds = command.Squads.ToHashSet();

            if (existingSquadIds.Overlaps(commandSquadIds))
            {
                return Error.Validation("Squad.Conflict", "The specified squads are already registered.");
            }

            var squads = (await _squadRepository.RetrieveAsync(command.Squads, cancellationToken)).Select(squad => new Models.Squad(squad));
            foreach (var squad in squads)
            {
                registrationModel.AddSquad(squad);
            }
        }

        if (command.Sweepers.Any())
        {
            //if sweepers on the command exist on the current registration, return a validation error
            var existingSweeperIds = existingRegistration.Squads.Select(s => s.SquadId).ToHashSet();
            var commandSweeperIds = command.Sweepers.ToHashSet();

            if (existingSweeperIds.Overlaps(commandSweeperIds))
            {
                return Error.Validation("Sweeper.Conflict", "The specified sweepers are already registered.");
            }

            var sweepers = (await _sweeperRepository.RetrieveAsync(command.Sweepers, cancellationToken)).Select(sweeper => new Models.Sweeper(sweeper));
            foreach (var sweeper in sweepers)
            {
                registrationModel.AddSweeper(sweeper);
            }
        }

        var validationResult = await _registrationValidator.ValidateAsync(registrationModel, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage)).ToList();
        }

        Database.Entities.Payment? paymentEntity = null;
        if (command.Payment is not null)
        {
            command.Payment.CreatedAtUtc = DateTime.UtcNow;
            registrationModel.AddPayment(command.Payment);
            paymentEntity = _paymentEntityMapper.Execute(command.Payment);
        }

        var bowlerEntity = _bowlerEntityMapper.Execute(command.Bowler);
        await _registrationRepository.UpdateAsync(
            existingRegistration,
            bowlerEntity,
            command.DivisionId,
            registrationModel.Average,
            command.Squads,
            command.Sweepers,
            registrationModel.SuperSweeper,
            paymentEntity,
            cancellationToken);

        return existingRegistration.Id;
    }
}