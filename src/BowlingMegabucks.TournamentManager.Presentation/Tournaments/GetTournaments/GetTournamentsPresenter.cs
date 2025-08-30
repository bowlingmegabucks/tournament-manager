using ErrorOr;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

public class GetTournamentsPresenter
{
    private readonly IGetTournamentsView _view;
    private readonly IGetTournamentsAdapter _adapter;

    public GetTournamentsPresenter(IGetTournamentsView view, IServiceProvider services)
    {
        _view = view;
        _adapter = services.GetRequiredService<IGetTournamentsAdapter>();
    }

    internal GetTournamentsPresenter(IGetTournamentsView mockView, IGetTournamentsAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    public async Task GetTournamentsAsync(int? page, int? pageSize, CancellationToken cancellationToken)
    {
        ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>> getTournamentResult =
            await _adapter.ExecuteAsync(page, pageSize, cancellationToken).ConfigureAwait(true);

        if (getTournamentResult.IsError)
        {
            _view.DisplayErrorMessage(getTournamentResult.Errors);
            _view.DisableOpenTournament();

            return;
        }

        IReadOnlyCollection<TournamentSummaryViewModel> tournaments = getTournamentResult.Value.Items;

        if (tournaments.Count == 0)
        {
            _view.DisableOpenTournament();

            return;
        }

        _view.BindTournaments(tournaments);
    }
}
