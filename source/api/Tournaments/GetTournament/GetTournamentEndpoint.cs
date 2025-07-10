using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using NortheastMegabuck.Api.BogusData;

namespace NortheastMegabuck.Api.Tournaments.GetTournament;

/// <summary>
/// 
/// </summary>
public sealed class GetTournamentEndpoint
    : Endpoint<GetTournamentRequest, GetTournamentResponse>
{
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
            .Produces<NotFound>(StatusCodes.Status404NotFound)
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError, HttpContentTypes.ProblemJson)
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

            s.ResponseExamples[200] = new BogusGetTournamentResponse(TournamentId.New()).Generate();
            s.ResponseExamples[500] = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                TraceId = "0HMPNHL0JHL76:00000001",
                Detail = "An error occurred while processing your request. Please try again later.",
                Instance = $"/tournaments/{TournamentId.New()}"
            };

            s.Response<GetTournamentResponse>(StatusCodes.Status200OK, "Returns the details of the requested tournament.");
            s.Response<NotFound>(StatusCodes.Status404NotFound, "Returns a 404 Not Found if the tournament does not exist.");
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "Returns a generic error response in case of an unexpected error.", HttpContentTypes.ProblemJson);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task HandleAsync(
        GetTournamentRequest req,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        // Simulate fetching the tournament from a data source
        var response = new BogusGetTournamentResponse(req.Id).Generate();

        if (response.Tournament is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}