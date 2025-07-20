using FastEndpoints;

namespace BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;

/// <summary>
/// Endpoint to retrieve a list of tournaments.
/// </summary>
public sealed class GetTournamentsEndpoint
    : EndpointWithoutRequest<GetTournamentsResponse>
{
    private readonly ILogger<GetTournamentsEndpoint> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTournamentsEndpoint"/> class.
    /// </summary>
    /// <param name="logger">The logger instance used to log diagnostic and operational information.</param>
    public GetTournamentsEndpoint(ILogger<GetTournamentsEndpoint> logger)
    {
        _logger = logger;
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
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleTooManyRequests429("/tournaments");
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
    public override async Task HandleAsync(CancellationToken ct)
    {
#pragma warning disable CA1848 // Use the LoggerMessage delegates
        _logger.LogDebug("Handling GetTournaments request");
#pragma warning restore CA1848 // Use the LoggerMessage delegates

        var response = new BogusData.BogusGetTournamentsResponse().Generate();

        await SendOkAsync(response, ct);
    }
}