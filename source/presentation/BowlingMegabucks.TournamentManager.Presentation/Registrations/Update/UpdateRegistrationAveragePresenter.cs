using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Registrations.Update;

/// <summary>
/// Handles presentation logic for updating a registration's average.
/// </summary>
public class UpdateRegistrationAveragePresenter
{
    private readonly IAverageView _view;

    private readonly Bowlers.Retrieve.IAdapter _retrieveBowlerAdapter;
    private readonly Retrieve.IAdapter _retrieveRegistrationAdapter;
    private readonly IAdapter _updateRegistrationAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRegistrationAveragePresenter"/> class.
    /// </summary>
    /// <param name="view">The view interface.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public UpdateRegistrationAveragePresenter(IAverageView view, IServiceProvider services)
    {
        _view = view;

        _retrieveBowlerAdapter = services.GetRequiredService<Bowlers.Retrieve.IAdapter>();
        _retrieveRegistrationAdapter = services.GetRequiredService<Retrieve.IAdapter>();
        _updateRegistrationAdapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor.
    /// </summary>
    /// <param name="mockView">Mock view for testing.</param>
    /// <param name="mockBowlerAdapter">Mock bowler adapter.</param>
    /// <param name="mockRetrieveRegistrationAdapter">Mock retrieve registration adapter.</param>
    /// <param name="mockUpdateRegistrationAdapter">Mock update registration adapter.</param>
    internal UpdateRegistrationAveragePresenter(IAverageView mockView,
        Bowlers.Retrieve.IAdapter mockBowlerAdapter, Retrieve.IAdapter mockRetrieveRegistrationAdapter,
        IAdapter mockUpdateRegistrationAdapter)
    {
        _view = mockView;
        _retrieveBowlerAdapter = mockBowlerAdapter;
        _retrieveRegistrationAdapter = mockRetrieveRegistrationAdapter;
        _updateRegistrationAdapter = mockUpdateRegistrationAdapter;
    }

    /// <summary>
    /// Loads the bowler and registration data for the specified registration and updates the view.
    /// </summary>
    /// <param name="registrationId">The registration identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method retrieves bowler and registration information, handles errors, and updates the view accordingly.
    /// </remarks>
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

    /// <summary>
    /// Executes the update of the registration's average and updates the view based on the result.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method updates the registration's average, handles errors, and updates the view accordingly.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
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
