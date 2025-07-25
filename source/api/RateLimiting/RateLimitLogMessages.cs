namespace BowlingMegabucks.TournamentManager.Api.RateLimiting;

internal static partial class RateLimitLogMessages
{
    [LoggerMessage(Level = LogLevel.Warning, Message = "Rate limit exceeded for user {User} from IP {Ip}.", EventName = "RateLimitExceeded")]
    public static partial void RateLimitExceeded(this ILogger logger, string user, string ip);
}