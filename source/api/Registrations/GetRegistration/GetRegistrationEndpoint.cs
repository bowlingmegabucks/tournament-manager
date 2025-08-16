using FastEndpoints;
using BowlingMegabucks.TournamentManager.Api.BogusData;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;

/// <summary>
/// 
/// </summary>
public sealed class GetRegistrationEndpoint
    : Endpoint<GetRegistrationRequest, Results<Ok<GetRegistrationResponse>,ProblemHttpResult>>
{
    internal const string EndpointName = "Get Registration";
    private const string _route = "/registrations/{Id}";

    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Get(_route);

        Description(d => d
            .Produces<GetRegistrationResponse>(StatusCodes.Status200OK)
            .ProducesProblemDetails(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblemDetails(StatusCodes.Status404NotFound)
            .WithName(EndpointName));

        Summary(s =>
        {
            s.Summary = "Retrieves a registration by its ID.";
            s.Description = "This endpoint allows you to retrieve a registration using its unique identifier.";

            s.ResponseExamples[StatusCodes.Status200OK] = new BogusGetRegistrationResponse().Generate();
            s.ResponseExamples[StatusCodes.Status400BadRequest] = HttpStatusCodeResponses.SampleBadRequest400(_route);
            s.ResponseExamples[StatusCodes.Status401Unauthorized] = HttpStatusCodeResponses.SampleUnauthorized401(_route);
            s.ResponseExamples[StatusCodes.Status404NotFound] = HttpStatusCodeResponses.SampleNotFound404(_route);
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleRateLimitExceeded429();
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500(_route);

            s.Response<GetRegistrationResponse>(StatusCodes.Status200OK, "Successfully retrieved the registration.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status404NotFound, "Registration not found.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status429TooManyRequests, "Rate limit exceeded.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "An unexpected error occurred.", HttpContentTypes.ProblemJson);
        });
    }

    private readonly IQueryHandler<GetRegistrationByIdQuery, Models.Registration?> _queryHandler;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryHandler"></param>
    public GetRegistrationEndpoint(IQueryHandler<GetRegistrationByIdQuery, Models.Registration?> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async override Task<Results<Ok<GetRegistrationResponse>, ProblemHttpResult>> ExecuteAsync(GetRegistrationRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        var registrationResult = await _queryHandler.HandleAsync(new() { Id = req.Id }, ct);

        if (!registrationResult.IsError)
        {
            var registration = registrationResult.Value!.ToDto();

            return TypedResults.Ok(new GetRegistrationResponse
            {
                Registration = registration
            });
        }

        return registrationResult.FirstError.Type == ErrorOr.ErrorType.NotFound
            ? TypedResults.NotFound().ToProblemDetails(HttpContext.TraceIdentifier)
            : registrationResult.Errors.ToProblemDetails("An error occurred while retrieving the registration", HttpContext.TraceIdentifier);
    }
}