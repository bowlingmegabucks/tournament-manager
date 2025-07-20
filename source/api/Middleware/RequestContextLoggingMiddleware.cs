using Microsoft.Extensions.Primitives;

namespace BowlingMegabucks.TournamentManager.Api.Middleware;

internal sealed class RequestContextLoggingMiddleware
    : IEndpointFilter
{
    private const string _correlationIdHeaderName = "x-correlation-id";
    private readonly ILogger<RequestContextLoggingMiddleware> _logger;

    public RequestContextLoggingMiddleware(ILogger<RequestContextLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var correlationId = GetCorrelationId(context.HttpContext);

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId
        }))
        {
            return await next(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(_correlationIdHeaderName, out var correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}