using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;

internal sealed class UpdateRegistrationCommandHandler
    : ICommandHandler<UpdateRegistrationCommand, Updated>
{
    private readonly IRepository _registrationRepository;

    public UpdateRegistrationCommandHandler(IRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
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

        if (command.DivisionId is not null)
        {
            // validate bowler can participate in the division (meets the criteria of division and that bowler hasn't bowled)
        }

        if (command.SquadIds is not null || command.SweeperIds is not null)
        {
            //validate the squad ids and sweeper ids are a part of the tournament (get tournament and check squads/sweepers)
            //if scores have been bowled already in a squad, make sure that squad (sweeper) is still in the list
        }

        if (command.SuperSweeper.HasValue)
        {
            // validate that the bowler is registered (or will be registered) for all sweepers (if SuperSweeper is true)
            // make sure no sweepers scores have been bowled
        }

        if (command.Payment is not null)
        {
            command.Payment.CreatedAtUtc = DateTime.UtcNow;

            //map payment model to entity (exists in registration entity mapper) and add to registration
        }

        await _registrationRepository.UpdateAsync(existingRegistration, cancellationToken);

        return Result.Updated;
    }
}