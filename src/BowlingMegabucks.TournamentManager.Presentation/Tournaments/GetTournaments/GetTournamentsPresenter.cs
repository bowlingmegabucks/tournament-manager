using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

/// <summary>
/// Presenter for managing tournament retrieval operations and coordinating between the view and adapter.
/// </summary>
public class GetTournamentsPresenter
{
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
    /// Asynchronously retrieves tournaments with optional pagination and updates the view with the results.
    /// </summary>
    /// <param name="page">The page number to retrieve (1-based). If null, returns the first page.</param>
    /// <param name="pageSize">The number of tournaments per page. If null, uses the default page size.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the view is null because <see cref="SetView"/> has not been called before invoking this method.
    /// </exception>
    /// <remarks>
    /// <para>
    /// This method first validates that the view has been properly initialized, then coordinates the tournament retrieval process by:
    /// </para>
    /// <list type="number">
    /// <item><description>Verifying that the view has been set via <see cref="SetView"/></description></item>
    /// <item><description>Calling the adapter to fetch tournament data</description></item>
    /// <item><description>Handling any errors by displaying them to the user and disabling tournament opening</description></item>
    /// <item><description>Binding successful results to the view or disabling tournament opening if no tournaments are found</description></item>
    /// </list>
    /// <para>
    /// The view must be set using <see cref="SetView"/> before calling this method, otherwise an <see cref="InvalidOperationException"/> will be thrown.
    /// </para>
    /// </remarks>
    public async Task GetTournamentsAsync(IGetTournamentsView view,int? page, int? pageSize, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(view);
        
        ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>> getTournamentResult =
            await _adapter.ExecuteAsync(page, pageSize, cancellationToken).ConfigureAwait(true);

        if (getTournamentResult.IsError)
        {
            view.DisplayErrorMessage(getTournamentResult.Errors);
            view.DisableOpenTournament();

            return;
        }

        IReadOnlyCollection<TournamentSummaryViewModel> tournaments = getTournamentResult.Value.Items;

        if (tournaments.Count == 0)
        {
            view.DisableOpenTournament();

            return;
        }

        view.BindTournaments(tournaments);
    }
}
