using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace NortheastMegabuck.Api.SampleEndpoints;

/// <summary>
/// 
/// </summary>
public sealed class SampleEndpoint
    : Endpoint<SampleRequest, Results<Ok<SampleResponse>, BadRequest<ValidationProblemDetails>, FastEndpoints.ProblemDetails>>
{
    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Post("/sample");
        AllowAnonymous();

        Description(d => d
            .Produces<SampleResponse>(StatusCodes.Status200OK, "application/json")
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")
            .Produces<FastEndpoints.ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")
            .WithName("SampleEndpoint"));

        Summary(s =>
        {
            s.Summary = "Demonstrates a sample request and response.";
            s.Description = "This endpoint is used to demonstrate a sample request and response.";
            s.Response<SampleResponse>(StatusCodes.Status200OK, "Returns a sample response with the provided name and age.");
            s.Response<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "Returns validation errors if the request is invalid.", "application/problem+json");
            s.Response<FastEndpoints.ProblemDetails>(StatusCodes.Status500InternalServerError, "Returns a generic error response in case of an unexpected error.", "application/problem+json");
        });
    }

    /// <summary>
    /// Handles the incoming request and returns a sample response.
    /// If the request contains the header "X-Force-Error" set to true,
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task<Results<Ok<SampleResponse>, BadRequest<ValidationProblemDetails>, FastEndpoints.ProblemDetails>> HandleAsync(
        SampleRequest request,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        await Task.CompletedTask;
        
        if (request.ForceError)
        {
            AddError("Forced error for testing purposes.", "Error.Code", FluentValidation.Severity.Error);

            return new FastEndpoints.ProblemDetails(ValidationFailures);
        }

        var response = new SampleResponse
        {
            RegistrationId = RegistrationId.New(),
            Name = request.Name,
            Age = request.Age
        };

        return TypedResults.Ok(response);
    }
}