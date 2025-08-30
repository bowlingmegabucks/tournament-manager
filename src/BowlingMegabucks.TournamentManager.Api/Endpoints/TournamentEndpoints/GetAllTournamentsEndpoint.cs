using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetAllTournaments;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.Api.Endpoints.TournamentEndpoints;

internal static class GetAllTournamentsEndpoint
{
    public static IEndpointRouteBuilder MapGetAllTournamentsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            IOffsetPaginationQueryHandler<GetAllTournamentsQuery, TournamentSummaryDto> queryHandler,
            CancellationToken cancellationToken = default) =>
        {
            var query = new GetAllTournamentsQuery
            {
                Page = page ?? 1,
                PageSize = pageSize ?? 10,
            };

            ErrorOr<OffsetPaginationQueryResponse<TournamentSummaryDto>> result = await queryHandler.HandleAsync(query, cancellationToken);

            if (result.IsError)
            {
                return result.Errors.ToProblemDetails();
            }

            OffsetPaginationApiResponse<TournamentSummary> apiResponse = result.Value
                .ToApiResponse()
                .ConvertValues(dto => dto.ToTournamentSummary());

            return TypedResults.Ok(apiResponse);
        })
            .WithName("GetAllTournaments")
            .WithSummary("Retrieves all tournaments with pagination support")
            .WithDescription("Gets a paginated list of all tournaments available in the system. Use the page and pageSize parameters to control pagination.")
            .WithTags("Tournaments")
            .Produces<OffsetPaginationApiResponse<TournamentSummary>>(
                StatusCodes.Status200OK)
            .Produces<ValidationProblemDetails>(
                StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(
                StatusCodes.Status500InternalServerError);

        return app;
    }
}
