using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;

/// <summary>
/// Command to update a registration.
/// </summary>
/// <value></value>
public sealed record UpdateRegistrationCommand
    : ICommand<Updated>
{
    /// <summary>
    /// The unique identifier of the registration to update.
    /// </summary>
    /// <value></value>
    public required RegistrationId Id { get; init; }

    /// <summary>
    /// The ID of the division to update.
    /// If not specified, the division will not be changed.
    /// </summary>
    /// <value></value>
    public DivisionId? DivisionId { get; init; }

    /// <summary>
    /// The bowlers average.  This is required for certain divisions
    /// </summary>
    /// <value></value>
    public int? Average { get; init; }

    /// <summary>
    /// Squad IDs associated with the registration.  This is a complete set of the participant's squads.
    /// If the participant is not in any squads, this will be empty.  If there is no change, this should be null.
    /// </summary>
    /// <value></value>
    public IEnumerable<SquadId>? SquadIds { get; init; }

    /// <summary>
    /// IDs of sweepers associated with the registration.  This is a complete set of the participant's sweepers.
    /// If the participant is not a sweeper, this will be empty.  If there is no change, this should be null.
    /// </summary>
    /// <value></value>
    public IEnumerable<SquadId>? SweeperIds { get; init; }

    /// <summary>
    /// Indicates if the participant is in the super sweeper.
    /// If there is no change, this should be null.
    /// </summary>
    /// <value></value>
    public bool? SuperSweeper { get; init; }

    /// <summary>
    /// The payment confirmation for the registration if a new payment has been made or a refund has been issued.
    /// If there is no new charge, this should be null.
    /// </summary>
    /// <value></value>
    public Payment? Payment { get; init; }
}