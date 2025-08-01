using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Divisions.Add;

/// <summary>
/// 
/// </summary>
public class Presenter
{
    private readonly Lazy<Retrieve.IAdapter> _retrieveDivisionsAdapter;
    private Retrieve.IAdapter RetrieveDivisionsAdapter => _retrieveDivisionsAdapter.Value;

    private readonly Lazy<IAdapter> _addDivisionAdapter;
    private IAdapter AddDivisionAdapter => _addDivisionAdapter.Value;

    private readonly IView _view;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="view"></param>
    /// <param name="services"></param>
    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _retrieveDivisionsAdapter = new Lazy<Retrieve.IAdapter>(services.GetRequiredService<Retrieve.IAdapter>);
        _addDivisionAdapter = new Lazy<IAdapter>(services.GetRequiredService<IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveDivisionsAdapter"></param>
    /// <param name="mockAddDivisionAdapter"></param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveDivisionsAdapter, IAdapter mockAddDivisionAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = new Lazy<Retrieve.IAdapter>(() => mockRetrieveDivisionsAdapter);
        _addDivisionAdapter = new Lazy<IAdapter>(() => mockAddDivisionAdapter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task GetNextDivisionNumberAsync(CancellationToken cancellationToken)
    {
        var divisions = await RetrieveDivisionsAdapter.ExecuteAsync(_view.Division.TournamentId, cancellationToken).ConfigureAwait(true);

        if (RetrieveDivisionsAdapter.Error != null)
        {
            _view.DisplayErrors([RetrieveDivisionsAdapter.Error.Message]);
        }
        else
        {
            _view.Division.Number = (short)(divisions.Count() + 1);
        }
    }

    /// <summary>
    /// Executes the operation to add a division asynchronously, handling validation, errors, and user feedback.
    /// </summary>
    /// <remarks>This method validates the division data before attempting to add it. If validation fails, the
    /// view remains open, and no further action is taken. If the operation encounters errors, the errors are displayed
    /// to the user, and the view remains open. On success, a confirmation message is displayed, the division's ID is
    /// updated, and the view is closed.</remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns></returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!_view.IsValid())
        {
            _view.KeepOpen();
            return;
        }

        var id = await AddDivisionAdapter.ExecuteAsync(_view.Division, cancellationToken).ConfigureAwait(true);

        if (AddDivisionAdapter.Errors.Any())
        {
            _view.KeepOpen();
            _view.DisplayErrors(AddDivisionAdapter.Errors.Select(e => e.Message));
        }
        else
        {
            _view.DisplayMessage($"{_view.Division.DivisionName} division added.");
            _view.Division.Id = id!.Value;
            _view.Close();
        }
    }
}
