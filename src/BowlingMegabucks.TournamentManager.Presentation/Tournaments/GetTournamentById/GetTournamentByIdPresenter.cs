using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;

/// <summary>
/// Presenter for managing the retrieval of a single tournament and coordinating between the view and adapter.
/// </summary>
public sealed class GetTournamentByIdPresenter
{
    private readonly IGetTournamentByIdAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTournamentByIdPresenter"/> class.
    /// </summary>
    /// <param name="adapter">The adapter used to retrieve a tournament from the data source.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="adapter"/> is null.</exception>
    public GetTournamentByIdPresenter(IGetTournamentByIdAdapter adapter)
    {
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
    }

    /// <summary>
    /// Asynchronously retrieves a tournament by its unique identifier and updates the view with the result.
    /// </summary>
    /// <param name="view">The view interface for displaying tournament data and messages.</param>
    /// <param name="id">The unique identifier of the tournament to retrieve.</param>
    /// <param name="cancellationTokenSource">A <see cref="CancellationTokenSource"/> to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the view is null because <see cref="SetView"/> has not been called before invoking this method.
    /// </exception>
    /// <remarks>
    /// <para>
    /// This method first validates that the view has been properly initialized, then coordinates the tournament retrieval process by:
    /// </para>
    /// <list type="number">
    /// <item><description>Verifying that the view is not null</description></item>
    /// <item><description>Calling the adapter to fetch tournament data</description></item>
    /// <item><description>Handling any errors by displaying them to the user</description></item>
    /// <item><description>Binding successful results to the view</description></item>
    /// </list>
    /// </remarks>
    public async Task GetTournamentAsync(IGetTournamentByIdView view, TournamentId id, CancellationTokenSource cancellationTokenSource)
    {
        ArgumentNullException.ThrowIfNull(view);
        ArgumentNullException.ThrowIfNull(cancellationTokenSource);

        view.ShowProcessingMessage("Loading tournament...", cancellationTokenSource);

        ErrorOr<TournamentDetailViewModel> getTournamentResult =
            await _adapter.ExecuteAsync(id, cancellationTokenSource.Token);

        if (getTournamentResult.IsError)
        {
            view.DisplayErrorMessage(getTournamentResult.Errors);

            view.HideProcessingMessage();

            return;
        }

        TournamentDetailViewModel tournament = getTournamentResult.Value;

        view.BindTournament(tournament);

        view.HideProcessingMessage();
    }
}
