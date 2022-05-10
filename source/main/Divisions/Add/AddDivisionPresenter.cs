using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic.Divisions.Add;
internal class Presenter
{
    private readonly Lazy<Retrieve.IAdapter> _retrieveDivisionsAdapter;
    private Retrieve.IAdapter RetrieveDivisionsAdapter => _retrieveDivisionsAdapter.Value;

    private readonly Lazy<IAdapter> _addDivisionAdapter;
    private IAdapter AddDivisionAdapter => _addDivisionAdapter.Value;

    private readonly IView _view;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveDivisionsAdapter = new Lazy<Retrieve.IAdapter>(() => new Retrieve.Adapter(config));
        _addDivisionAdapter = new Lazy<IAdapter>(() => new Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveDivisionsAdapter"></param>
    /// <param name="mockAddDivisionAdapter"></param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveDivisionsAdapter, IAdapter mockAddDivisionAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = new Lazy<Retrieve.IAdapter>(() => mockRetrieveDivisionsAdapter);
        _addDivisionAdapter = new Lazy<IAdapter>(() => mockAddDivisionAdapter);
    }

    public void GetNextDivisionNumber()
    {
        var divisions = RetrieveDivisionsAdapter.ForTournament(_view.Division.TournamentId);

        if (RetrieveDivisionsAdapter.Errors.Any())
        {
            _view.DisplayErrors(RetrieveDivisionsAdapter.Errors.Select(e => e.Message));
        }
        else
        {
            _view.Division.Number = (short)(divisions.Count() + 1);
        }
    }

    public void Execute()
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = AddDivisionAdapter.Execute(_view.Division);

        if (AddDivisionAdapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayErrors(AddDivisionAdapter.Errors.Select(e => e.Message));
        }
        else
        {
            _view.DisplayMessage($"{_view.Division.DivisionName} division added.");
            _view.Division.Id = id!.Value;
            _view.Close();
        }
    }
}
