using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Middleware;

internal static partial class GlobalExceptionHandlerLogMessages
{
    [LoggerMessage(LogLevel.Error, "An unhandled exception occurred.")]
    public static partial void LogException(this ILogger<GlobalExceptionHandler> logger, Exception exception);
}
