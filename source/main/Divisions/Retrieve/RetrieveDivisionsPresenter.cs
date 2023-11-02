namespace NortheastMegabuck.Divisions.Retrieve;

internal class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _adapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;
        _adapter = new Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var divisions = await _adapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);
            _view.Disable();
        }
        else
        {
            _view.BindDivisions(divisions);
        }
    }

    internal async Task AddDivisionAsync(CancellationToken cancellationToken)
    {
        var divisionId = _view.AddDivision(_view.TournamentId);

        if (divisionId != null)
        {
            await _view.RefreshDivisionsAsync(cancellationToken).ConfigureAwait(true);
        }
    }
}
