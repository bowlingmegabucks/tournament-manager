namespace BowlingMegabucks.TournamentManager.Squads;

/// <summary>
/// Represents the view model for a squad, exposing squad details for presentation and data transfer.
/// </summary>
internal class ViewModel(Models.Squad squad) : IViewModel
{
    /// <inheritdoc/>
    public SquadId Id { get; set; } = squad.Id;

    /// <inheritdoc/>
    public TournamentId TournamentId { get; set; } = squad.TournamentId;

    /// <inheritdoc/>
    public decimal? EntryFee { get; set; } = squad.EntryFee;

    /// <inheritdoc/>
    public decimal? CashRatio { get; set; } = squad.CashRatio;

    /// <inheritdoc/>
    public decimal? FinalsRatio { get; set; } = squad.FinalsRatio;

    /// <inheritdoc/>
    public DateTime SquadDate { get; set; } = squad.Date;

    /// <inheritdoc/>
    public short MaxPerPair { get; set; } = squad.MaxPerPair;

    /// <inheritdoc/>
    public short StartingLane { get; set; } = squad.StartingLane;

    /// <inheritdoc/>
    public short NumberOfLanes { get; set; } = squad.NumberOfLanes;

    /// <inheritdoc/>
    public bool Complete { get; set; } = squad.Complete;

    /// <summary>
    /// Gets or sets the number of games in the squad.
    /// </summary>
    public short NumberOfGames { get; set; }
}

/// <summary>
/// Represents a view model interface for a squad.
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
    /// Gets or sets the entry fee for the squad.
    /// </summary>
    decimal? EntryFee { get; set; }

    /// <summary>
    /// Gets or sets the cash ratio for the squad.
    /// </summary>
    decimal? CashRatio { get; set; }

    /// <summary>
    /// Gets or sets the finals ratio for the squad.
    /// </summary>
    decimal? FinalsRatio { get; set; }

    /// <summary>
    /// Gets or sets the date of the squad.
    /// </summary>
    DateTime SquadDate { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of bowlers per pair.
    /// </summary>
    short MaxPerPair { get; set; }

    /// <summary>
    /// Gets or sets the starting lane for the squad.
    /// </summary>
    short StartingLane { get; set; }

    /// <summary>
    /// Gets or sets the number of lanes for the squad.
    /// </summary>
    short NumberOfLanes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the squad is complete.
    /// </summary>
    bool Complete { get; set; }
}

/// <summary>
/// Provides extension methods for the squad view model.
/// </summary>
internal static class ViewModelExtensions
{
    /// <summary>
    /// Converts an <see cref="IViewModel"/> to a <see cref="Models.Squad"/> model.
    /// </summary>
    /// <param name="viewModel">The view model to convert.</param>
    /// <returns>A <see cref="Models.Squad"/> instance with mapped properties.</returns>
    /// <remarks>
    /// This method is used to map view model data to the domain model for persistence or business logic.
    /// </remarks>
    public static Models.Squad ToModel(this IViewModel viewModel)
    {
        return new Models.Squad
        {
            Id = viewModel.Id,
            TournamentId = viewModel.TournamentId,
            CashRatio = viewModel.CashRatio,
            FinalsRatio = viewModel.FinalsRatio,
            Date = viewModel.SquadDate,
            MaxPerPair = viewModel.MaxPerPair,
            StartingLane = viewModel.StartingLane,
            NumberOfLanes = viewModel.NumberOfLanes,
            Complete = viewModel.Complete,
            EntryFee = viewModel.EntryFee
        };
    }
}