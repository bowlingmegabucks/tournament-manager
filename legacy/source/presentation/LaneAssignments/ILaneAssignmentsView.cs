namespace BowlingMegabucks.TournamentManager.LaneAssignments;

/// <summary>
/// Represents the view interface for lane assignments in the tournament manager.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets the tournament identifier.
    /// </summary>
    TournamentId TournamentId { get; }

    /// <summary>
    /// Gets the squad identifier.
    /// </summary>
    SquadId SquadId { get; }

    /// <summary>
    /// Gets the starting lane number.
    /// </summary>
    int StartingLane { get; }

    /// <summary>
    /// Gets the number of lanes.
    /// </summary>
    int NumberOfLanes { get; }

    /// <summary>
    /// Gets the maximum number of bowlers per lane pair.
    /// </summary>
    int MaxPerPair { get; }

    /// <summary>
    /// Builds the lanes using the provided lane identifiers.
    /// </summary>
    /// <param name="lanes">A collection of lane identifiers.</param>
    void BuildLanes(IEnumerable<string> lanes);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message.</param>
    void DisplayError(string message);

    /// <summary>
    /// Disables the view.
    /// </summary>
    void Disable();

    /// <summary>
    /// Binds the registrations to the view.
    /// </summary>
    /// <param name="registrations">A collection of registration view models.</param>
    void BindRegistrations(IEnumerable<IViewModel> registrations);

    /// <summary>
    /// Binds the lane assignments to the view.
    /// </summary>
    /// <param name="assignments">A collection of lane assignment view models.</param>
    void BindLaneAssignments(IEnumerable<IViewModel> assignments);

    /// <summary>
    /// Binds the number of entries per division to the view.
    /// </summary>
    /// <param name="entriesPerDivision">A dictionary mapping division names to entry counts.</param>
    void BindEntriesPerDivision(IDictionary<string, int> entriesPerDivision);

    /// <summary>
    /// Removes a lane assignment for the specified registration.
    /// </summary>
    /// <param name="registration">The registration view model to remove.</param>
    void RemoveLaneAssignment(IViewModel registration);

    /// <summary>
    /// Assigns a registration to a lane position.
    /// </summary>
    /// <param name="registration">The registration view model.</param>
    /// <param name="position">The lane position.</param>
    void AssignToLane(IViewModel registration, string position);

    /// <summary>
    /// Prompts the user to select a bowler for the specified tournament and squad.
    /// </summary>
    /// <param name="tournamentId">The tournament identifier.</param>
    /// <param name="squadId">The squad identifier.</param>
    /// <returns>The selected bowler identifier, or null if none selected.</returns>
    BowlerId? SelectBowler(TournamentId tournamentId, SquadId squadId);

    /// <summary>
    /// Prompts the user to create a new registration for the specified tournament and squad.
    /// </summary>
    /// <param name="tournamentId">The tournament identifier.</param>
    /// <param name="squadId">The squad identifier.</param>
    /// <returns>True if a new registration was created; otherwise, false.</returns>
    bool NewRegistration(TournamentId tournamentId, SquadId squadId);

    /// <summary>
    /// Displays a message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    void DisplayMessage(string message);

    /// <summary>
    /// Adds a lane assignment to the unassigned list.
    /// </summary>
    /// <param name="laneAssignment">The lane assignment view model.</param>
    void AddToUnassigned(IViewModel laneAssignment);

    /// <summary>
    /// Clears all lanes from the view.
    /// </summary>
    void ClearLanes();

    /// <summary>
    /// Clears all unassigned lane assignments from the view.
    /// </summary>
    void ClearUnassigned();

    /// <summary>
    /// Gets a value indicating whether staggered skip is selected.
    /// </summary>
    bool StaggeredSkipSelected { get; }

    /// <summary>
    /// Gets the number of games.
    /// </summary>
    short Games { get; }

    /// <summary>
    /// Generates recap sheets for the provided recaps.
    /// </summary>
    /// <param name="recaps">A collection of recap sheet view models.</param>
    void GenerateRecaps(IEnumerable<Scores.IRecapSheetViewModel> recaps);

    /// <summary>
    /// Deletes the registration for the specified bowler.
    /// </summary>
    /// <param name="bowlerId">The bowler identifier.</param>
    void DeleteRegistration(BowlerId bowlerId);

    /// <summary>
    /// Prompts the user to confirm an action with the specified message.
    /// </summary>
    /// <param name="message">The confirmation message.</param>
    /// <returns>True if the user confirms; otherwise, false.</returns>
    bool Confirm(string message);

    /// <summary>
    /// Clears all highlights from the view.
    /// </summary>
    void ClearHighlights();
}
