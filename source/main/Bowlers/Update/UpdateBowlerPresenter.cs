
namespace NortheastMegabuck.Bowlers.Update;
internal class Presenter
{
    private readonly IView _view;

    private readonly Retrieve.IAdapter _retrieveBowlerAdapter;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;

        _retrieveBowlerAdapter = new Retrieve.Adapter(config);
        _adapter = new Lazy<IAdapter>(() => new Adapter(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveBowlerAdapter"></param>
    /// <param name="mockUpdateBowlerAdapter"></param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveBowlerAdapter, IAdapter mockUpdateBowlerAdapter)
    {
        _view = mockView;
        _retrieveBowlerAdapter = mockRetrieveBowlerAdapter;
        _adapter = new Lazy<IAdapter>(() => mockUpdateBowlerAdapter);
    }

    public async Task LoadAsync(BowlerId id, CancellationToken cancellationToken)
    {
        var bowler = await _retrieveBowlerAdapter.ExecuteAsync(id, cancellationToken).ConfigureAwait(true);

        if (_retrieveBowlerAdapter.Error != null)
        {
            _view.DisplayError(_retrieveBowlerAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.Bind(bowler!);
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();

            return;
        }

        await Adapter.ExecuteAsync(_view.Bowler, cancellationToken).ConfigureAwait(true);

        if (Adapter.Errors.Any())
        {
            _view.DisplayErrors(Adapter.Errors.Select(error => error.Message));
            _view.KeepOpen();

            return;
        }

        _view.DisplayMessage($"Bowler updated");
        _view.OkToClose();
    }
}
