using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Squads.Portal;
internal class Presenter
{
    private readonly IView _view;
    private readonly Retrieve.IAdapter _retrieveSquadAdapter;

    private readonly Lazy<Complete.IAdapter> _completeSquadAdapter;
    private Complete.IAdapter CompleteSquadAdapter => _completeSquadAdapter.Value;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _retrieveSquadAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _completeSquadAdapter = new Lazy<Complete.IAdapter>(services.GetRequiredService<Complete.IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockRetrieveSquadAdapter"></param>
    /// <param name="mockCompleteSquadAdapter"></param>
    internal Presenter(IView mockView, Retrieve.IAdapter mockRetrieveSquadAdapter, Complete.IAdapter mockCompleteSquadAdapter)
    {
        _view = mockView;
        _retrieveSquadAdapter = mockRetrieveSquadAdapter;
        _completeSquadAdapter = new Lazy<Complete.IAdapter>(() => mockCompleteSquadAdapter);
    }

    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        var squad = await _retrieveSquadAdapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true);

        if (_retrieveSquadAdapter.Error != null)
        {
            _view.DisplayError(_retrieveSquadAdapter.Error.Message);

            _view.Close();

            return;
        }

        _view.SetPortalTitle($"{squad!.Date:MM/dd/yyyy hh:mmtt}");

        _view.SetStartingLane(squad.StartingLane);
        _view.SetNumberOfLanes(squad.NumberOfLanes);
        _view.SetMaxPerPair(squad.MaxPerPair);
    }

    internal async Task CompleteAsync(CancellationToken cancellationToken)
    {
        if (!_view.Confirm("Are you sure you want to complete this squad?"))
        {
            return;
        }

        await CompleteSquadAdapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true);

        if (CompleteSquadAdapter.Error != null)
        {
            _view.DisplayError(CompleteSquadAdapter.Error.Message);
        }
        else
        {
            _view.DisplayMessage("Squad successfully completed");
            _view.Close();
        }
    }
}
