namespace BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;

/// <summary>
/// Represents the address details of a bowler in a registration.
/// </summary>
public sealed record AddressDetailDto
{
    /// <summary>
    /// The street address of the bowler.
    /// </summary>
    public required string Street { get; init; }

    /// <summary>
    /// The city of the bowler's address.
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// The state of the bowler's address.
    /// </summary>
    public required string State { get; init; }

    /// <summary>
    /// The postal code of the bowler's address.
    /// </summary>
    public required string ZipCode { get; init; }
}