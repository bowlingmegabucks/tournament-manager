using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Squads.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _getSquadsAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;
        _getSquadsAdapter = new Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockGetSquadsAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockGetSquadsAdapter)
    {
        _view = mockView;
        _getSquadsAdapter = mockGetSquadsAdapter;
    }

    public void Execute(Guid tournamentId)
    {
        var squads = _getSquadsAdapter.ForTournament(tournamentId);

        if (_getSquadsAdapter.Error != null)
        {
            _view.Disable();
            _view.DisplayError(_getSquadsAdapter.Error.Message);
        }
        else
        {
            _view.BindSquads(squads.OrderBy(squad => squad.Date));
        }
    }
}
