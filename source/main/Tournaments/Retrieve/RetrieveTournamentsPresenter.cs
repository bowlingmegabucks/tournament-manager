using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tournaments.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _adapter = new Lazy<IAdapter>(() => new Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = new Lazy<IAdapter>(() => mockAdapter);
    }

    public void Execute()
    {
        var tournaments = Adapter.Execute();

        if (Adapter.ErrorDetail != null)
        {
            _view.DisplayErrorMessage(Adapter.ErrorDetail.Message);
            _view.DisableOpenTournament();
        }
        else if (!tournaments.Any())
        {
            _view.DisableOpenTournament();
        }
        else
        {
            _view.BindTournaments(tournaments.ToList());
        }
    }
}
