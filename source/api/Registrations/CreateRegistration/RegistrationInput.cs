
namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// RegistrationInput is a record that represents the input data required to create a registration for a tournament.
/// It includes details about the tournament, bowler, division, squads, and payment confirmation.
/// </summary>
public sealed record RegistrationInput
{
    /// <summary>
    /// The unique identifier for the tournament.
    /// </summary>
    public required TournamentId TournamentId { get; init; }

    /// <summary>
    /// Bowler information required for the registration.
    /// This includes personal details such as name, address, contact information, USBC ID,
    /// </summary>
    public required BowlerInput Bowler { get; init; }

    /// <summary>
    /// Bowler's entering average, which is optional.
    /// </summary>
    public int? Average { get; init; }

    /// <summary>
    /// The unique identifier for the division in which the bowler is registering.
    /// </summary>
    public DivisionId DivisionId { get; init; }

    /// <summary>
    /// A list of unique identifiers for the squads in which the bowler is registering.
    /// </summary>
    public required IEnumerable<SquadId> Squads { get; init; }

    /// <summary>
    /// A list of unique identifiers for the sweepers in the registration.
    /// </summary>
    public required IEnumerable<SquadId> Sweepers { get; init; }

    /// <summary>
    /// Indicates whether the bowler is registering for the Super Sweeper event.
    /// </summary>
    public required bool SuperSweeper { get; init; }
    
    /// <summary>
    /// Payment details required to finalize the registration.
    /// This includes payment method, amount, and any other necessary payment information.
    /// </summary>
    public required PaymentInput Payment { get; init; }
}