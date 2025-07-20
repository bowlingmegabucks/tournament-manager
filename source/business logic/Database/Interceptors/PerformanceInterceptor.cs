using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.Database.Interceptors;

internal class PerformanceInterceptor
    : DbCommandInterceptor
{
    private const int _warningThresholdMilliseconds = 250; // Threshold for logging long queries
    private readonly ILogger<PerformanceInterceptor> _logger;

    public PerformanceInterceptor(ILogger<PerformanceInterceptor> logger)
    {
        _logger = logger;
    }

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        if (eventData.Duration.TotalMilliseconds > _warningThresholdMilliseconds)
        {
            PerformanceLogger.LogCommandExecutionWarning(_logger, command.CommandText, eventData.Duration.TotalMilliseconds);
        }
        else
        {
            PerformanceLogger.LogCommandExecution(_logger, command.CommandText, eventData.Duration.TotalMilliseconds);
        }

        return base.ReaderExecuted(command, eventData, result);
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    { 
        if (eventData.Duration.TotalMilliseconds > _warningThresholdMilliseconds)
        {
            PerformanceLogger.LogCommandExecutionWarning(_logger, command.CommandText, eventData.Duration.TotalMilliseconds);
        }
        else
        {
            PerformanceLogger.LogCommandExecution(_logger, command.CommandText, eventData.Duration.TotalMilliseconds);
        }

        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }
}

internal static partial class PerformanceLogger
{
    [LoggerMessage(Level = LogLevel.Debug, Message = "Executing command: {CommandText} -- Duration: {ElapsedMilliseconds} ms", EventName = "CommandExecution")]
    public static partial void LogCommandExecution(ILogger<PerformanceInterceptor> logger, string commandText, double elapsedMilliseconds);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Long Query: {Query} -- Duration: {ElapsedMilliseconds} ms", EventName = "CommandExecutionWarning")]
    public static partial void LogCommandExecutionWarning(ILogger<PerformanceInterceptor> logger, string query, double elapsedMilliseconds);
}