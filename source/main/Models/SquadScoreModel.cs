
namespace NortheastMegabuck.Models;
internal class SquadScore
{
    public SquadId SquadId { get; init; }

    public BowlerId BowlerId { get; init; }

    public short GameNumber { get; init; }

    public int Score { get; init; }

    public SquadScore(Scores.Update.IViewModel viewModel)
    {
        SquadId = viewModel.SquadId;
        BowlerId = viewModel.BowlerId;
        GameNumber = viewModel.GameNumber;
        Score = viewModel.Score;
    }

    /// <summary>
    /// Unit Test Constuctor
    /// </summary>
    internal SquadScore()
    {

    }
}
