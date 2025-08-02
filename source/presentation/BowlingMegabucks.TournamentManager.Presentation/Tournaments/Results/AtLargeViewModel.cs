using System.Globalization;

namespace BowlingMegabucks.TournamentManager.Tournaments.Results;

internal class AtLargeViewModel(short place, Models.BowlerSquadScore result, bool previousCasher) 
    : IAtLargeViewModel
{
    /// <inheritdoc/>
    public short Place { get; } = place;

    /// <inheritdoc/>
    public string BowlerName { get; } = result.Bowler.ToString();

    /// <inheritdoc/>
    public string DivisionName { get; } = result.Division.Name;

    /// <inheritdoc/>
    public string SquadDate { get; } = result.SquadDate.ToString("MM/dd hh:mm tt", CultureInfo.InvariantCulture);

    /// <inheritdoc/>
    public bool PreviousCasher { get; } = previousCasher;

    /// <inheritdoc/>
    public int Score { get; } = result.Score;

    /// <inheritdoc/>
    public int ScratchScore { get; } = result.ScratchScore;

    /// <inheritdoc/>
    public int HighGame { get; } = result.HighGame;

    /// <inheritdoc/>
    public int HighGameScratch { get; } = result.HighGameScratch;
}

/// <summary>
/// Represents the view model for an at-large tournament result.
/// </summary>
public interface IAtLargeViewModel
{
    /// <summary>
    /// Gets the place/rank of the bowler.
    /// </summary>
    short Place { get; }

    /// <summary>
    /// Gets the name of the bowler.
    /// </summary>
    string BowlerName { get; }

    /// <summary>
    /// Gets the name of the division.
    /// </summary>
    string DivisionName { get; }

    /// <summary>
    /// Gets the date and time of the squad.
    /// </summary>
    string SquadDate { get; }

    /// <summary>
    /// Gets a value indicating whether the bowler was a previous casher.
    /// </summary>
    bool PreviousCasher { get; }

    /// <summary>
    /// Gets the total score.
    /// </summary>
    int Score { get; }

    /// <summary>
    /// Gets the scratch score.
    /// </summary>
    int ScratchScore { get; }

    /// <summary>
    /// Gets the highest game score.
    /// </summary>
    int HighGame { get; }

    /// <summary>
    /// Gets the highest scratch game score.
    /// </summary>
    int HighGameScratch { get; }
}