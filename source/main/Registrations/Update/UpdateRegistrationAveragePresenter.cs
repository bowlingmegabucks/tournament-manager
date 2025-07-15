namespace NortheastMegabuck.Registrations.Update;

internal class UpdateRegistrationAveragePresenter
{
    private readonly IAverageView _view;

    private readonly Bowlers.Retrieve.IAdapter _retrieveBowlerAdapter;
    private readonly Retrieve.IAdapter _retrieveRegistrationAdapter;
    private readonly IAdapter _updateRegistrationAdapter;

    internal UpdateRegistrationAveragePresenter(IAverageView view,
        Bowlers.Retrieve.IAdapter bowlerAdapter, Retrieve.IAdapter retrieveRegistrationAdapter,
        IAdapter updateRegistrationAdapter)
    {
        _view = view;
        _retrieveBowlerAdapter = bowlerAdapter;
        _retrieveRegistrationAdapter = retrieveRegistrationAdapter;
        _updateRegistrationAdapter = updateRegistrationAdapter;
    }

    public async Task LoadAsync(RegistrationId registrationId, CancellationToken cancellationToken)
    {
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

        _view.BindBowler(bowler!);
        _view.BindRegistration(registration!);
    }

    internal async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _updateRegistrationAdapter.ExecuteAsync(_view.RegistrationId, _view.Average, cancellationToken).ConfigureAwait(true);

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
