using System.Globalization;

namespace NortheastMegabuck.Squads.Add;

internal class Presenter
{
    private readonly IView _view;

    private readonly Tournaments.Retrieve.IAdapter _retrieveTournamentAdapter;

    private readonly Lazy<IAdapter> _addSquadAdapter;
    private IAdapter AddSquadAdapter => _addSquadAdapter.Value;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveTournamentAdapter = new Tournaments.Retrieve.Adapter(config);
        _addSquadAdapter = new Lazy<IAdapter>(() => new Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveTournamentAdapter"></param>
    /// <param name="mockAddSquadAdapter"></param>
    internal Presenter(IView mockView, Tournaments.Retrieve.IAdapter mockRetrieveTournamentAdapter, IAdapter mockAddSquadAdapter)
    {
        _view = mockView;
        _retrieveTournamentAdapter = mockRetrieveTournamentAdapter;
        _addSquadAdapter = new Lazy<IAdapter>(() => mockAddSquadAdapter);
    }

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
