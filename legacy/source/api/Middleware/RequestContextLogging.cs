using Serilog.Context;

namespace BowlingMegabucks.TournamentManager.Api.Middleware;

internal sealed class RequestContextLoggingMiddleware
{
    private const string _correlationIdHeader = "x-correlation-id";
    private readonly RequestDelegate _next;

    public RequestContextLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        {
            return _next.Invoke(context);
        }
    }
    
    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}