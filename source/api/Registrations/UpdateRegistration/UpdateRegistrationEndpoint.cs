using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using BowlingMegabucks.TournamentManager.Api.BogusData;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;

/// <summary>
/// 
/// </summary>
public sealed class UpdateRegistrationEndpoint
    : Endpoint<UpdateRegistrationRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Patch("/registrations/{Id}");

        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblemDetails(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError)
            .WithName("Update Registration"));

        Summary(s =>
        {
            s.Summary = "Updates an existing registration.";
            s.Description = "This endpoint allows you to update an existing registration for a bowler.  This only allows updates to squads/sweepers/super sweeper entered.";

            s.ExampleRequest = new BogusUpdateRegistrationRequest().Generate();

            s.ResponseExamples[StatusCodes.Status400BadRequest] = HttpStatusCodeResponses.SampleBadRequest400("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status401Unauthorized] = HttpStatusCodeResponses.SampleUnauthorized401("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleTooManyRequests429("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/registrations/{Id}");

            s.Response<NoContent>(StatusCodes.Status204NoContent, "Successfully updated the registration.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
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
        UpdateRegistrationRequest req,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        // Logic to update the registration goes here
        // For example, you might call a service to update the registration in the database

        // If successful, return a 204 No Content response
        await SendNoContentAsync(ct);
    }
}