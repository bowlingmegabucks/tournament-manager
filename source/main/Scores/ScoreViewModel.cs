
namespace NortheastMegabuck.Scores;

internal class ViewModel : IViewModel
{
    public SquadId SquadId { get; set; }

    public BowlerId BowlerId { get; set; }

    public short GameNumber { get; set; }

    public int Score { get; set; }

    public ViewModel(Models.SquadScore squadScore)
    {
        BowlerId = squadScore.Bowler.Id;
        GameNumber = squadScore.GameNumber;
        Score = squadScore.Score;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {

    }
}

public interface IViewModel
{
    SquadId SquadId { get; set; }

    BowlerId BowlerId { get; set; }

    short GameNumber { get; set; }

    int Score { get; set; }
}
