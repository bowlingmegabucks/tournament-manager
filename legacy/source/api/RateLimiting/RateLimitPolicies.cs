using System.ComponentModel.DataAnnotations;

namespace BowlingMegabucks.TournamentManager.Api.RateLimiting;

internal sealed class RateLimitingOptions
{
    [Required]
    public RateLimitPolicy Authenticated { get; set; } = new();

    [Required]
    public RateLimitPolicy Anonymous { get; set; } = new();
}

internal sealed class RateLimitPolicy
{
    public int PermitLimit { get; set; }

    public int WindowSeconds { get; set; }

    public int QueueLimit { get; set; }
}