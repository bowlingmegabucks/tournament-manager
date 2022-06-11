
namespace NewEnglandClassic.Sweepers.Add;
internal class Presenter
{
    private readonly IView _view;

    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveDivisionsAdapter = new Divisions.Retrieve.Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveDivisionsAdapter"></param>
    internal Presenter(IView mockView, Divisions.Retrieve.IAdapter mockRetrieveDivisionsAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = mockRetrieveDivisionsAdapter;
    }

    public void GetDivisions()
    {
        var divisions = _retrieveDivisionsAdapter.ForTournament(_view.TournamentId);

        if (_retrieveDivisionsAdapter.Error != null)
        {
            _view.DisplayError(_retrieveDivisionsAdapter.Error.Message);
            _view.Disable();
        }
        else
        {
            _view.BindDivisions(divisions);
        }
    }
}
