namespace BowlingMegabucks.TournamentManager.Registrations.Add;

/// <summary>
/// Represents the view interface for adding a registration.
/// </summary>
public interface IView
{
    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <remarks>
    /// Use this method to show error messages in the UI.
    /// </remarks>
    void DisplayError(string message);

    /// <summary>
    /// Disables the view, preventing further user interaction.
    /// </summary>
    /// <remarks>
    /// Use this method to disable the UI when an error occurs or processing is ongoing.
    /// </remarks>
    void Disable();

    /// <summary>
    /// Binds the list of divisions to the view.
    /// </summary>
    /// <param name="divisions">A collection of division view models.</param>
    /// <remarks>
    /// Use this method to populate division selection controls.
    /// </remarks>
    void BindDivisions(IEnumerable<Divisions.IViewModel> divisions);

    /// <summary>
    /// Binds the list of squads to the view.
    /// </summary>
    /// <param name="squads">A collection of squad view models.</param>
    /// <remarks>
    /// Use this method to populate squad selection controls.
    /// </remarks>
    void BindSquads(IEnumerable<Squads.IViewModel> squads);

    /// <summary>
    /// Binds the list of squads to the view and selects the specified squad.
    /// </summary>
    /// <param name="squads">A collection of squad view models.</param>
    /// <param name="squadId">The squad identifier to select.</param>
    /// <remarks>
    /// Use this method to populate squad selection controls and pre-select a squad.
    /// </remarks>
    void BindSquads(IEnumerable<Squads.IViewModel> squads, SquadId squadId);

    /// <summary>
    /// Binds the list of sweepers to the view.
    /// </summary>
    /// <param name="sweepers">A collection of sweeper view models.</param>
    /// <remarks>
    /// Use this method to populate sweeper selection controls.
    /// </remarks>
    void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers);

    /// <summary>
    /// Binds the list of sweepers to the view and selects the specified squad.
    /// </summary>
    /// <param name="sweepers">A collection of sweeper view models.</param>
    /// <param name="squadId">The squad identifier to select.</param>
    /// <remarks>
    /// Use this method to populate sweeper selection controls and pre-select a squad.
    /// </remarks>
    void BindSweepers(IEnumerable<Sweepers.IViewModel> sweepers, SquadId squadId);

    /// <summary>
    /// Binds the specified bowler to the view.
    /// </summary>
    /// <param name="bowler">The bowler view model.</param>
    /// <remarks>
    /// Use this method to display bowler information in the UI.
    /// </remarks>
    void BindBowler(Bowlers.Retrieve.IViewModel bowler);

    /// <summary>
    /// Prompts the user to select a bowler.
    /// </summary>
    /// <returns>The selected bowler identifier, or null if none selected.</returns>
    /// <remarks>
    /// Use this method to show a bowler selection dialog or control.
    /// </remarks>
    BowlerId? SelectBowler();

    /// <summary>
    /// Closes the view.
    /// </summary>
    /// <remarks>
    /// Use this method to close the registration form or dialog.
    /// </remarks>
    void Close();

    /// <summary>
    /// Gets the selected bowler view model.
    /// </summary>
    Bowlers.IViewModel Bowler { get; }

    /// <summary>
    /// Gets the selected division identifier.
    /// </summary>
    DivisionId DivisionId { get; }

    /// <summary>
    /// Gets the average score entered by the user.
    /// </summary>
    int? Average { get; }

    /// <summary>
    /// Gets the selected squad identifiers.
    /// </summary>
    IEnumerable<SquadId> Squads { get; }

    /// <summary>
    /// Gets the selected sweeper identifiers.
    /// </summary>
    IEnumerable<SquadId> Sweepers { get; }

    /// <summary>
    /// Gets a value indicating whether the bowler is a super sweeper.
    /// </summary>
    bool SuperSweeper { get; }

    /// <summary>
    /// Determines whether the current registration data is valid.
    /// </summary>
    /// <returns>True if the data is valid; otherwise, false.</returns>
    /// <remarks>
    /// Use this method to validate user input before submitting the registration.
    /// </remarks>
    bool IsValid();

    /// <summary>
    /// Keeps the view open, preventing it from closing.
    /// </summary>
    /// <remarks>
    /// Use this method to keep the registration form open when validation fails or errors occur.
    /// </remarks>
    void KeepOpen();

    /// <summary>
    /// Displays a message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <remarks>
    /// Use this method to show informational messages in the UI.
    /// </remarks>
    void DisplayMessage(string message);
}
