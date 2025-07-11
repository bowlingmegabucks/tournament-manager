using FastEndpoints;
using NortheastMegabuck.Api.BogusData;
using NortheastMegabuck.Api.Registrations.GetRegistration;

namespace NortheastMegabuck.Api.Registrations.CreateRegistration;

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
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError, HttpContentTypes.ProblemJson)
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
            s.ResponseExamples[StatusCodes.Status400BadRequest] = ResponseExamples.BadRequest400("/registrations");
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = ResponseExamples.InternalServerError500("/registrations");

            s.Response<CreateRegistrationResponse>(StatusCodes.Status201Created, "Successfully created the registration.");
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
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

        // Logic to create the registration goes here
        // For example, you might save the registration to a database

        var response = new CreateRegistrationResponse
        {
            RegistrationId = RegistrationId.New()
        };

        await SendCreatedAtAsync(GetRegistrationEndpoint.EndpointName, new { Id = response.RegistrationId }, response, cancellation: ct);
    }
}