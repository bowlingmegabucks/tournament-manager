namespace NortheastMegabuck.Registrations.Update;

internal class UpdateRegistrationDivisionPresenter
{
    private readonly IView _view;

    private readonly Divisions.Retrieve.IAdapter _retrieveDivisionsAdapter;
    private readonly Bowlers.Retrieve.IAdapter _retrieveBowlerAdapter;
    private readonly Retrieve.IAdapter _retrieveRegistrationAdapter;
    private readonly IAdapter _updateRegistrationAdapter;

    public UpdateRegistrationDivisionPresenter(IConfiguration config, IView view)
    {
        _view = view;
        _retrieveDivisionsAdapter = new Divisions.Retrieve.Adapter(config);
        _retrieveBowlerAdapter = new Bowlers.Retrieve.Adapter(config);
        _retrieveRegistrationAdapter = new Retrieve.Adapter(config);
        _updateRegistrationAdapter = new Adapter(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockDivisionAdapter"></param>
    /// <param name="mockBowlerAdapter"></param>
    /// <param name="mockRetrieveRegistrationAdapter"></param>
    /// <param name="mockUpdateRegistrationAdapter"></param>
    internal UpdateRegistrationDivisionPresenter(IView mockView, Divisions.Retrieve.IAdapter mockDivisionAdapter,
        Bowlers.Retrieve.IAdapter mockBowlerAdapter, Retrieve.IAdapter mockRetrieveRegistrationAdapter,
        IAdapter mockUpdateRegistrationAdapter)
    {
        _view = mockView;
        _retrieveDivisionsAdapter = mockDivisionAdapter;
        _retrieveBowlerAdapter = mockBowlerAdapter;
        _retrieveRegistrationAdapter = mockRetrieveRegistrationAdapter;
        _updateRegistrationAdapter = mockUpdateRegistrationAdapter;
    }

    public async Task LoadAsync(TournamentId tournamentId, RegistrationId registrationId, CancellationToken cancellationToken)
    {
        var divisions = await _retrieveDivisionsAdapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(true);

        if (_retrieveDivisionsAdapter.Error is not null)
        {
            _view.DisplayError(_retrieveDivisionsAdapter.Error.Message);
            _view.Disable();

            return;
        }

        var bowler = await _retrieveBowlerAdapter.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(true);

        if (_retrieveBowlerAdapter.Error is not null)
        {
            _view.DisplayError(_retrieveBowlerAdapter.Error.Message);
            _view.Disable();

            return;
        }

        var registration = await _retrieveRegistrationAdapter.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(true);

        if (_retrieveRegistrationAdapter.Error is not null)
        {
            _view.DisplayError(_retrieveRegistrationAdapter.Error.Message);
            _view.Disable();

            return;
        }

        _view.BindDivisions(divisions);
        _view.BindBowler(bowler!);
        _view.BindRegistration(registration!);
    }

    internal async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _updateRegistrationAdapter.ExecuteAsync(_view.RegistrationId, _view.DivisionId, _view.Gender, _view.Average, _view.UsbcId, _view.DateOfBirth, cancellationToken).ConfigureAwait(true);

        if (_updateRegistrationAdapter.Errors.Any())
        {
            _view.DisplayError(_updateRegistrationAdapter.Errors.First().Message);
            _view.KeepOpen();
        }
        else
        {
            _view.DisplayMessage("Registration updated successfully");
            _view.OkToClose();
        }
    }
}
