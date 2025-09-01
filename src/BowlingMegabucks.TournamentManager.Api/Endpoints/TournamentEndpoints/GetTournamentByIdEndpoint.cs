using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.Api.Endpoints.TournamentEndpoints;

internal static class GetTournamentByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetTournamentByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (
            string id,
            IQueryHandler<GetTournamentByIdQuery, TournamentDetailDto?> handler,
            CancellationToken cancellationToken = default) =>
        {
            if (!TournamentId.TryParse(id, provider: null, out TournamentId tournamentId))
            {
                return ApiExtensions.InvalidId(TournamentErrors.InvalidTournamentIdMessage, id);
            }

            var query = new GetTournamentByIdQuery
            {
                Id = tournamentId,
            };

            ErrorOr<TournamentDetailDto?> result = await handler.HandleAsync(query, cancellationToken);

            if (result.IsError)
            {
                return result.Errors.ToProblemDetails();
            }

            return TypedResults.Ok(ApiResponse.Ok(result.Value!.ToTournamentDetail()));
        })
            .WithName("GetTournamentById")
            .WithSummary("Retrieves the details of a specific tournament by its unique identifier")
            .WithDescription("Gets the full details of a tournament specified by its ID. Returns 404 if the tournament is not found.")
            .WithTags("Tournaments")
            .Produces<ApiResponse<TournamentDetail>>(
                StatusCodes.Status200OK)
            .Produces<ProblemDetails>(
                StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(
                StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(
                StatusCodes.Status500InternalServerError);

        return app;
    }
}
