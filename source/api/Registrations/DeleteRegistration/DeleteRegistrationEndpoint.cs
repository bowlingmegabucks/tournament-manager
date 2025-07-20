using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenTelemetry.Trace;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.DeleteRegistration;

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
            .ProducesProblemDetails(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblemDetails(StatusCodes.Status404NotFound)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
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
            s.ResponseExamples[StatusCodes.Status401Unauthorized] = HttpStatusCodeResponses.SampleUnauthorized401("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status404NotFound] = HttpStatusCodeResponses.SampleNotFound404("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleTooManyRequests429("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/registrations/{Id}");

            s.Response(StatusCodes.Status204NoContent, "Successfully deleted the registration.");
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status404NotFound, "Registration not found.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status429TooManyRequests, "Rate limit exceeded.", HttpContentTypes.ProblemJson);
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