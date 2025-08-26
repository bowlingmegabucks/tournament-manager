using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database.Interceptors;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class SlowQueryInterceptor
    : DbCommandInterceptor
{
    private readonly ILogger<SlowQueryInterceptor> _logger;
    private readonly SlowQueryOptions _options;

    public SlowQueryInterceptor(ILogger<SlowQueryInterceptor> logger, IOptions<SlowQueryOptions> options)
    {
        _logger = logger;
        _options = options.Value;

        if (_options.ThresholdMilliseconds <= 0)
        {
            throw new ArgumentException("ThresholdMilliseconds must be greater than zero.", nameof(options));
        }
    }

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        CheckQuery(command, eventData);

        return base.ReaderExecuted(command, eventData, result);
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        CheckQuery(command, eventData);

        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    private void CheckQuery(DbCommand command, CommandExecutedEventData eventData)
    {
        if (eventData.Duration.TotalMilliseconds > _options.ThresholdMilliseconds)
        {
            _logger.SlowQuery(command.CommandText, eventData.Duration.TotalMilliseconds);
        }
        else
        {
            _logger.QueryExecuted(command.CommandText, eventData.Duration.TotalMilliseconds);
        }
    }
}
