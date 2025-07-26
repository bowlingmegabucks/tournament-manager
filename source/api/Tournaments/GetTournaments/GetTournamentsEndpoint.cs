using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;

/// <summary>
/// Endpoint to retrieve a list of tournaments.
/// </summary>
public sealed class GetTournamentsEndpoint
    : EndpointWithoutRequest<IResult>
{
    private readonly IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> _queryHandler;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryHandler"></param>
    public GetTournamentsEndpoint(IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Version(1);
        Get("/tournaments");
        AllowAnonymous();

        Description(d => d
            .Produces<GetTournamentsResponse>(StatusCodes.Status200OK, HttpContentTypes.Json)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError)
            .WithName("Get Tournaments")
        );

        Summary(s =>
        {
            s.Summary = "Retrieves a list of tournaments.";
            s.Description = "This endpoint returns a list of tournaments with their details such as name, start date, end date, entry fee, and bowling center.";

            s.ResponseExamples[StatusCodes.Status200OK] = new BogusData.BogusGetTournamentsResponse().Generate();
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleRateLimitExceeded429();
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/tournaments");

            s.Response<GetTournamentsResponse>(StatusCodes.Status200OK, "Returns a list of tournaments with their details.");
            s.Response<ProblemDetails>(StatusCodes.Status429TooManyRequests, "Returns a 429 Too Many Requests if the rate limit is exceeded.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "Returns a generic error response in case of an unexpected error.", HttpContentTypes.ProblemJson);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task<IResult> ExecuteAsync(CancellationToken ct)
    {
        var tournamentsResult = await _queryHandler.HandleAsync(new(), ct);

        return tournamentsResult.Match<IResult>(
            tournaments =>
            {
                var response = new GetTournamentsResponse
                {
                    Tournaments = tournaments.Select(t => t.ToDto()).ToList().AsReadOnly()
                };

                return TypedResults.Ok(response);
            },
            errors =>
            {
                var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Detail = "An error occurred while retrieving tournaments.",
                    Status = StatusCodes.Status500InternalServerError
                };

                problemDetails.Extensions["errors"] = errors.Select(e => new
                {
                    e.Code,
                    e.Description
                }).ToList();

                return TypedResults.Problem(problemDetails);
            }
        );
    }
}