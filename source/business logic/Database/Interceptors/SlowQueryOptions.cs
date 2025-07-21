namespace BowlingMegabucks.TournamentManager.Database.Interceptors;

internal sealed class SlowQueryOptions
{
    public int ThresholdMilliseconds { get; set; }
}