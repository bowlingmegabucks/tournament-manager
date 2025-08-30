using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Services;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;
using Refit;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class GetTournamentsAdapter
    : TournamentManagerAdapter, IGetTournamentsAdapter
{
    public GetTournamentsAdapter(ITournamentManagerApi tournamentManagerApi)
        : base(tournamentManagerApi)
    { }

    public async Task<ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>>> ExecuteAsync(int? page, int? pageSize, CancellationToken cancellationToken)
    {
#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            OffsetPaginationResponse<TournamentSummary> response = await _tournamentManagerApi.GetTournamentsAsync(
                page,
                pageSize,
                cancellationToken);

            var viewModels = response.Items.Select(tournamentSummary => tournamentSummary.ToViewModel()).ToList();

            return new OffsetPagingResult<TournamentSummaryViewModel>
            {
                Items = viewModels,
                TotalPages = response.TotalPages,
                CurrentPage = response.CurrentPage,
                PageSize = response.PageSize,
                TotalItems = response.TotalItems,
            };
        }
        catch (ApiException ex)
        {
            return GenerateError(ex, "Tournaments.GetAllException", "Error fetching tournaments");
        }
        catch (HttpRequestException ex)
        {
            return Error.Failure(
                code: "Tournaments.GetAllRequest",
                description: ex.Message);
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }
}
