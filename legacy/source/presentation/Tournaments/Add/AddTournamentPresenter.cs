using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Tournaments.Add;
/// <summary>
/// Handles the presentation logic for adding a tournament.
/// </summary>
public class Presenter
{
    private readonly IView _view;

    private readonly IAdapter _adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="view">The view interface for the presenter.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _adapter = services.GetRequiredService<IAdapter>();
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

    /// <summary>
    /// Executes the add tournament workflow asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Validates the view, attempts to add the tournament, and updates the view based on the result.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = await _adapter.ExecuteAsync(_view.Tournament, cancellationToken).ConfigureAwait(true);

        if (_adapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayErrors(_adapter.Errors.Select(e => e.Message).ToList());
        }
        else
        {
            _view.DisplayMessage($"{_view.Tournament.TournamentName} successfully added");
            _view.Tournament.Id = id!.Value;
            _view.OkToClose();
            _view.Close();
        }
    }
}
