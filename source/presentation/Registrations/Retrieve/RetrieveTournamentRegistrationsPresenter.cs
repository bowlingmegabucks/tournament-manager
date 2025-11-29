using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;

/// <summary>
/// Handles presentation logic for retrieving and managing tournament registrations.
/// </summary>
public class TournamentRegistrationsPresenter
{
    private readonly ITournamentRegistrationsView _view;

    private readonly IAdapter _registrationsAdapter;
    private readonly Squads.Retrieve.IAdapter _squadsAdapter;
    private readonly Sweepers.Retrieve.IAdapter _sweepersAdapter;

    private readonly Lazy<Delete.IAdapter> _deleteAdapter;
    private Delete.IAdapter DeleteAdapter => _deleteAdapter.Value;

    private readonly Lazy<Update.IAdapter> _updateAdapter;
    private Update.IAdapter UpdateAdapter => _updateAdapter.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="TournamentRegistrationsPresenter"/> class.
    /// </summary>
    /// <param name="view">The view interface.</param>
    /// <param name="services">The service provider for dependency injection.</param>
    public TournamentRegistrationsPresenter(ITournamentRegistrationsView view, IServiceProvider services)
    {
        _view = view;

        _registrationsAdapter = services.GetRequiredService<IAdapter>();
        _squadsAdapter = services.GetRequiredService<Squads.Retrieve.IAdapter>();
        _sweepersAdapter = services.GetRequiredService<Sweepers.Retrieve.IAdapter>();

        _deleteAdapter = new Lazy<Delete.IAdapter>(services.GetRequiredService<Delete.IAdapter>);
        _updateAdapter = new Lazy<Update.IAdapter>(services.GetRequiredService<Update.IAdapter>);
    }

    /// <summary>
    /// Unit Test Constructor.
    /// </summary>
    /// <param name="mockView">Mock view for testing.</param>
    /// <param name="mockRegistrationsAdapter">Mock registrations adapter.</param>
    /// <param name="mockSquadsAdapter">Mock squads adapter.</param>
    /// <param name="mockSweepersAdapter">Mock sweepers adapter.</param>
    /// <param name="mockDeleteAdapter">Mock delete adapter.</param>
    /// <param name="mockUpdateAdapter">Mock update adapter.</param>
    internal TournamentRegistrationsPresenter(ITournamentRegistrationsView mockView, IAdapter mockRegistrationsAdapter, Squads.Retrieve.IAdapter mockSquadsAdapter, Sweepers.Retrieve.IAdapter mockSweepersAdapter, Delete.IAdapter mockDeleteAdapter, Update.IAdapter mockUpdateAdapter)
    {
        _view = mockView;
        _registrationsAdapter = mockRegistrationsAdapter;
        _squadsAdapter = mockSquadsAdapter;
        _sweepersAdapter = mockSweepersAdapter;
        _deleteAdapter = new Lazy<Delete.IAdapter>(() => mockDeleteAdapter);
        _updateAdapter = new Lazy<Update.IAdapter>(() => mockUpdateAdapter);
    }

    /// <summary>
    /// Loads and binds tournament registrations, squads, and sweepers to the view.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method retrieves registrations, squads, and sweepers, handles errors, and updates the view accordingly.
    /// </remarks>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var registrations = await _registrationsAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);
        var squads = await _squadsAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);
        var sweepers = await _sweepersAdapter.ExecuteAsync(_view.TournamentId, cancellationToken).ConfigureAwait(true);

        var errors = new[] { _registrationsAdapter.Error, _squadsAdapter.Error, _sweepersAdapter.Error }.Where(error => error != null).ToList();

        if (errors.Count > 0)
        {
            _view.DisplayError(string.Join(Environment.NewLine, errors.Select(error => error!.Message).Distinct()));

            return;
        }

        const string dateFormat = "MM/dd/yy hh:mmtt";
        var squadsDictionary = squads.ToDictionary(squad => squad.Id, squad => squad.SquadDate.ToString(dateFormat, CultureInfo.CurrentCulture));
        var squadEntries = squadsDictionary.ToDictionary(squad => squad.Value, squad => registrations.Count(registration => registration.SquadsEntered.Contains(squad.Key)));
        _view.SetSquadEntries(squadEntries);
        _view.BindSquadDates(squadsDictionary);

        var sweepersDictionary = sweepers.ToDictionary(sweeper => sweeper.Id, sweeper => sweeper.SweeperDate.ToString(dateFormat, CultureInfo.CurrentCulture));
        var sweeperEntries = sweepersDictionary.ToDictionary(sweeper => sweeper.Value, sweeper => registrations.Count(registration => registration.SweepersEntered.Contains(sweeper.Key)));
        sweeperEntries.Add("Super Sweeper", registrations.Count(registration => registration.SuperSweeperEntered));
        _view.SetSweeperEntries(sweeperEntries);
        _view.BindSquadDates(sweepersDictionary);

        _view.BindRegistrations(registrations.OrderBy(registration => registration.LastName).ThenBy(registration => registration.FirstName));

        var divisionEntries = registrations.GroupBy(registration => registration.DivisionName).ToDictionary(g => g.Key, g => g.Sum(r => r.SquadsEnteredCount));
        _view.SetDivisionEntries(divisionEntries);
    }

    /// <summary>
    /// Deletes a registration and updates the view.
    /// </summary>
    /// <param name="id">The registration identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method prompts for confirmation, deletes the registration, handles errors, and updates the view accordingly.
    /// </remarks>
    public async Task DeleteAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        if (!_view.Confirm("Are you sure you want to delete this bowler's entire registration?"))
        {
            return;
        }

        await DeleteAdapter.ExecuteAsync(id, cancellationToken).ConfigureAwait(true);

        if (DeleteAdapter.Error != null)
        {
            _view.DisplayError(DeleteAdapter.Error.Message);
        }
        else
        {
            _view.RemoveRegistration(id);
        }
    }

    /// <summary>
    /// Updates the bowler's name in the view.
    /// </summary>
    /// <param name="id">The bowler identifier.</param>
    /// <remarks>
    /// This method prompts the user to update the bowler's name and updates the view if a new name is provided.
    /// </remarks>
    public void UpdateBowlerName(BowlerId id)
    {
        var updatedName = _view.UpdateBowlerName(id);

        if (updatedName == null)
        {
            return;
        }

        _view.UpdateBowlerName(updatedName!);
    }

    /// <summary>
    /// Adds a Super Sweeper entry to a registration and updates the view.
    /// </summary>
    /// <param name="id">The registration identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method prompts for confirmation, adds a Super Sweeper entry, handles errors, and updates the view accordingly.
    /// </remarks>
    public async Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        if (!_view.Confirm("Are you sure you want to add a Super Sweeper entry to this registration?"))
        {
            return;
        }

        await UpdateAdapter.AddSuperSweeperAsync(id, cancellationToken).ConfigureAwait(false);

        if (UpdateAdapter.Errors.Any())
        {
            _view.DisplayError(UpdateAdapter.Errors.First().Message);

            return;
        }

        _view.DisplayMessage("Super Sweeper has been added");
        _view.UpdateBowlerSuperSweeper(id, true);
    }

    /// <summary>
    /// Removes a Super Sweeper entry from a registration and updates the view.
    /// </summary>
    /// <param name="id">The registration identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <remarks>
    /// This method prompts for confirmation, removes a Super Sweeper entry, handles errors, and updates the view accordingly.
    /// </remarks>
    public async Task RemoveSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        if (!_view.Confirm("Are you sure you want to remove the Super Sweeper entry from this registration?"))
        {
            return;
        }

        await UpdateAdapter.RemoveSuperSweeperAsync(id, cancellationToken).ConfigureAwait(false);

        if (UpdateAdapter.Errors.Any())
        {
            _view.DisplayError(UpdateAdapter.Errors.First().Message);

            return;
        }

        _view.DisplayMessage("Super Sweeper has been removed");
        _view.UpdateBowlerSuperSweeper(id, false);
    }
}