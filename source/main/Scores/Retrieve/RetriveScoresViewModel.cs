namespace NortheastMegabuck.Scores.Retrieve;

internal class ViewModel : IViewModel
{ 
    public BowlerId BowlerId { get; internal init; }

    public short GameNumber { get; internal init; }

    public int Score { get; internal init; }

    public ViewModel(Models.SquadScore squadScore)
    {
        BowlerId = squadScore.BowlerId;
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

internal interface IViewModel 
{ 
    BowlerId BowlerId { get; }

    short GameNumber { get; }

    int Score { get; }
}