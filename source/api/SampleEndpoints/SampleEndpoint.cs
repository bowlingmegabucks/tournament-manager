using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NortheastMegabuck.Api.SampleEndpoints;

/// <summary>
/// 
/// </summary>
public sealed class SampleEndpoint
    : Endpoint<SampleRequest, SampleResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Post("/sample");
        AllowAnonymous();

        Description(d => d
            .Produces<SampleResponse>(StatusCodes.Status200OK, HttpContentTypes.Json)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError, HttpContentTypes.ProblemJson)
            .WithName("SampleEndpoint"));

        Summary(s =>
        {
            s.Summary = "Demonstrates a sample request and response.";
            s.Description = "This endpoint is used to demonstrate a sample request and response.";
            s.Response<SampleResponse>(StatusCodes.Status200OK, "Returns a sample response with the provided name and age.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Returns validation errors if the request is invalid.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "Returns a generic error response in case of an unexpected error.", HttpContentTypes.ProblemJson);
        });
    }

    /// <summary>
    /// Handles the incoming request and returns a sample response.
    /// If the request contains the header "X-Force-Error" set to true,
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task HandleAsync(
        SampleRequest req,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        if (req.ForceError)
        {
            AddError("Forced error for testing purposes.", "Error.Code");

            ThrowIfAnyErrors();
        }

        var response = new SampleResponse
        {
            RegistrationId = RegistrationId.New(),
            Name = req.Name,
            Age = req.Age
        };

        await SendOkAsync(response, ct);
    }
}