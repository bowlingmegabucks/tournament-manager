
namespace NortheastMegabuck.Sweepers.Results;
internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly Lazy<Retrieve.IBusinessLogic> _retrieveSweeper;
    private Retrieve.IBusinessLogic RetrieveSweeper => _retrieveSweeper.Value;

    private readonly Lazy<Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;
    private Tournaments.Retrieve.IBusinessLogic RetrieveTournament => _retrieveTournament.Value;

    private readonly Scores.Retrieve.IBusinessLogic _retrieveScores;

    public BusinessLogic(IConfiguration config)
    {
        _retrieveSweeper = new Lazy<Retrieve.IBusinessLogic>(()=>  new Retrieve.BusinessLogic(config));
        _retrieveTournament = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => new Tournaments.Retrieve.BusinessLogic(config));
        _retrieveScores = new Scores.Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRetrieveSweeper"></param>
    /// <param name="mockRetrieveScores"></param>
    internal BusinessLogic(Retrieve.IBusinessLogic mockRetrieveSweeper, Tournaments.Retrieve.IBusinessLogic mockRetrieveTournament, Scores.Retrieve.IBusinessLogic mockRetrieveScores)
    {
        _retrieveSweeper = new Lazy<Retrieve.IBusinessLogic>(() => mockRetrieveSweeper);
        _retrieveTournament = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => mockRetrieveTournament);
        _retrieveScores = mockRetrieveScores;
    }

    public Models.SweeperResult? Execute(SquadId squadId)
    {
        var sweeper = RetrieveSweeper.Execute(squadId);

        if (RetrieveSweeper.Error != null)
        {
            Error = RetrieveSweeper.Error;

            return null;
        }

        var scores = _retrieveScores.Execute(squadId);

        return Execute(scores, sweeper!.CashRatio);
    }

    public Models.SweeperResult? Execute(TournamentId tournamentId)
    {
        var tournament = RetrieveTournament.Execute(tournamentId);

        if (RetrieveTournament.Error != null)
        {
            Error = RetrieveTournament.Error;

            return null;
        }

        var scores = _retrieveScores.Execute(tournament!.Sweepers.Select(sweeper=> sweeper.Id));

        //todo: this might be a different field
        return Execute(scores, tournament!.SuperSweeperCashRatio);
    }

    private Models.SweeperResult? Execute(IEnumerable<Models.SquadScore> scores, decimal cashRatio)
    {
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

        var bowlerScores = scores.GroupBy(score => score.Bowler).Select(bowlerScore => new Models.BowlerSquadScore(bowlerScore)).Order().ToList();

        var casherCount = Math.Max(Convert.ToInt16(Math.Floor(bowlerScores.Count / cashRatio)), (short)1);

        return new Models.SweeperResult
        {
            Scores = bowlerScores,
            CasherCount = casherCount,
            CutScore = bowlerScores[casherCount - 1].Score
        };
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Models.SweeperResult? Execute(SquadId squadId);

    Models.SweeperResult? Execute(TournamentId tournamentId);
}