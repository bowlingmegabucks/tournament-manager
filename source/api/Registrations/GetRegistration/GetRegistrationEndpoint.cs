using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using BowlingMegabucks.TournamentManager.Api.BogusData;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;

/// <summary>
/// 
/// </summary>
public sealed class GetRegistrationEndpoint
    : Endpoint<GetRegistrationRequest, GetRegistrationResponse>
{
    internal const string EndpointName = "Get Registration";

    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Get("/registrations/{Id}");

        Description(d => d
            .Produces<GetRegistrationResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound, HttpContentTypes.ProblemJson)
            .WithName(EndpointName));

        Summary(s =>
        {
            s.Summary = "Retrieves a registration by its ID.";
            s.Description = "This endpoint allows you to retrieve a registration using its unique identifier.";

            s.ResponseExamples[StatusCodes.Status200OK] = new BogusGetRegistrationResponse().Generate();
            s.ResponseExamples[StatusCodes.Status400BadRequest] = HttpStatusCodeResponses.SampleBadRequest400("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status401Unauthorized] = HttpStatusCodeResponses.SampleUnauthorized401("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status404NotFound] = HttpStatusCodeResponses.SampleNotFound404("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/registrations/{Id}");

            s.Response<GetRegistrationResponse>(StatusCodes.Status200OK, "Successfully retrieved the registration.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status404NotFound, "Registration not found.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "An unexpected error occurred.", HttpContentTypes.ProblemJson);
        });
    }

    /// <summary>
    /// Handles the retrieval of a registration by its ID.
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task HandleAsync(
        GetRegistrationRequest req,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        // Logic to retrieve the registration goes here
        // For example, you might fetch the registration from a database

        var response = new BogusGetRegistrationResponse();

        await SendOkAsync(response, ct);
    }
}