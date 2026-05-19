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

        var currentSquadIds = existingRegistration.Squads
            .Where(squadRegistration => squadRegistration.Squad is TournamentSquad)
            .Select(squadRegistration => squadRegistration.SquadId)
            .ToList();

        var currentSweeperIds = existingRegistration.Squads
            .Where(squadRegistration => squadRegistration.Squad is SweeperSquad)
            .Select(squadRegistration => squadRegistration.SquadId)
            .ToList();

        var squadIds = command.SquadIds?.ToList() ?? currentSquadIds;
        var sweeperIds = command.SweeperIds?.ToList() ?? currentSweeperIds;

        Tournament? tournament = null;

        if (command.SquadIds is not null
            || command.SweeperIds is not null
            || command.SuperSweeper.HasValue
            || command.DivisionId is not null)
        { 
            tournament = await _tournamentRepository.RetrieveAsync(existingRegistration.Division.TournamentId, cancellationToken);
        }

        if (!(command.SquadIds is null && command.SweeperIds is null))
            {
                var squadRegistrationResult = await UpdateSquads(squadIds, tournament!, existingRegistration, cancellationToken);
                if (squadRegistrationResult.IsError)
                {
                    return squadRegistrationResult.Errors;
                }

                var sweeperRegistrationResult = await UpdateSweepers(sweeperIds, tournament!, existingRegistration, command.SuperSweeper, cancellationToken);
                if (sweeperRegistrationResult.IsError)
                {
                    return sweeperRegistrationResult.Errors;
                }

                var updatedSquads = squadRegistrationResult.Value.Concat(sweeperRegistrationResult.Value);

                existingRegistration.Squads = [.. updatedSquads];
            }

        if (command.SuperSweeper.HasValue)
        {
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
                var sweeperCount = command.SweeperIds?.Count()
                    ?? existingRegistration.Squads.Count(squadRegistration => squadRegistration.Squad is SweeperSquad);

                if (tournamentSweeperCount != sweeperCount)
                {
                    return Error.Validation(code: "Registration.InvalidSuperSweeper",
                        description: "Cannot set Super Sweeper when not all sweepers are registered.");
                }

                existingRegistration.SuperSweeper = true;
            }
        }

        if (command.DivisionId is not null)
        {
            var tournamentDivisionIds = tournament!.Divisions.Select(d => d.Id);

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

            var updateRecord = new UpdateRegistrationRecord(
                existingRegistration.Bowler,
                tournament!,
                tournament!.Divisions.Single(division => division.Id == command.DivisionId.Value),
                existingRegistration.Average);

            var validatorResults = await _registrationValidator.ValidateAsync(updateRecord, cancellationToken);

            if (!validatorResults.IsValid)
            {
                return validatorResults.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage)).ToList();
            }

            existingRegistration.DivisionId = command.DivisionId.Value;
        }

        if (command.Payment is not null)
        {
            command.Payment.CreatedAtUtc = DateTime.UtcNow;
            command.Payment.RegistrationId = existingRegistration.Id;

            existingRegistration.Payments.Add(_paymentMapper.Execute(command.Payment));
        }

        await _registrationRepository.UpdateAsync(existingRegistration, cancellationToken);

        return Result.Updated;
    }

    private async Task<ErrorOr<IEnumerable<SquadRegistration>>> UpdateSquads(IReadOnlyCollection<SquadId> squadIds, Tournament tournament, Registration existingRegistration, CancellationToken cancellationToken)
    {
        var invalidSquadIds = squadIds.Except(tournament!.Squads.Select(s => s.Id)).ToList(); // command squad ids not a part of tournament

        if (invalidSquadIds.Count > 0)
        {
            return Error.Validation(
                code: "Registration.InvalidSquadIds",
                description: "Invalid squad IDs",
                metadata: new Dictionary<string, object>
                {
                    { "InvalidSquadIds", string.Join(", ", invalidSquadIds) }
                });
        }

        var addedSquadIds = squadIds.Except(existingRegistration.Squads.Select(s => s.SquadId));
        var completedSquadIds = addedSquadIds.Where(squadId => tournament.Squads.Where(s => s.Complete).Select(s => s.Id).Contains(squadId)).ToList();

        if (completedSquadIds.Count > 0)
        {
            return Error.Validation(
                code: "Registration.InvalidSquadIds",
                description: "Cannot add squad(s) that are already complete.",
                metadata: new Dictionary<string, object>
                {
                    { "InvalidSquadIds", string.Join(", ", completedSquadIds) }
                });
        }

        var removedSquadIds = existingRegistration.Squads
            .Select(squadRegistration => squadRegistration.Squad)
            .OfType<TournamentSquad>()
            .Select(squad => squad.Id)
            .Except(squadIds)
            .ToList();

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

        var squadRegistrations = squadIds
            .Select(s => new SquadRegistration { SquadId = s, RegistrationId = existingRegistration.Id })
            .ToList();

        return squadRegistrations;
    }
    
    private async Task<ErrorOr<IEnumerable<SquadRegistration>>> UpdateSweepers(List<SquadId> sweeperIds, Tournament tournament, Registration existingRegistration, bool? superSweeperUpdate, CancellationToken cancellationToken)
    {       
        var invalidSweeperIds = sweeperIds.Except(tournament!.Sweepers.Select(s => s.Id)).ToList(); // command sweeper ids not a part of tournament

        if (invalidSweeperIds.Count > 0)
        {
            return Error.Validation(
                code: "Registration.InvalidSweeperIds",
                description: "Invalid sweeper IDs",
                metadata: new Dictionary<string, object>
                {
                    { "InvalidSweeperIds", string.Join(", ", invalidSweeperIds) }
                });
        }

        var addedSquadIds = sweeperIds.Except(existingRegistration.Squads.Select(s => s.SquadId));
        var addedCompletedSquadIds = addedSquadIds.Where(squadId => tournament.Sweepers.Where(s => s.Complete).Select(s => s.Id).Contains(squadId)).ToList();
        if (addedCompletedSquadIds.Count > 0)
        {
            return Error.Validation(
                code: "Registration.InvalidSweeperIds",
                description: "Cannot add sweeper(s) that are already complete.",
                metadata: new Dictionary<string, object>
                {
                    { "InvalidSweeperIds", string.Join(", ", addedCompletedSquadIds) }
                });
        }

        var removedSweeperIds = existingRegistration.Squads
            .Select(sweeperRegistration => sweeperRegistration.Squad)
            .OfType<SweeperSquad>()
            .Select(sweeper => sweeper.Id)
            .Except(sweeperIds)
            .ToList();

        var bowlerScoresFromRemovedSquads = await _scoresRepository.BowlerScoresForSquads(existingRegistration.BowlerId, removedSweeperIds, cancellationToken);

        if (bowlerScoresFromRemovedSquads.Count > 0)
        {
            return Error.Validation(
                code: "Registration.BowlerHasBowled",
                description: "Bowler has already bowled in removed sweeper(s).",
                metadata: new Dictionary<string, object>
                {
                    { "RemovedSweeperIds", string.Join(", ", removedSweeperIds) }
                });
        }

        if ((superSweeperUpdate.HasValue && superSweeperUpdate.Value) || existingRegistration.SuperSweeper)
        { 
            var tournamentSweeperCount = tournament!.Sweepers.Count;
            var sweeperCount = sweeperIds.Count;

            if (tournamentSweeperCount != sweeperCount)
            {
                return Error.Validation(code: "Registration.NotAllSweepersRegistered",
                    description: "Super Sweeper cannot be enrolled when not all sweepers are registered.");
            }
        }

        var sweeperRegistrations = sweeperIds
            .Select(s => new SquadRegistration { SquadId = s, RegistrationId = existingRegistration.Id })
            .ToList();

        return sweeperRegistrations;
    }
}