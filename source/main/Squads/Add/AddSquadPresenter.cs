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

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveTournamentAdapter = new Tournaments.Retrieve.Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveTournamentAdapter"></param>
    internal Presenter(IView mockView, Tournaments.Retrieve.IAdapter mockRetrieveTournamentAdapter)
    {
        _view = mockView;
        _retrieveTournamentAdapter = mockRetrieveTournamentAdapter;
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
}
