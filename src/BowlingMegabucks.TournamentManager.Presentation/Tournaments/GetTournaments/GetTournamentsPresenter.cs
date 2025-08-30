using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

/// <summary>
/// Presenter for managing tournament retrieval operations and coordinating between the view and adapter.
/// </summary>
public class GetTournamentsPresenter
{
    private IGetTournamentsView? _view;
    private readonly IGetTournamentsAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTournamentsPresenter"/> class.
    /// </summary>
    /// <param name="adapter">The adapter used to retrieve tournaments from the data source.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="adapter"/> is null.</exception>
    public GetTournamentsPresenter(IGetTournamentsAdapter adapter)
    {
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">Mock view for the UI</param>
    /// <param name="mockAdapter">Mock adapter to get tournaments</param>
    internal GetTournamentsPresenter(IGetTournamentsView mockView, IGetTournamentsAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    /// <summary>
    /// Sets the view that this presenter will interact with.
    /// </summary>
    /// <param name="view">The view instance that implements <see cref="IGetTournamentsView"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="view"/> is null.</exception>
    /// <remarks>
    /// This method must be called before invoking <see cref="GetTournamentsAsync"/> to establish
    /// the connection between the presenter and the view.
    /// </remarks>
    public void SetView(IGetTournamentsView view)
        => _view = view ?? throw new ArgumentNullException(nameof(view));

    /// <summary>
    /// Asynchronously retrieves tournaments with optional pagination and updates the view with the results.
    /// </summary>
    /// <param name="page">The page number to retrieve (1-based). If null, returns the first page.</param>
    /// <param name="pageSize">The number of tournaments per page. If null, uses the default page size.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <see cref="SetView"/> has not been called before invoking this method.
    /// </exception>
    /// <remarks>
    /// <para>
    /// This method coordinates the tournament retrieval process by:
    /// </para>
    /// <list type="number">
    /// <item><description>Calling the adapter to fetch tournament data</description></item>
    /// <item><description>Handling any errors by displaying them to the user and disabling tournament opening</description></item>
    /// <item><description>Binding successful results to the view or disabling tournament opening if no tournaments are found</description></item>
    /// </list>
    /// <para>
    /// The view must be set using <see cref="SetView"/> before calling this method.
    /// </para>
    /// </remarks>
    public async Task GetTournamentsAsync(int? page, int? pageSize, CancellationToken cancellationToken)
    {
        if (_view is null)
        {
            throw new InvalidOperationException($"{nameof(SetView)} must be set before calling {nameof(GetTournamentsAsync)}");
        }

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
