namespace BowlingMegabucks.TournamentManager.Database.Interceptors;

internal sealed class DatabasePerformanceOptions
{
    public int DatabaseWarningThresholdMilliseconds { get; set; } = 250;
}