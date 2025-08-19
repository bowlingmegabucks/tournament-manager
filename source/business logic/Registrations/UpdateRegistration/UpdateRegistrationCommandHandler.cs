using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Database.Entities;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;

internal sealed class UpdateRegistrationCommandHandler
    : ICommandHandler<UpdateRegistrationCommand, Updated>
{
    private readonly IRepository _registrationRepository;
    private readonly Scores.IRepository _scoresRepository;
    private readonly Tournaments.IRepository _tournamentRepository;
    private readonly IPaymentEntityMapper _paymentMapper;

    public UpdateRegistrationCommandHandler(IRepository registrationRepository, Scores.IRepository scoresRepository, Tournaments.IRepository tournamentRepository, IPaymentEntityMapper paymentMapper)
    {
        _registrationRepository = registrationRepository;
        _scoresRepository = scoresRepository;
        _tournamentRepository = tournamentRepository;
        _paymentMapper = paymentMapper;
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

        var tournamentTask = new Lazy<Task<Database.Entities.Tournament?>>(async () => await _tournamentRepository.RetrieveAsync(existingRegistration.Division.TournamentId, cancellationToken));

        if (command.DivisionId is not null)
        {
            existingRegistration.Average = command.Average ?? existingRegistration.Average;
            // validate division id is valid for tournament
            // validate bowler can participate in the division (meets the criteria of division and that bowler hasn't bowled)
            // look at the create registration validator for the division rules

            existingRegistration.DivisionId = command.DivisionId.Value;
        }

        if (command.SquadIds is not null || command.SweeperIds is not null)
        {
            var squadIds = (command.SquadIds ?? []).Union(command.SweeperIds ?? []).ToList();
            var invalidSquadIds = squadIds.Except((await tournamentTask.Value)!.Squads.Select(s => s.Id)).ToList(); // command squad ids not a part of tournament

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

            var tournamentSquadIds = (await tournamentTask.Value)!.Squads.Select(squad => squad.Id)
                .Union((await tournamentTask.Value)!.Sweepers.Select(sweeper => sweeper.Id));

            existingRegistration.Squads = [.. (await tournamentTask.Value)!.Squads
                .Where(s => tournamentSquadIds.Contains(s.Id))
                .Select(s => new SquadRegistration { SquadId = s.Id, RegistrationId = existingRegistration.Id })];
        }

        if (command.SuperSweeper.HasValue)
        {
            var sweeperScores = _scoresRepository.Retrieve([.. (await tournamentTask.Value)!.Sweepers.Select(s => s.Id)]);

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
                var tournamentSweeperCount = (await tournamentTask.Value)!.Sweepers.Count;
                var sweeperCount = (await tournamentTask.Value)!.Sweepers.Count;

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