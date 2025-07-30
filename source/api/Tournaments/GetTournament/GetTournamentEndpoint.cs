using FastEndpoints;
using BowlingMegabucks.TournamentManager.Api.BogusData;
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using Microsoft.AspNetCore.Http.HttpResults;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournament;

/// <summary>
/// 
/// </summary>
public sealed class GetTournamentEndpoint
    : Endpoint<GetTournamentRequest, Results<Ok<GetTournamentResponse>, ProblemHttpResult>>
{
    private readonly IQueryHandler<GetTournamentByIdQuery, Models.Tournament?> _queryHandler;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryHandler"></param>
    public GetTournamentEndpoint(IQueryHandler<GetTournamentByIdQuery, Models.Tournament?> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Version(1);
        Get("/tournaments/{Id}");
        AllowAnonymous();

        Description(d => d
            .Produces<GetTournamentResponse>(StatusCodes.Status200OK, HttpContentTypes.Json)
            .ProducesProblemDetails(StatusCodes.Status404NotFound)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError)
            .WithName("Get Tournament")
        );

        Summary(s =>
        {
            s.Summary = "Retrieves a specific tournament by its ID.";
            s.Description = "This endpoint returns the details of a specific tournament identified by its unique ID.";

            s.ExampleRequest = new()
            {
                Id = TournamentId.New()
            };

            s.ResponseExamples[StatusCodes.Status200OK] = new BogusGetTournamentResponse(TournamentId.New()).Generate();
            s.ResponseExamples[StatusCodes.Status404NotFound] = HttpStatusCodeResponses.SampleNotFound404("/tournaments/{Id}");
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleRateLimitExceeded429();
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/tournaments/{Id}");

            s.Response<GetTournamentResponse>(StatusCodes.Status200OK, "Returns the details of the requested tournament.");
            s.Response<ProblemDetails>(StatusCodes.Status404NotFound, "Returns a 404 Not Found if the tournament does not exist.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status429TooManyRequests, "Returns a 429 Too Many Requests if the rate limit is exceeded.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "Returns a generic error response in case of an unexpected error.", HttpContentTypes.ProblemJson);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task<Results<Ok<GetTournamentResponse>, ProblemHttpResult>> ExecuteAsync(GetTournamentRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        var tournamentResult = await _queryHandler.HandleAsync(new() { Id = req.Id }, ct);

        if (!tournamentResult.IsError)
        {
            var tournament = tournamentResult.Value!.ToDto();

            return TypedResults.Ok(new GetTournamentResponse
            {
                Tournament = tournament
            });
        }

        return tournamentResult.FirstError.NumericType == (int)ErrorType.NotFound
            ? TypedResults.NotFound().ToProblemDetails()
            : tournamentResult.Errors.ToProblemDetails("An error occurred while retrieving the tournament.");
    }
}