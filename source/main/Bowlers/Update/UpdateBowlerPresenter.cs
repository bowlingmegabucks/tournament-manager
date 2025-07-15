
namespace NortheastMegabuck.Bowlers.Update;
internal class Presenter
{
    private readonly IView _view;

    private readonly Retrieve.IAdapter _retrieveBowlerAdapter;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    internal Presenter(IView view, Retrieve.IAdapter retrieveBowlerAdapter, IAdapter updateBowlerAdapter)
    {
        _view = view;
        _retrieveBowlerAdapter = retrieveBowlerAdapter;
        _adapter = new Lazy<IAdapter>(() => updateBowlerAdapter);
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
