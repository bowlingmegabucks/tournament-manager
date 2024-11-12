
namespace NortheastMegabuck.Tournaments.Seeding;

internal class Calculator : ICalculator
{
    public Models.TournamentFinalsSeeding Execute(Models.TournamentResults result)
    {
        var advanceringBowlerIds = result.SquadResults.SelectMany(squadResult => squadResult.AdvancingScores.Select(advancingScore => advancingScore.Bowler.Id)).Union(result.AtLarge.AdvancingScores.Select(advancingScore => advancingScore.Bowler.Id)).ToList();

        var bestScores = result.SquadResults.SelectMany(squadResult => squadResult.Scores).GroupBy(score => score.Bowler).Select(bowlerScore => bowlerScore.MaxBy(score => score.Score)!).ToList();

        var advancingScores = bestScores.Where(bestScore => advanceringBowlerIds.Contains(bestScore.Bowler.Id)).Order();
        var nonAdvancingScores = bestScores.Where(bestScore => !advanceringBowlerIds.Contains(bestScore!.Bowler.Id)).Order();

        return new()
        {
            Qualifiers = advancingScores!.ToList(),
            NonQualifiers = nonAdvancingScores.ToList(),
            Division = result.Division,
            AtLargeCashers = result.AtLarge.AdvancersWhoPreviouslyCashed
        };
    }
}

internal interface ICalculator
{
    Models.TournamentFinalsSeeding Execute(Models.TournamentResults result);
}