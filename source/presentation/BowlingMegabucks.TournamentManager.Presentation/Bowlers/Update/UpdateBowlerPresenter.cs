
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Bowlers.Update;

/// <summary>
/// 
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly Retrieve.IAdapter _retrieveBowlerAdapter;

    private readonly Lazy<IAdapter> _adapter;
    private IAdapter Adapter => _adapter.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="view"></param>
    /// <param name="services"></param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _retrieveBowlerAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _adapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
