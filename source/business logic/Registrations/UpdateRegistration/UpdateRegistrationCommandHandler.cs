using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;

internal sealed class UpdateRegistrationCommandHandler
    : ICommandHandler<UpdateRegistrationCommand, Updated>
{
    private readonly IRepository _registrationRepository;
    private readonly Scores.IRepository _scoresRepository;
    private readonly Tournaments.IRepository _tournamentRepository;

    public UpdateRegistrationCommandHandler(IRepository registrationRepository, Scores.IRepository scoresRepository, Tournaments.IRepository tournamentRepository)
    {
        _registrationRepository = registrationRepository;
        _scoresRepository = scoresRepository;
        _tournamentRepository = tournamentRepository;
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

        var tournament = new Lazy<Task<Database.Entities.Tournament?>>(async () => await _tournamentRepository.RetrieveAsync(existingRegistration.Division.TournamentId, cancellationToken));

        if (command.DivisionId is not null)
        {
            existingRegistration.Average = command.Average ?? existingRegistration.Average;
            // validate division id is valid for tournament
            // validate bowler can participate in the division (meets the criteria of division and that bowler hasn't bowled)
            // look at the create registration validator for the division rules
        }

        if (command.Payment is not null)
        {
            command.Payment.CreatedAtUtc = DateTime.UtcNow;

            //map payment model to entity (exists in registration entity mapper) and add to registration
        }

        if (command.SquadIds is not null || command.SweeperIds is not null)
        {
            var squadIds = (command.SquadIds ?? []).Union(command.SweeperIds ?? []).ToList();
            var invalidSquadIds = squadIds.Except(existingRegistration.Squads.Select(s => s.SquadId)).ToList();

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

            //validate the squad ids and sweeper ids are a part of the tournament (get tournament and check squads/sweepers)
            //if scores have been bowled already in a squad, make sure that squad (sweeper) is still in the list
        }

        if (command.SuperSweeper.HasValue)
        {
            // validate that the bowler is registered (or will be registered) for all sweepers (if SuperSweeper is true)
            // make sure no sweepers scores have been bowled
        }

        await _registrationRepository.UpdateAsync(existingRegistration, cancellationToken);

        return Result.Updated;
    }
}