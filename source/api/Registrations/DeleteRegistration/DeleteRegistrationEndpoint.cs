using FastEndpoints;

namespace NortheastMegabuck.Api.Registrations.DeleteRegistration;

/// <summary>
/// 
/// </summary>
public sealed class DeleteRegistrationEndpoint
    : Endpoint<DeleteRegistrationRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Delete("/registrations/{Id}");

        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound, HttpContentTypes.ProblemJson)
            .WithName("Delete Registration"));

        Summary(s =>
        {
            s.Summary = "Deletes a registration by its ID.";
            s.Description = "This endpoint allows you to delete a registration using its unique identifier.";

            s.ExampleRequest = new DeleteRegistrationRequest
            {
                Id = RegistrationId.New()
            };

            s.ResponseExamples[StatusCodes.Status204NoContent] = new();
            s.ResponseExamples[StatusCodes.Status400BadRequest] = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                TraceId = "0HMPNHL0JHL76:00000001",
                Detail = "The request parameters are invalid.",
                Instance = "/registrations/{Id}"
            };
            s.ResponseExamples[StatusCodes.Status401Unauthorized] = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                TraceId = "0HMPNHL0JHL76:00000001",
                Detail = "You do not have permission to access this resource.",
                Instance = "/registrations/{Id}"
            };
            s.ResponseExamples[StatusCodes.Status404NotFound] = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                TraceId = "0HMPNHL0JHL76:00000001",
                Detail = "The registration with the specified ID was not found.",
                Instance = "/registrations/{Id}"
            };
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                TraceId = "0HMPNHL0JHL76:00000001",
                Detail = "An unexpected error occurred while processing your request.",
                Instance = "/registrations/{Id}"
            };

            s.Response(StatusCodes.Status204NoContent, "Successfully deleted the registration.");
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status404NotFound, "Registration not found.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "An unexpected error occurred.", HttpContentTypes.ProblemJson);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task HandleAsync(
        DeleteRegistrationRequest req,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        // Logic to delete the registration goes here

        await SendNoContentAsync(ct);
    }
}