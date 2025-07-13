
namespace NortheastMegabuck.Api.Registrations.UpdateRegistration;

/// <summary>
/// Input for updating a registration.
/// </summary>
public sealed record UpdateRegistrationInput
{
    /// <summary>
    /// The ID of the registration to update.
    /// </summary>
    public required RegistrationId RegistrationId { get; init; }

    /// <summary>
    /// Squad IDs associated with the registration.  This is a complete set of the participant's squads.
    /// If the participant is not in any squads, this will be empty.  If there is no change, this should be null.
    /// </summary>
    public IEnumerable<SquadId>? SquadIds { get; init; }

    /// <summary>
    /// IDs of sweepers associated with the registration.  This is a complete set of the participant's sweepers.
    /// If the participant is not a sweeper, this will be empty.  If there is no change, this should be null.
    /// </summary>
    public IEnumerable<SquadId>? SweeperIds { get; init; }

    /// <summary>
    /// Indicates if the participant is a super sweeper.
    /// If there is no change, this should be null.
    /// </summary>
    public bool? SuperSweeper { get; init; }

    /// <summary>
    /// The payment confirmation for the registration if a new payment has been made or a refund has been issued.
    /// If there is no new charge, this should be null.
    /// </summary>
    public string? PaymentConfirmation { get; init; }
}