
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers.Update;
internal class NamePresenter
{
    private readonly IBowlerNameView _view;

    private readonly Retrieve.IAdapter _retrieveBowlerAdapter;

    private readonly Lazy<IAdapter> _updateBowlerNameAdapter;
    private IAdapter UpdateBowlerNameAdapter => _updateBowlerNameAdapter.Value;

    public NamePresenter(IBowlerNameView view, IServiceProvider services)
    {
        _view = view;

        _retrieveBowlerAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _updateBowlerNameAdapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveBowlerAdapter"></param>
    /// <param name="mockUpdateBowlerNameAdapter"></param>
    internal NamePresenter(IBowlerNameView mockView, Retrieve.IAdapter mockRetrieveBowlerAdapter, IAdapter mockUpdateBowlerNameAdapter)
    {
        _view = mockView;
        _retrieveBowlerAdapter = mockRetrieveBowlerAdapter;
        _updateBowlerNameAdapter = new Lazy<IAdapter>(() => mockUpdateBowlerNameAdapter);
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
