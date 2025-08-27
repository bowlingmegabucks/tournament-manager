using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database.Interceptors;

internal static partial class SlowQueryInterceptorLogMessages
{
    [LoggerMessage(Level = LogLevel.Warning, Message = "Slow query detected: {CommandText} took {Duration} ms", EventName = "SlowQueryDetected")]
    public static partial void SlowQuery(this ILogger<SlowQueryInterceptor> logger, string commandText, double duration);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Query executed: {CommandText} in {Duration} ms", EventName = "QueryExecuted")]
    public static partial void QueryExecuted(this ILogger<SlowQueryInterceptor> logger, string commandText, double duration);
}
