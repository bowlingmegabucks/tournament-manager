using BowlingMegabucks.TournamentManager.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Registrations.AppendRegistration;

/// <summary>
/// Command to create a new registration.
/// </summary>
public record AppendRegistrationCommand
    : ICommand<RegistrationId>
{
    /// <summary>
    /// The bowler being registered.
    /// </summary>
    public required Models.Bowler Bowler { get; init; }

    /// <summary>
    /// The unique identifier for the tournament.
    /// </summary>
    public required TournamentId TournamentId { get; init; }

    /// <summary>
    /// The division the bowler is registering for.
    /// </summary>
    public required DivisionId DivisionId { get; init; }

    /// <summary>
    /// The average of the bowler.
    /// </summary>
    public int? Average { get; init; }

    /// <summary>
    /// The squads the bowler is registering for.
    /// </summary>
    public required IEnumerable<SquadId> Squads { get; init; } = [];

    /// <summary>
    /// The sweepers the bowler is registering for.
    /// </summary>
    public IEnumerable<SquadId> Sweepers { get; init; } = [];

    /// <summary>
    /// Indicates whether the bowler is registering for the super sweeper.
    /// </summary>
    public bool? SuperSweeper { get; init; }
    
    /// <summary>
    /// The payment details for the registration.
    /// </summary>
    public Models.Payment? Payment { get; init; }
}