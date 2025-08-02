using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Scores;

/// <summary>
/// Represents the view model for a squad score, exposing score details for presentation and data transfer.
/// </summary>
internal class ViewModel : IViewModel
{
    /// <inheritdoc/>
    public SquadId SquadId { get; set; }

    /// <inheritdoc/>
    public BowlerId BowlerId { get; set; }

    /// <inheritdoc/>
    public short GameNumber { get; set; }

    /// <inheritdoc/>
    public int Score { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class from a <see cref="Models.SquadScore"/>.
    /// </summary>
    /// <param name="squadScore">The squad score model to map from.</param>
    public ViewModel(Models.SquadScore squadScore)
    {
        BowlerId = squadScore.Bowler.Id;
        GameNumber = squadScore.GameNumber;
        Score = squadScore.Score;
        SquadId = squadScore.SquadId;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {
    }
}

/// <summary>
/// Represents a view model interface for a squad score.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the squad.
    /// </summary>
    SquadId SquadId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the bowler.
    /// </summary>
    BowlerId BowlerId { get; set; }

    /// <summary>
    /// Gets or sets the game number for the score.
    /// </summary>
    short GameNumber { get; set; }

    /// <summary>
    /// Gets or sets the score for the game.
    /// </summary>
    int Score { get; set; }
}

internal static class ViewModelExtensions
{
    /// <summary>
    /// Converts an <see cref="IViewModel"/> to a <see cref="Models.SquadScore"/> model.
    /// </summary>
    /// <param name="viewModel">The view model to convert.</param>
    /// <returns>A <see cref="Models.SquadScore"/> instance with mapped properties.</returns>
    /// <remarks>
    /// This method is used to map view model data to the domain model for persistence or business logic.
    /// </remarks>
    public static Models.SquadScore ToModel(this IViewModel viewModel)
    {
        return new()
        {
            SquadId = viewModel.SquadId,
            Bowler = new Bowler { Id = viewModel.BowlerId },
            GameNumber = viewModel.GameNumber,
            Score = viewModel.Score,
            Division = new Division()
        };
    }
}