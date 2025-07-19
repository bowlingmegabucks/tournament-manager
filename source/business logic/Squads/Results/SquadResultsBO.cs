namespace BowlingMegabucks.TournamentManager.Squads.Results;

/// <summary>
/// 
/// </summary>
internal class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    private readonly Tournaments.Retrieve.IBusinessLogic _retrieveTournament;
    private readonly ICalculator _squadResultCalculator;
    private readonly Scores.Retrieve.IBusinessLogic _retrieveScores;

    public BusinessLogic(Tournaments.Retrieve.IBusinessLogic retrieveTournament, ICalculator calculator, Scores.Retrieve.IBusinessLogic retrieveScores)
    {
        _retrieveTournament = retrieveTournament;
        _squadResultCalculator = calculator;
        _retrieveScores = retrieveScores;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<IGrouping<Models.Division, Models.SquadResult>>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var tournament = await _retrieveTournament.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        if (_retrieveTournament.ErrorDetail != null)
        {
            ErrorDetail = _retrieveTournament.ErrorDetail;

            return [];
        }

        var scores = (await _retrieveScores.ExecuteAsync(tournament!.Squads.Where(squad => squad.Date <= tournament.Squads.Single(s => s.Id == squadId).Date).Select(squad => squad.Id), cancellationToken).ConfigureAwait(false)).ToList();

        if (_retrieveScores.ErrorDetail != null)
        {
            ErrorDetail = _retrieveScores.ErrorDetail;

            return [];
        }

        return Execute(scores, tournament).Where(result => result.Squad.Id == squadId).GroupBy(result => result.Division).ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tournamentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<IGrouping<Models.Division, Models.SquadResult>>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var tournament = await _retrieveTournament.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        if (_retrieveTournament.ErrorDetail != null)
        {
            ErrorDetail = _retrieveTournament.ErrorDetail;

            return [];
        }

        var scores = (await _retrieveScores.ExecuteAsync(tournament!.Squads.Select(squad => squad.Id), cancellationToken).ConfigureAwait(false)).ToList();

        if (_retrieveScores.ErrorDetail != null)
        {
            ErrorDetail = _retrieveScores.ErrorDetail;

            return [];
        }

        return Execute(scores, tournament).GroupBy(result => result.Division).ToList();
    }

    private List<Models.SquadResult> Execute(IEnumerable<Models.SquadScore> scores, Models.Tournament tournament)
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
                    [.. bowlerScores],
                    results.Where(result => result.Division.Id == scoresInDivision.Key.Id).SelectMany(result => result.AdvancingScores.Select(score => score.Bowler.Id)).ToList(),
                    scoresInSquad.Squad.FinalsRatio ?? tournament.FinalsRatio,
                    scoresInSquad.Squad.CashRatio ?? tournament.CashRatio);

                results.Add(result);
            }
        }

        return results;
    }
}

/// <summary>
/// 
/// </summary>
public interface IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    Models.ErrorDetail? ErrorDetail { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<IGrouping<Models.Division, Models.SquadResult>>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tournamentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<IGrouping<Models.Division, Models.SquadResult>>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}