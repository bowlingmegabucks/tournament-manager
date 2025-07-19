using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Sweepers.Retrieve;
internal class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _getSweepersAdapter;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _getSweepersAdapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockGetSweepersAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockGetSweepersAdapter)
    {
        _view = mockView;
        _getSweepersAdapter = mockGetSweepersAdapter;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var sweepers = await _getSweepersAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

        if (_getSweepersAdapter.Error != null)
        {
            _view.Disable();
            _view.DisplayError(_getSweepersAdapter.Error.Message);
        }
        else
        {
            _view.BindSweepers(sweepers.OrderBy(sweeper => sweeper.Date));
        }
    }

    internal async Task AddSweeperAsync(CancellationToken cancellationToken)
    {
        var sweeperId = _view.AddSweeper(_view.TournamentId);

        if (sweeperId != null)
        {
            await _view.RefreshSweepersAsync(cancellationToken).ConfigureAwait(true);
        }
    }
}
