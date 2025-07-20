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
    private static readonly ConcurrentDictionary<string, TokenBucketRateLimiter> _limiters = new();

    public RateLimitingMiddleware(
        RequestDelegate next,
        ILogger<RateLimitingMiddleware> logger,
        IOptions<RateLimitingOptions> options,
        IProblemDetailsService problemDetailsService)
    {
        _next = next;
        _logger = logger;
        _options = options.Value;
        _problemDetailsService = problemDetailsService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var limiter = _limiters.GetOrAdd(ip, _ => new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
        {
            TokenLimit = _options.PermitLimit,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0,
            ReplenishmentPeriod = TimeSpan.FromSeconds(_options.WindowSeconds),
            TokensPerPeriod = _options.PermitLimit,
            AutoReplenishment = true
        }));

        var lease = await limiter.AcquireAsync(1);

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