using FastEndpoints;
using FluentValidation.Results;

namespace BowlingMegabucks.TournamentManager.Api;

internal static class HttpStatusCodeResponses
{
    internal static ProblemDetails SampleBadRequest400(string instance)
        => new()
        {
            Status = StatusCodes.Status400BadRequest,
            Instance = instance,
            TraceId = "0HMPNHL0JHL76:00000001",
            Detail = "The request parameters are invalid or missing.",
            Errors = new List<ProblemDetails.Error>
                {
                    new(new ValidationFailure("Entity.Property1","Validation error for Property1"){ ErrorCode = "Entity.ValidationFailure1" }),
                    new(new ValidationFailure("Entity.Property2","Validation error for Property2"){ ErrorCode = "Entity.ValidationFailure2" }),
                    new(new ValidationFailure("Entity.Property3","Validation error for Property3"){ ErrorCode = "Entity.ValidationFailure3" })
                }
        };

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
            TraceId = "0HMPNHL0JHL76:00000001"
        };

    internal static ProblemDetails SampleInternalServerError500(string instance)
        => new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Instance = instance,
            TraceId = "0HMPNHL0JHL76:00000001",
            Detail = "An unexpected error occurred while processing the request."
        };
}