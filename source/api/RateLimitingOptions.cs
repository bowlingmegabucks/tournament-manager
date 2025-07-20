namespace BowlingMegabucks.TournamentManager.Api;

internal sealed class RateLimitingOptions
{
    public int PermitLimit { get; set; }
    public int WindowSeconds { get; set; }
}