using System.Net;

namespace BowlingMegabucks.TournamentManager.Api.Middleware;

internal sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogUnhandledException(ex);

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Title = "An error occurred",
                    Detail = "An unexpected error occurred while processing your request.",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Instance = context.Request.Path,
                    Extensions =
                    {
                        ["traceId"] = context.TraceIdentifier
                    }
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}

internal static partial class GlobalExceptionLogMessages
{
    [LoggerMessage(Level = LogLevel.Error, Message = "An unhandled exception occurred while processing the request")]
    public static partial void LogUnhandledException(this ILogger<GlobalExceptionMiddleware> logger, Exception ex);
}

internal static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionMiddleware>();
}