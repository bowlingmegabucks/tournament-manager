using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace BowlingMegabucks.TournamentManager.Api;

internal static class HttpStatusCodeResponses
{
    private static JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    private static string ToJson<T>(this T value)
        => JsonSerializer.Serialize(value, _jsonOptions);

    internal static string SampleBadRequest400(string instance)
        => new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Instance = instance,
            Detail = "The request parameters are invalid or missing.",
            Extensions =
            {
                ["errors"] = new List<object>
                {
                    new { code = "Entity.ValidationFailure1", description = "Validation error for Property1", value = "InvalidValue" },
                    new { code = "Entity.ValidationFailure2", description = "Validation error for Property2", value = 123 },
                    new { code = "Entity.ValidationFailure3", description = "Validation error for Property3", value = true }
                },
                ["traceId"] = "0HMPNHL0JHL76:00000001"
            }
        }.ToJson();

    internal static ProblemDetails SampleUnauthorized401(string instance)
        => new()
        {
            Status = StatusCodes.Status401Unauthorized,
            Instance = instance,
            TraceId = "0HMPNHL0JHL76:00000001",
            Detail = "You do not have permission to access this resource."
        };

    internal static ProblemDetails NotFound404(string instance, string traceId)
        => new()
        {
            Status = StatusCodes.Status404NotFound,
            Detail = "The requested resource was not found.",
            Instance = instance,
            TraceId = traceId
        };

    internal static ProblemDetails SampleNotFound404(string instance)
        => new()
        {
            Status = StatusCodes.Status404NotFound,
            Detail = "The requested resource was not found.",
            Instance = instance,
            TraceId = "0HMPNHL0JHL76:00000002"
        };

    internal static ProblemDetails SampleRateLimitExceeded429(string instance = "/generic/endpoint")
        => new()
        {
            Status = StatusCodes.Status429TooManyRequests,
            Detail = "You have exceeded the rate limit for this API. Please try again later.",
            Instance = instance,
            TraceId = "0HMPNHL0JHL76:00000003"
        };

    internal static ProblemDetails SampleInternalServerError500(string instance)
        => new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Instance = instance,
            TraceId = "0HMPNHL0JHL76:00000004",
            Detail = "An unexpected error occurred while processing the request."
        };

    internal static ProblemHttpResult ToProblemDetails(this ICollection<Error> errors, string detail, string traceId)
    {
        var statusCode = errors.Any(error => error.Type == ErrorType.Validation)
            ? StatusCodes.Status400BadRequest
            : StatusCodes.Status500InternalServerError;

        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Detail = detail,
            Status = statusCode,
            Extensions =
                {
                    ["errors"] = errors.Select(e => new { e.Code, e.Description, Value=e.Metadata?["PropertyValue"] }).ToList(),
                    ["traceId"] = traceId
                }
        };

        return TypedResults.Problem(problemDetails);
    }
    
    internal static ProblemHttpResult ToProblemDetails(this NotFound _, string traceId)
    {
        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Detail = "Resource not found",
            Status = StatusCodes.Status404NotFound,
            Extensions =
                {
                    ["traceId"] = traceId
                }
        };

        return TypedResults.Problem(problemDetails);
    }
}