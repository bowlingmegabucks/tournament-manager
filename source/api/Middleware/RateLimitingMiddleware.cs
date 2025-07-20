using System.Collections.Concurrent;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BowlingMegabucks.TournamentManager.Api.Middleware;

internal sealed class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;
    private readonly RateLimitingOptions _options;
    private readonly IProblemDetailsService _problemDetailsService;
    private static readonly ConcurrentDictionary<string, LimiterEntry> _limiters = new();
    private static readonly TimeSpan _limiterTtl = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan _cleanupInterval = TimeSpan.FromMinutes(5);
    private static readonly Timer _cleanupTimer = new(_ => CleanupStaleLimiters(), null, _cleanupInterval, _cleanupInterval);

    private static void CleanupStaleLimiters()
    {
        var now = DateTime.UtcNow;
        foreach (var kvp in _limiters)
        {
            if (now - kvp.Value.LastAccessed > _limiterTtl)
            {
                _limiters.TryRemove(kvp.Key, out _);
            }
        }
    }

    public RateLimitingMiddleware(
        RequestDelegate next,
        ILogger<RateLimitingMiddleware> logger,
        IOptions<RateLimitingOptions> options,
        IProblemDetailsService problemDetailsService,
        IHostApplicationLifetime? appLifetime = null)
    {
        _next = next;
        _logger = logger;
        _options = options.Value;
        _problemDetailsService = problemDetailsService;

        // Dispose the timer on application shutdown
        appLifetime?.ApplicationStopping.Register(_cleanupTimer.Dispose);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var entry = _limiters.GetOrAdd(ip, _ => new LimiterEntry(new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
        {
            TokenLimit = _options.PermitLimit,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0,
            ReplenishmentPeriod = TimeSpan.FromSeconds(_options.WindowSeconds),
            TokensPerPeriod = _options.PermitLimit,
            AutoReplenishment = true
        })));
        entry.Touch();

        var lease = await entry.Limiter.AcquireAsync(1);

        if (!lease.IsAcquired)
        {
            _logger.RateLimitExceeded(ip);
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;

            await _problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status429TooManyRequests,
                    Title = "Too Many Requests",
                    Detail = "You have exceeded the allowed number of requests. Please try again later.",
                    Type = "https://tools.ietf.org/html/rfc6585#section-4"
                }
            });

            return;
        }

        await _next(context);
    }
}

internal static partial class RateLimitingLoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Warning, Message = "Rate limit exceeded for IP: {IP}", EventName = "RateLimitExceeded")]
    public static partial void RateLimitExceeded(this ILogger<RateLimitingMiddleware> logger, string ip);
}

internal sealed class LimiterEntry
{
    public TokenBucketRateLimiter Limiter { get; }
    public DateTime LastAccessed { get; private set; }

    public LimiterEntry(TokenBucketRateLimiter limiter)
    {
        Limiter = limiter;
        LastAccessed = DateTime.UtcNow;
    }

    public void Touch() => LastAccessed = DateTime.UtcNow;
}