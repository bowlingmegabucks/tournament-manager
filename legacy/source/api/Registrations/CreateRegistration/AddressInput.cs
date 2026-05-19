namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// Represents an address input for registration.
/// </summary>
public sealed record AddressInput
{
    /// <summary>
    /// The street address.
    /// </summary>
    public required string Street { get; init; }

    /// <summary>
    /// The city.
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// The state.
    /// </summary>
    public required string State { get; init; }

    /// <summary>
    /// The ZIP code.
    /// </summary>
    public required string ZipCode { get; init; }
}