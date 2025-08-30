using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Services;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;
using Refit;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class TournamentsAdapter
    : TournamentManagerAdapter, ITournamentsAdapter
{
    public TournamentsAdapter(ITournamentManagerApi tournamentManagerApi)
        : base(tournamentManagerApi)
    { }

    public async Task<ErrorOr<IReadOnlyCollection<TournamentSummaryViewModel>>> GetTournamentsAsync(int? page, int? pageSize, CancellationToken cancellationToken)
    {
        try
        {
            OffsetPaginationResponse<TournamentSummary> response = await _tournamentManagerApi.GetTournamentsAsync(
                page,
                pageSize,
                cancellationToken).ConfigureAwait(false);

            return response.Items.Select(tournamentSummary => tournamentSummary.ToViewModel()).ToList();
        }
        catch (ApiException ex)
        {
            return GenerateError(ex, "Error fetching tournaments");
        }
    }
}
