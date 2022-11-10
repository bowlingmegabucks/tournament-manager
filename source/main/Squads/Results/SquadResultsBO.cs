
namespace NortheastMegabuck.Squads.Results;
internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly Tournaments.Retrieve.IBusinessLogic _retrieveTournament;
    private readonly ICalculator _squadResultCalculator;
    private readonly Scores.Retrieve.IBusinessLogic _retrieveScores;

    public BusinessLogic(IConfiguration config)
    {
        _retrieveTournament = new Tournaments.Retrieve.BusinessLogic(config);
        _squadResultCalculator = new Calculator();
        _retrieveScores = new Scores.Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRetrieveTournament"></param>
    /// <param name="mockCalculator"></param>
    /// <param name="mockRetrieveScores"></param>
    internal BusinessLogic(Tournaments.Retrieve.IBusinessLogic mockRetrieveTournament, ICalculator mockCalculator, Scores.Retrieve.IBusinessLogic mockRetrieveScores)
    {
        _retrieveTournament = mockRetrieveTournament;
        _squadResultCalculator = mockCalculator;
        _retrieveScores = mockRetrieveScores;
    }

    public IEnumerable<IGrouping<Models.Division,Models.SquadResult>> Execute(SquadId squadId)
    {
        var tournament = _retrieveTournament.Execute(squadId);

        if (_retrieveTournament.Error != null)
        {
            Error = _retrieveTournament.Error;

            return Enumerable.Empty<IGrouping<Models.Division, Models.SquadResult>>();
        }

        var scores = _retrieveScores.Execute(tournament!.Squads.Where(squad => squad.Date <= tournament.Squads.Single(s => s.Id == squadId).Date).Select(squad => squad.Id)).ToList();

        if (_retrieveScores.Error != null)
        {
            Error = _retrieveScores.Error;

            return Enumerable.Empty<IGrouping<Models.Division, Models.SquadResult>>();
        }

        return Execute(scores, tournament).Where(result => result.Squad.Id == squadId).GroupBy(result=> result.Division).ToList();
    }

    public IEnumerable<IGrouping<Models.Division, Models.SquadResult>> Execute(TournamentId tournamentId)
    {
        var tournament = _retrieveTournament.Execute(tournamentId);

        if (_retrieveTournament.Error != null)
        {
            Error = _retrieveTournament.Error;

            return Enumerable.Empty<IGrouping<Models.Division, Models.SquadResult>>();
        }

        var scores = _retrieveScores.Execute(tournament!.Squads.Select(squad => squad.Id)).ToList();

        if (_retrieveScores.Error != null)
        {
            Error = _retrieveScores.Error;

            return Enumerable.Empty<IGrouping<Models.Division, Models.SquadResult>>();
        }

        return Execute(scores, tournament).GroupBy(result=> result.Division).ToList();
    }

    private IEnumerable<Models.SquadResult> Execute(IEnumerable<Models.SquadScore> scores, Models.Tournament tournament)
    {
        var scoresBySquad = scores.GroupBy(score => score.SquadId).Select(group => new { Squad = tournament.Squads.Single(squad => squad.Id == group.Key), Scores = group }).OrderBy(scores => scores.Squad.Date).ToList();

        var results = new List<Models.SquadResult>();

        foreach (var scoresInSquad in scoresBySquad)
        {
            var scoresByDivision = scoresInSquad.Scores.GroupBy(score => score.Division).ToList();

            foreach (var scoresInDivision in scoresByDivision)
            {
                var bowlerScores = scoresInDivision.GroupBy(score => score.Bowler).Select(bowlerScore => new Models.BowlerSquadScore(bowlerScore));

                var result = _squadResultCalculator.Execute(
                    scoresInSquad.Squad,
                    scoresInDivision.Key,
                    bowlerScores.ToList(),
                    results.Where(result => result.Division == scoresInDivision.Key).SelectMany(result => result.AdvancingScores.Select(score => score.Bowler.Id)).ToList(),
                    scoresInSquad.Squad.FinalsRatio ?? tournament.FinalsRatio,
                    scoresInSquad.Squad.CashRatio ?? tournament.CashRatio);

                results.Add(result);
            }
        }

        return results;
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<IGrouping<Models.Division, Models.SquadResult>> Execute(SquadId squadId);

    IEnumerable<IGrouping<Models.Division, Models.SquadResult>> Execute(TournamentId tournamentId);
}