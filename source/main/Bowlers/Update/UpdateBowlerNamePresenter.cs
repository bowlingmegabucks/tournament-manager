
namespace NortheastMegabuck.Bowlers.Update;
internal class NamePresenter
{
    private readonly IBowlerNameView _view;

    private readonly Retrieve.IAdapter _retrieveBowlerAdapter;

    private readonly Lazy<IAdapter> _updateBowlerNameAdapter;
    private IAdapter UpdateBowlerNameAdapter => _updateBowlerNameAdapter.Value;

    internal NamePresenter(IBowlerNameView view, Retrieve.IAdapter retrieveBowlerAdapter, IAdapter updateBowlerNameAdapter)
    {
        _view = view;
        _retrieveBowlerAdapter = retrieveBowlerAdapter;
        _updateBowlerNameAdapter = new Lazy<IAdapter>(() => updateBowlerNameAdapter);
    }

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var bowler = await _retrieveBowlerAdapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true);

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

        await UpdateBowlerNameAdapter.ExecuteAsync(_view.Id, _view.BowlerName, cancellationToken).ConfigureAwait(true);

        if (UpdateBowlerNameAdapter.Errors.Any())
        {
            _view.DisplayErrors(UpdateBowlerNameAdapter.Errors.Select(error => error.Message));
            _view.KeepOpen();

            return;
        }

        _view.DisplayMessage($"{_view.FullName}'s name updated");
        _view.OkToClose();
    }
}
