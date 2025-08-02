namespace BowlingMegabucks.TournamentManager.Sweepers;

/// <summary>
/// Represents the view model for a sweeper squad, exposing sweeper details for presentation and data transfer.
/// </summary>
internal class ViewModel 
    : IViewModel
{
    /// <inheritdoc/>
    public SquadId Id { get; set; }

    /// <inheritdoc/>
    public TournamentId TournamentId { get; set; }

    /// <inheritdoc/>
    public decimal EntryFee { get; set; }

    /// <inheritdoc/>
    public short Games { get; set; }

    /// <inheritdoc/>
    public decimal CashRatio { get; set; }

    /// <inheritdoc/>
    public DateTime Date { get; set; }

    /// <inheritdoc/>
    public short MaxPerPair { get; set; }

    /// <inheritdoc/>
    public short StartingLane { get; set; }

    /// <inheritdoc/>
    public short NumberOfLanes { get; set; }

    /// <inheritdoc/>
    public bool Complete { get; set; }

    /// <inheritdoc/>
    public IDictionary<DivisionId, int?> Divisions { get; set; } = new Dictionary<DivisionId, int?>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class from a <see cref="Models.Sweeper"/>.
    /// </summary>
    /// <param name="sweeper">The sweeper model to map from.</param>
    public ViewModel(Models.Sweeper sweeper)
    {
        Id = sweeper.Id;
        TournamentId = sweeper.TournamentId;
        EntryFee = sweeper.EntryFee;
        Games = sweeper.Games;
        CashRatio = sweeper.CashRatio;
        Date = sweeper.Date;
        MaxPerPair = sweeper.MaxPerPair;
        StartingLane = sweeper.StartingLane;
        NumberOfLanes = sweeper.NumberOfLanes;
        Complete = sweeper.Complete;
        Divisions = sweeper.Divisions.ToDictionary(division => division.Key, division => division.Value);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {
    }
}

/// <summary>
/// Represents a view model interface for a sweeper squad.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the squad.
    /// </summary>
    SquadId Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the tournament.
    /// </summary>
    TournamentId TournamentId { get; set; }

    /// <summary>
    /// Gets or sets the entry fee for the sweeper.
    /// </summary>
    decimal EntryFee { get; set; }

    /// <summary>
    /// Gets or sets the number of games in the sweeper.
    /// </summary>
    short Games { get; set; }

    /// <summary>
    /// Gets or sets the cash ratio for the sweeper.
    /// </summary>
    decimal CashRatio { get; set; }

    /// <summary>
    /// Gets or sets the date of the sweeper.
    /// </summary>
    DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of bowlers per pair.
    /// </summary>
    short MaxPerPair { get; set; }

    /// <summary>
    /// Gets or sets the starting lane for the sweeper.
    /// </summary>
    short StartingLane { get; set; }

    /// <summary>
    /// Gets or sets the number of lanes for the sweeper.
    /// </summary>
    short NumberOfLanes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sweeper is complete.
    /// </summary>
    bool Complete { get; set; }

    /// <summary>
    /// Gets the dictionary of divisions and their associated values.
    /// </summary>
    IDictionary<BowlingMegabucks.TournamentManager.DivisionId, int?> Divisions { get; }
}

/// <summary>
/// Provides extension methods for the sweeper view model.
/// </summary>
internal static class ViewModelExtensions
{
    /// <summary>
    /// Converts an <see cref="IViewModel"/> to a <see cref="Models.Sweeper"/> model.
    /// </summary>
    /// <param name="viewModel">The view model to convert.</param>
    /// <returns>A <see cref="Models.Sweeper"/> instance with mapped properties.</returns>
    /// <remarks>
    /// This method is used to map view model data to the domain model for persistence or business logic.
    /// </remarks>
    public static Models.Sweeper ToModel(this IViewModel viewModel)
    {
        return new Models.Sweeper
        {
            Id = viewModel.Id,
            TournamentId = viewModel.TournamentId,
            EntryFee = viewModel.EntryFee,
            Games = viewModel.Games,
            CashRatio = viewModel.CashRatio,
            Date = viewModel.Date,
            MaxPerPair = viewModel.MaxPerPair,
            StartingLane = viewModel.StartingLane,
            NumberOfLanes = viewModel.NumberOfLanes,
            Complete = viewModel.Complete,
            Divisions = viewModel.Divisions
        };
    }
}