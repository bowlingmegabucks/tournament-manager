using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Registrations.Update;

internal class UpdateRegistrationAveragePresenter
{
    private readonly IAverageView _view;

    private readonly Bowlers.Retrieve.IAdapter _retrieveBowlerAdapter;
    private readonly Retrieve.IAdapter _retrieveRegistrationAdapter;
    private readonly IAdapter _updateRegistrationAdapter;

    public UpdateRegistrationAveragePresenter(IAverageView view, IServiceProvider services)
    {
        _view = view;

        _retrieveBowlerAdapter = services.GetRequiredService<Bowlers.Retrieve.IAdapter>();
        _retrieveRegistrationAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _updateRegistrationAdapter = services.GetRequiredService<IAdapter>();

    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockBowlerAdapter"></param>
    /// <param name="mockRetrieveRegistrationAdapter"></param>
    /// <param name="mockUpdateRegistrationAdapter"></param>
    internal UpdateRegistrationAveragePresenter(IAverageView mockView,
        Bowlers.Retrieve.IAdapter mockBowlerAdapter, Retrieve.IAdapter mockRetrieveRegistrationAdapter,
        IAdapter mockUpdateRegistrationAdapter)
    {
        _view = mockView;
        _retrieveBowlerAdapter = mockBowlerAdapter;
        _retrieveRegistrationAdapter = mockRetrieveRegistrationAdapter;
        _updateRegistrationAdapter = mockUpdateRegistrationAdapter;
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
