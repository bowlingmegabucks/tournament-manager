
namespace NortheastMegabuck.Sweepers.Add;
internal class Presenter
{
    private readonly IView _view;

    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveDivisionsAdapter = new Divisions.Retrieve.Adapter(config);
        _adapter = new Lazy<IAdapter>(() => new Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveDivisionsAdapter"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, Divisions.Retrieve.IAdapter mockRetrieveDivisionsAdapter, IAdapter mockAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = mockRetrieveDivisionsAdapter;
        _adapter = new Lazy<IAdapter>(() => mockAdapter);
    }

    public async Task GetDivisionsAsync(CancellationToken cancellationToken)
    {
        var divisions = await _retrieveDivisionsAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

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

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }
        
        var id = await Adapter.ExecuteAsync(_view.Sweeper, cancellationToken).ConfigureAwait(true);

        if (Adapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayError(string.Join(Environment.NewLine, Adapter.Errors.Select(error => error.Message)));
        }
        else
        {
            _view.Sweeper.Id = id!.Value;
            _view.DisplayMessage($"Sweeper added for {_view.Sweeper.Date:MM/dd/yyyy hh:mm tt}");
            _view.Close();
        }
    }
}
