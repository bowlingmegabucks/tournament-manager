using System.Diagnostics;

namespace BowlingMegabucks.TournamentManager.Registrations;

/// <summary>
/// Contains the activity source for registration-related operations.
/// </summary>
public static class RegistrationsTelemetry
{
    /// <summary>
    /// The name of the activity source for registration-related operations.
    /// </summary>
    public const string ActivitySourceName = "BowlingMegabucks.TournamentManager.Registrations";

    internal static readonly ActivitySource _activity = new(ActivitySourceName);
}