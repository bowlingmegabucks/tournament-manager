using System.Diagnostics;

namespace BowlingMegabucks.TournamentManager.Tournaments;

/// <summary>
/// Contains the activity source for tournament-related operations.
/// </summary>
public static class TournamentsTelemetry
{
    /// <summary>
    /// The name of the activity source for tournament-related operations.
    /// </summary>
    public const string ActivitySourceName = "BowlingMegabucks.TournamentManager.Tournaments";

    internal static readonly ActivitySource _activity = new(ActivitySourceName);
}