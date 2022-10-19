
namespace NortheastMegabuck.Sweepers.Cut;
internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly Lazy<Retrieve.IBusinessLogic> _retrieveSweeper;
    private Retrieve.IBusinessLogic RetrieveSweeper => _retrieveSweeper.Value;

    private readonly Scores.Retrieve.IBusinessLogic _retrieveScores;

    public BusinessLogic(IConfiguration config)
    {
        _retrieveSweeper = new Lazy<Retrieve.IBusinessLogic>(()=>  new Retrieve.BusinessLogic(config));
        _retrieveScores = new Scores.Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRetrieveSweeper"></param>
    /// <param name="mockRetrieveScores"></param>
    internal BusinessLogic(Retrieve.IBusinessLogic mockRetrieveSweeper, Scores.Retrieve.IBusinessLogic mockRetrieveScores)
    {
        _retrieveSweeper = new Lazy<Retrieve.IBusinessLogic>(() => mockRetrieveSweeper);
        _retrieveScores = mockRetrieveScores;
    }

    public Models.SweeperCut? Execute(SquadId squadId)
    {
        var sweeper = RetrieveSweeper.Execute(squadId);

        if (RetrieveSweeper.Error != null)
        {
            Error = RetrieveSweeper.Error;

            return null;
        }

        var scores = _retrieveScores.Execute(squadId);

        if (_retrieveScores.Error != null)
        {
            Error = _retrieveScores.Error;

            return null;
        }

        if (!scores.Any())
        {
            Error = new Models.ErrorDetail("No scores entered for sweeper");

            return null;
        }

        var bowlerScores = scores.GroupBy(score => score.Bowler).Select(bowlerScore => new Models.BowlerSquadScore(bowlerScore)).ToList();
        bowlerScores.Sort();

        var casherCount = Convert.ToInt16(Math.Floor(bowlerScores.Count / sweeper!.CashRatio));

        return new Models.SweeperCut
        {
            Scores = bowlerScores,
            CasherCount = casherCount,
            SquadId = squadId,
            CutScore = bowlerScores[casherCount - 1].GameScores.Sum(score => score.Value)
        };
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Models.SweeperCut? Execute(SquadId squadId);
}