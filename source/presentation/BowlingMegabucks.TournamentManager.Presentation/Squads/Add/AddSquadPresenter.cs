using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Squads.Add;

/// <summary>
/// Handles the presentation logic for adding a new squad to a tournament.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly Tournaments.Retrieve.IAdapter _retrieveTournamentAdapter;

    private readonly Lazy<IAdapter> _addSquadAdapter;
    private IAdapter AddSquadAdapter => _addSquadAdapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for adding a squad.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        _retrieveTournamentAdapter = services.GetRequiredService<Tournaments.Retrieve.IAdapter>();
        _addSquadAdapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView">The mock view for testing.</param>
    /// <param name="mockRetrieveTournamentAdapter">The mock tournament adapter for testing.</param>
    /// <param name="mockAddSquadAdapter">The mock add squad adapter for testing.</param>
    internal Presenter(IView mockView, Tournaments.Retrieve.IAdapter mockRetrieveTournamentAdapter, IAdapter mockAddSquadAdapter)
    {
        _view = mockView;
        _retrieveTournamentAdapter = mockRetrieveTournamentAdapter;
        _addSquadAdapter = new Lazy<IAdapter>(() => mockAddSquadAdapter);
    }

    /// <summary>
    /// Retrieves tournament details and updates the view with entry fee, finals ratio, and cash ratio.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method fetches tournament details and updates the view with formatted values. Displays errors if retrieval fails.
    /// </remarks>
    public async Task GetTournamentDetailsAsync(CancellationToken cancellationToken)
    {
        var tournament = await _retrieveTournamentAdapter.ExecuteAsync(_view.Squad.TournamentId, cancellationToken).ConfigureAwait(true);

        if (_retrieveTournamentAdapter.Error != null)
        {
            _view.DisplayError(_retrieveTournamentAdapter.Error.Message);
        }
        else
        {
            _view.SetTournamentEntryFee(tournament!.EntryFee.ToString("C2", CultureInfo.CurrentCulture));
            _view.SetTournamentFinalsRatio(tournament.FinalsRatio.ToString("N1", CultureInfo.CurrentCulture));
            _view.SetTournamentCashRatio(tournament.CashRatio.ToString("N1", CultureInfo.CurrentCulture));
        }
    }

    /// <summary>
    /// Executes the process of adding a new squad asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method validates the view, attempts to add a squad, and updates the view with the result or errors.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = await AddSquadAdapter.ExecuteAsync(_view.Squad, cancellationToken).ConfigureAwait(true);

        if (AddSquadAdapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayError(string.Join(Environment.NewLine, AddSquadAdapter.Errors.Select(error => error.Message)));
        }
        else
        {
            _view.DisplayMessage($"Squad added for {_view.Squad.Date:MM/dd/yyyy hh:mm tt}");
            _view.Squad.Id = id!.Value;
            _view.Close();
        }
    }
}
