using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;

internal sealed class DeleteRegistrationCommandHandler
    : ICommandHandler<DeleteRegistrationCommand, Deleted>
{
    private readonly IRepository _registrationRepository;
    private readonly Scores.IRepository _scoresRepository;

    public DeleteRegistrationCommandHandler(IRepository registrationRepository, Scores.IRepository scoresRepository)
    {
        _registrationRepository = registrationRepository;
        _scoresRepository = scoresRepository;
    }

    public async Task<ErrorOr<Deleted>> HandleAsync(DeleteRegistrationCommand command, CancellationToken cancellationToken)
    {
        var registration = await _registrationRepository.RetrieveAsync(command.Id, cancellationToken);

        if (registration is null)
        {
            return Error.NotFound(
                code: "RegistrationNotFound",
                description: $"Registration with ID {command.Id} not found.");
        }

        var tournamentId = registration.Division.TournamentId;
        var bowlerHasScoresForTournament = await _scoresRepository.DoesBowlerHaveAnyScoresForTournamentAsync(registration.Id, tournamentId, cancellationToken);

        if (bowlerHasScoresForTournament)
        {
            return Error.Validation(
                code: "RegistrationHasScores",
                description: "Cannot delete registration with scores.");
        }

        await _registrationRepository.DeleteAsync(command.Id, cancellationToken);

        return Result.Deleted;
    }
}