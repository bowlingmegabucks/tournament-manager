using FastEndpoints;

namespace NortheastMegabuck.Api.Tournaments.GetTournaments;

/// <summary>
/// Endpoint to retrieve a list of tournaments.
/// </summary>
public sealed class GetTournamentsEndpoint
    : EndpointWithoutRequest<GetTournamentsResponse>
{
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
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError, HttpContentTypes.ProblemJson)
            .WithName("Get Tournaments")
        );

        Summary(s =>
        {
            s.Summary = "Retrieves a list of tournaments.";
            s.Description = "This endpoint returns a list of tournaments with their details such as name, start date, end date, entry fee, and bowling center.";

            s.ResponseExamples[200] = new BogusData.BogusGetTournamentsResponse().Generate();
            s.ResponseExamples[500] = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                TraceId = "0HMPNHL0JHL76:00000001",
                Detail = "An error occurred while processing your request. Please try again later.",
                Instance = "/tournaments"
            };

            s.Response<GetTournamentsResponse>(StatusCodes.Status200OK, "Returns a list of tournaments with their details.");
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
        var response = new BogusData.BogusGetTournamentsResponse().Generate();

        await SendOkAsync(response, ct);
    }
}