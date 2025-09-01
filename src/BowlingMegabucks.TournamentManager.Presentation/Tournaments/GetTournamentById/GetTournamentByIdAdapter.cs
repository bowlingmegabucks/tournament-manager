using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Services;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class GetTournamentByIdAdapter
    : TournamentManagerAdapter, IGetTournamentByIdAdapter
{
    public GetTournamentByIdAdapter(ITournamentManagerApi tournamentManagerApi)
        : base(tournamentManagerApi)
    { }

    public async Task<ErrorOr<TournamentDetailViewModel>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
    {
        try
        {
            ApiResponse<TournamentDetail> response = await _tournamentManagerApi.GetTournamentByIdAsync(id, cancellationToken);

            return response.Data.ToViewModel();
        }
        catch (Refit.ApiException ex)
        {
            return GenerateError(ex, "Tournaments.GetById", "Error fetching tournament");
        }
    }
}
