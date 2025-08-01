using System.Threading.RateLimiting;
using BowlingMegabucks.TournamentManager.Api.RateLimiting;

namespace BowlingMegabucks.TournamentManager.Api.Extensions;

internal static class RateLimitingExtensions
{
    public static WebApplicationBuilder ConfigureRateLimiting(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RateLimitingOptions>(builder.Configuration.GetSection("RateLimiting"));

        var rateLimitingOptions = builder.Configuration.GetSection("RateLimiting").Get<RateLimitingOptions>()
            ?? throw new InvalidOperationException("Rate limiting options are not configured.");

        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create(SetupGlobalLimiter(rateLimitingOptions));

            options.OnRejected = async (context, _) =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("RateLimiting");
                var user = context.HttpContext.User.Identity?.Name ?? "anonymous";
                var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                logger.RateLimitExceeded(user, ip);

                var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

                var problemDetailsContext = new ProblemDetailsContext
                {
                    HttpContext = context.HttpContext,
                    ProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                    {
                        Title = "Rate limit exceeded",
                        Detail = "You have exceeded the rate limit for this API.",
                        Status = StatusCodes.Status429TooManyRequests,
                        Type = "https://httpstatuses.org/429"
                    }
                };

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                await problemDetailsService.WriteAsync(problemDetailsContext);
            };
        });

        return builder;
    }

    private static Func<HttpContext, RateLimitPartition<string>> SetupGlobalLimiter(RateLimitingOptions rateLimitingOptions)
    {
        return httpContext =>
        {
            var isAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;
            var policy = isAuthenticated
                ? rateLimitingOptions.Authenticated
                : rateLimitingOptions.Anonymous;

            return RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpContext.User.Identity?.Name
                    ?? httpContext.Connection.RemoteIpAddress?.ToString()
                    ?? httpContext.Request.Headers.Host.ToString(),
                factory: partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = policy.PermitLimit,
                    QueueLimit = policy.QueueLimit,
                    Window = TimeSpan.FromSeconds(policy.WindowSeconds)
                });
        };
    }
    
    internal static IApplicationBuilder UseApiRateLimiting(this IApplicationBuilder app)
    {
        app.UseRateLimiter();
        app.UseMiddleware<RateLimitHeaders>();
        
        return app;
    }
}