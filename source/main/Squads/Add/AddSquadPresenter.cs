using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Squads.Add;
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

    public void GetTournamentRatios()
    {
        var tournament = _retrieveTournamentAdapter.Execute(_view.Squad.TournamentId);

        if (_retrieveTournamentAdapter.Error != null)
        {
            _view.DisplayError(_retrieveTournamentAdapter.Error.Message);
        }
        else
        {
            _view.SetTournamentFinalsRatio(tournament!.FinalsRatio.ToString("N1"));
            _view.SetTournamentCashRatio(tournament.CashRatio.ToString("N1"));
        }
    }

    public void Execute()
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }
        
        var id = AddSquadAdapter.Execute(_view.Squad);

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
