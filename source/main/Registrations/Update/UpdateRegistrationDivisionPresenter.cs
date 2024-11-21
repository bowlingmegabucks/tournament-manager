
namespace NortheastMegabuck.Registrations.Update;

internal class UpdateRegistrationDivisionPresenter
{
    private readonly IView _view;

    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;
    private readonly Bowlers.Retrieve.IAdapter _retrieveBowlerAdapter;

    public UpdateRegistrationDivisionPresenter(IConfiguration config, IView view)
    {
        _view = view;
        _retrieveDivisionsAdapter = new Divisions.Retrieve.Adapter(config);
        _retrieveBowlerAdapter = new Bowlers.Retrieve.Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockDivisionAdapter"></param>
    /// <param name="mockBowlerAdapter"></param>
    internal UpdateRegistrationDivisionPresenter(IView mockView, Divisions.Retrieve.IAdapter mockDivisionAdapter, Bowlers.Retrieve.IAdapter mockBowlerAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = mockDivisionAdapter;
        _retrieveBowlerAdapter = mockBowlerAdapter;
    }

    public async Task LoadAsync(TournamentId tournamentId, RegistrationId registrationId, CancellationToken cancellationToken)
    {
        var divisions = await _retrieveDivisionsAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (_retrieveDivisionsAdapter.Error is not null)
        {
            _view.DisplayError(_retrieveDivisionsAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.BindDivisions(divisions);

        var bowler = await _retrieveBowlerAdapter.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(false);

        if (_retrieveBowlerAdapter.Error is not null)
        {
            _view.DisplayError(_retrieveBowlerAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.BindBowler(bowler!);
    }
}
