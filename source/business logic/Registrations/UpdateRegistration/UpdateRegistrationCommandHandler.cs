using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Database.Entities;
using ErrorOr;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;

internal sealed class UpdateRegistrationCommandHandler
    : ICommandHandler<UpdateRegistrationCommand, Updated>
{
    private readonly IRepository _registrationRepository;
    private readonly Scores.IRepository _scoresRepository;
    private readonly IValidator<UpdateRegistrationRecord> _registrationValidator;
    private readonly Tournaments.IRepository _tournamentRepository;

    private readonly IPaymentEntityMapper _paymentMapper;

    public UpdateRegistrationCommandHandler(IRepository registrationRepository, Scores.IRepository scoresRepository, Tournaments.IRepository tournamentRepository, IPaymentEntityMapper paymentMapper, IValidator<UpdateRegistrationRecord> registrationValidator)
    {
        _registrationRepository = registrationRepository;
        _scoresRepository = scoresRepository;
        _tournamentRepository = tournamentRepository;
        _paymentMapper = paymentMapper;
        _registrationValidator = registrationValidator;
    }

    public async Task<ErrorOr<Updated>> HandleAsync(UpdateRegistrationCommand command, CancellationToken cancellationToken)
    {
        var existingRegistration = await _registrationRepository.RetrieveAsync(command.Id, cancellationToken);

        if (existingRegistration is null)
        {
            return Error.NotFound(
                code: "Registration.NotFound",
                description: $"Registration with ID {command.Id} not found.");
        }

        var tournamentTask = new Lazy<Task<Tournament?>>(async () => await _tournamentRepository.RetrieveAsync(existingRegistration.Division.TournamentId, cancellationToken));

        if (command.DivisionId is not null)
        {
            var tournamentDivisionIds = (await tournamentTask.Value)!.Divisions.Select(d => d.Id);

            if (!tournamentDivisionIds.Contains(command.DivisionId.Value))
            {
                return Error.Validation(
                    code: "Registration.InvalidDivisionId",
                    description: "Division ID is not valid for the tournament.",
                    metadata: new Dictionary<string, object>
                    {
                        { "ValidDivisionIds", string.Join(", ", tournamentDivisionIds) }
                    });
            }

            existingRegistration.Average = command.Average ?? existingRegistration.Average;
            
            var tournament = await tournamentTask.Value;
            var updateRecord = new UpdateRegistrationRecord(
                existingRegistration.Bowler,
                tournament!,
                tournament!.Divisions.Single(division => division.Id == command.DivisionId.Value),
                command.Average);

            var validatorResults = await _registrationValidator.ValidateAsync(updateRecord, cancellationToken);

            if (!validatorResults.IsValid)
            {
                return validatorResults.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage)).ToList();
            }

            existingRegistration.DivisionId = command.DivisionId.Value;
        }

        if (command.SquadIds is not null || command.SweeperIds is not null)
        {
            var tournament = await tournamentTask.Value;
            var squadIds = (command.SquadIds ?? []).Union(command.SweeperIds ?? []).ToList();
            var invalidSquadIds = squadIds.Except(tournament!.Squads.Select(s => s.Id)).ToList(); // command squad ids not a part of tournament

            if (invalidSquadIds.Count > 0)
            {
                return Error.Validation(
                    code: "Registration.InvalidSquadIds",
                    description: "Invalid squad/sweeper IDs",
                    metadata: new Dictionary<string, object>
                    {
                        { "InvalidSquadIds", string.Join(", ", invalidSquadIds) }
                    });
            }

            var removedSquadIds = existingRegistration.Squads.Select(s => s.SquadId).Except(squadIds).ToList();
            var bowlerScoresFromRemovedSquads = await _scoresRepository.BowlerScoresForSquads(existingRegistration.BowlerId, removedSquadIds, cancellationToken);

            if (bowlerScoresFromRemovedSquads.Count > 0)
            {
                return Error.Validation(
                    code: "Registration.BowlerHasBowled",
                    description: "Bowler has already bowled in removed squads.",
                    metadata: new Dictionary<string, object>
                    {
                        { "RemovedSquadIds", string.Join(", ", removedSquadIds) }
                    });
            }

            var tournamentSquadIds = tournament!.Squads.Select(squad => squad.Id)
                .Union(tournament!.Sweepers.Select(sweeper => sweeper.Id));

            existingRegistration.Squads = [.. tournament!.Squads
                .Where(s => tournamentSquadIds.Contains(s.Id))
                .Select(s => new SquadRegistration { SquadId = s.Id, RegistrationId = existingRegistration.Id })];
        }

        if (command.SuperSweeper.HasValue)
        {
            var tournament = await tournamentTask.Value;
            var sweeperScores = _scoresRepository.Retrieve([.. tournament!.Sweepers.Select(s => s.Id)]);

            if (sweeperScores.Any())
            {
                return Error.Validation(
                    code: "Registration.SuperSweeperScoresExist",
                    description: "Cannot change Super Sweeper when sweeper scores have been recorded.");
            }

            if (!command.SuperSweeper.Value)
            {
                existingRegistration.SuperSweeper = false;
            }
            else
            {
                var tournamentSweeperCount = tournament!.Sweepers.Count;
                var sweeperCount = tournament!.Sweepers.Count;

                if (tournamentSweeperCount != sweeperCount)
                {
                    return Error.Validation(code: "Registration.InvalidSuperSweeper",
                        description: "Cannot set Super Sweeper when not all sweepers are registered.");
                }

                existingRegistration.SuperSweeper = true;
            }
        }

        if (command.Payment is not null)
        {
            command.Payment.CreatedAtUtc = DateTime.UtcNow;

            existingRegistration.Payments.Add(_paymentMapper.Execute(command.Payment));
        }

        await _registrationRepository.UpdateAsync(existingRegistration, cancellationToken);

        return Result.Updated;
    }
}