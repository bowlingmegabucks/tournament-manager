
using NortheastMegabuck.Models;

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
        SquadId = squadScore.SquadId;
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
    SquadId SquadId { get; set; }

    BowlerId BowlerId { get; set; }

    short GameNumber { get; set; }

    int Score { get; set; }
}

internal static class ViewModelExtensions
{
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