namespace BowlingMegabucks.TournamentManager.Tournaments;

/// <summary>
/// Represents the view model for a tournament, exposing tournament details for presentation and data transfer.
/// </summary>
internal class ViewModel : IViewModel
{
    /// <inheritdoc/>
    public TournamentId Id { get; set; }

    /// <inheritdoc/>
    public string TournamentName { get; set; } = string.Empty;

    /// <inheritdoc/>
    public DateOnly Start { get; set; }

    /// <inheritdoc/>
    public DateOnly End { get; set; }

    /// <inheritdoc/>
    public decimal EntryFee { get; set; }

    /// <inheritdoc/>
    public short Games { get; set; }

    /// <inheritdoc/>
    public decimal FinalsRatio { get; set; }

    /// <inheritdoc/>
    public decimal CashRatio { get; set; }

    /// <inheritdoc/>
    public decimal SuperSweeperCashRatio { get; set; }

    /// <inheritdoc/>
    public string BowlingCenter { get; set; } = string.Empty;

    /// <inheritdoc/>
    public bool Completed { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class from a <see cref="Models.Tournament"/>.
    /// </summary>
    /// <param name="model">The tournament model to map from.</param>
    public ViewModel(Models.Tournament model)
    {
        Id = model.Id;
        TournamentName = model.Name;
        Start = model.Start;
        End = model.End;
        EntryFee = model.EntryFee;
        Games = model.Games;
        FinalsRatio = model.FinalsRatio;
        CashRatio = model.CashRatio;
        SuperSweeperCashRatio = model.SuperSweeperCashRatio;
        BowlingCenter = model.BowlingCenter;
        Completed = model.Completed;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {
    }
}

/// <summary>
/// Represents a view model interface for a tournament.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the tournament.
    /// </summary>
    TournamentId Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the tournament.
    /// </summary>
    string TournamentName { get; set; }

    /// <summary>
    /// Gets or sets the start date of the tournament.
    /// </summary>
    DateOnly Start { get; set; }

    /// <summary>
    /// Gets or sets the end date of the tournament.
    /// </summary>
    DateOnly End { get; set; }

    /// <summary>
    /// Gets or sets the entry fee for the tournament.
    /// </summary>
    decimal EntryFee { get; set; }

    /// <summary>
    /// Gets or sets the number of games in the tournament.
    /// </summary>
    short Games { get; set; }

    /// <summary>
    /// Gets or sets the finals ratio for the tournament.
    /// </summary>
    decimal FinalsRatio { get; set; }

    /// <summary>
    /// Gets or sets the cash ratio for the tournament.
    /// </summary>
    decimal CashRatio { get; set; }

    /// <summary>
    /// Gets or sets the Super Sweeper cash ratio for the tournament.
    /// </summary>
    decimal SuperSweeperCashRatio { get; set; }

    /// <summary>
    /// Gets or sets the name of the bowling center.
    /// </summary>
    string BowlingCenter { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the tournament is completed.
    /// </summary>
    bool Completed { get; set; }
}

/// <summary>
/// Provides extension methods for the tournament view model.
/// </summary>
internal static class  ViewModelExtensions
{
    /// <summary>
    /// Converts an <see cref="IViewModel"/> to a <see cref="Models.Tournament"/> model.
    /// </summary>
    /// <param name="viewModel">The view model to convert.</param>
    /// <returns>A <see cref="Models.Tournament"/> instance with mapped properties.</returns>
    /// <remarks>
    /// This method is used to map view model data to the domain model for persistence or business logic.
    /// </remarks>
    public static Models.Tournament ToModel(this IViewModel viewModel)
        => new()
        {
            Id = viewModel.Id,
            Name = viewModel.TournamentName,
            Start = viewModel.Start,
            End = viewModel.End,
            EntryFee = viewModel.EntryFee,
            Games = viewModel.Games,
            FinalsRatio = viewModel.FinalsRatio,
            CashRatio = viewModel.CashRatio,
            SuperSweeperCashRatio = viewModel.SuperSweeperCashRatio,
            BowlingCenter = viewModel.BowlingCenter,
            Completed = viewModel.Completed,
        };
}