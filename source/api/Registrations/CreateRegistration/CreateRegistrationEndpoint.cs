using FastEndpoints;
using BowlingMegabucks.TournamentManager.Api.BogusData;
using BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// 
/// </summary>
public sealed class CreateRegistrationEndpoint
    : Endpoint<CreateRegistrationRequest, CreateRegistrationResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Post("/registrations");

        Description(d => d
            .Produces<CreateRegistrationResponse>(StatusCodes.Status201Created, HttpContentTypes.Json)
            .ProducesProblemDetails(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError)
            .WithName("Create Registration"));

        Summary(s =>
        {
            s.Summary = "Creates a new registration.";
            s.Description = "This endpoint allows you to create a new registration for a bowler.";

            s.ExampleRequest = new BogusCreateRegistrationRequest();

            s.ResponseExamples[StatusCodes.Status201Created] = new CreateRegistrationResponse
            {
                RegistrationId = RegistrationId.New()
            };
            s.ResponseExamples[StatusCodes.Status400BadRequest] = HttpStatusCodeResponses.SampleBadRequest400("/registrations");
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleRateLimitExceeded429();
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/registrations");

            s.Response<CreateRegistrationResponse>(StatusCodes.Status201Created, "Successfully created the registration.");
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
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
        CreateRegistrationRequest req,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        // need to see if a registration is already created, if so, we need to call the update flow
        // check for a conflict error

        var response = new CreateRegistrationResponse
        {
            RegistrationId = RegistrationId.New()
        };

        await SendCreatedAtAsync(GetRegistrationEndpoint.EndpointName, new { Id = response.RegistrationId }, response, cancellation: ct);
    }
}