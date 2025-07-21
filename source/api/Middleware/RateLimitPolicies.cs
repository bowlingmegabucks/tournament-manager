namespace BowlingMegaBucks.TournamentManager.Api.Middleware;

internal sealed class RateLimitingOptions
{
    public RateLimitPolicy Authenticated { get; set; } = new();
    public RateLimitPolicy Anonymous { get; set; } = new();
}

internal sealed class RateLimitPolicy
{
    public int PermitLimit { get; set; }

    public int WindowSeconds { get; set; }

    public int QueueLimit { get; set; }
}