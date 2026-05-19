namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;

/// <summary>
/// Represents a view model for a tournament registration.
/// </summary>
internal class TournamentRegistrationViewModel 
    : ITournamentRegistrationViewModel
{
    /// <inheritdoc/>
    public RegistrationId Id { get; }

    /// <inheritdoc/>
    public string FirstName { get; init; }

    /// <inheritdoc/>
    public string LastName { get; init; }

    /// <inheritdoc/>
    public BowlerId BowlerId { get; }

    /// <inheritdoc/>
    public string BowlerName { get; set; }

    /// <inheritdoc/>
    public DivisionId DivisionId { get; }

    /// <inheritdoc/>
    public string DivisionName { get; }

    /// <inheritdoc/>
    public IEnumerable<SquadId> SquadsEntered { get; }

    /// <inheritdoc/>
    public short SquadsEnteredCount
        => (short)SquadsEntered.Count();

    /// <inheritdoc/>
    public IEnumerable<SquadId> SweepersEntered { get; }

    /// <inheritdoc/>
    public short SweepersEnteredCount
        => (short)SweepersEntered.Count();

    /// <inheritdoc/>
    public bool SuperSweeperEntered { get; set; }

    /// <inheritdoc/>
    public int? Average { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TournamentRegistrationViewModel"/> class from a registration model.
    /// </summary>
    /// <param name="registration">The registration model.</param>
    public TournamentRegistrationViewModel(Models.Registration registration)
    {
        Id = registration.Id;
        FirstName = registration.Bowler.Name.First;
        LastName = registration.Bowler.Name.Last;
        BowlerName = registration.Bowler.ToString();
        BowlerId = registration.Bowler.Id;
        DivisionId = registration.Division.Id;
        DivisionName = registration.Division.Name;
        SquadsEntered = registration.Squads.Select(squad => squad.Id).ToList();
        SweepersEntered = registration.Sweepers.Select(sweeper => sweeper.Id).ToList();
        SuperSweeperEntered = registration.SuperSweeper;
        Average = registration.Average;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TournamentRegistrationViewModel"/> class for unit testing.
    /// </summary>
    internal TournamentRegistrationViewModel()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        BowlerName = string.Empty;
        DivisionName = string.Empty;
        SquadsEntered = [];
        SweepersEntered = [];
    }
}

/// <summary>
/// Represents the interface for a tournament registration view model.
/// </summary>
public interface ITournamentRegistrationViewModel
{
    /// <summary>
    /// Gets the bowler's first name.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Gets the bowler's last name.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Gets the registration identifier.
    /// </summary>
    RegistrationId Id { get; }

    /// <summary>
    /// Gets the bowler identifier.
    /// </summary>
    BowlerId BowlerId { get; }

    /// <summary>
    /// Gets or sets the bowler's display name.
    /// </summary>
    string BowlerName { get; set; }

    /// <summary>
    /// Gets the division identifier.
    /// </summary>
    DivisionId DivisionId { get; }

    /// <summary>
    /// Gets the division name.
    /// </summary>
    string DivisionName { get; }

    /// <summary>
    /// Gets the squads the bowler is entered in.
    /// </summary>
    IEnumerable<SquadId> SquadsEntered { get; }

    /// <summary>
    /// Gets the count of squads entered.
    /// </summary>
    short SquadsEnteredCount { get; }

    /// <summary>
    /// Gets the sweepers the bowler is entered in.
    /// </summary>
    IEnumerable<SquadId> SweepersEntered { get; }

    /// <summary>
    /// Gets the count of sweepers entered.
    /// </summary>
    short SweepersEnteredCount { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the bowler is a super sweeper.
    /// </summary>
    bool SuperSweeperEntered { get; set; }

    /// <summary>
    /// Gets or sets the average score.
    /// </summary>
    int? Average { get; set; }
}