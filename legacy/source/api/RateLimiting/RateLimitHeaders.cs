using System.Globalization;
using Microsoft.Extensions.Options;

namespace BowlingMegabucks.TournamentManager.Api.RateLimiting;

internal sealed class RateLimitHeaders
{ 
    private readonly RequestDelegate _next;
    private readonly RateLimitingOptions _options;

    public RateLimitHeaders(RequestDelegate next, IOptions<RateLimitingOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
        var policy = isAuthenticated
            ? _options.Authenticated 
            : _options.Anonymous;

        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-RateLimit-Limit"] = policy.PermitLimit.ToString(CultureInfo.InvariantCulture);
            context.Response.Headers["X-RateLimit-Reset"] = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + policy.WindowSeconds).ToString(CultureInfo.InvariantCulture);
            return Task.CompletedTask;
        });

        await _next(context);
    }
}