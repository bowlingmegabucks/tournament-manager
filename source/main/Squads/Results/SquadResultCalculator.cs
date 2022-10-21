
namespace NortheastMegabuck.Squads.Results;

internal class Calculator : ICalculator
{
    public Models.SquadResult Execute(List<Models.BowlerSquadScore> scores, IEnumerable<BowlerId> previousAdvancersIds, decimal finalsRatio, decimal cashRatio)
    {
        var advancerCount = Convert.ToInt16(Math.Floor(scores.Count / finalsRatio));

        var casherCount = Math.Max(Convert.ToInt16(Math.Floor(scores.Count / cashRatio)) - advancerCount, 0);

        scores.Sort();

        var eligibleBowlers = scores.Where(score => !previousAdvancersIds.Contains(score.Bowler.Id)).ToList(); //Bowlers who didn't previously qualify

        eligibleBowlers.Sort();

        var advancers = eligibleBowlers.Take(advancerCount).ToList(); 

        var previousAdvancersScores = from score in scores
                                      from bowlerId in previousAdvancersIds
                                      where score.Bowler.Id == bowlerId
                                      select score;

        var cashers = previousAdvancersScores.Where(score => score.Score >= advancers.Last().Score).ToList(); //start cashers with list of would be advancers

        var scoresWithOutAdvancers = scores.Where(score => !advancers.Select(advance => advance.Bowler.Id).Contains(score.Bowler.Id)).Where(score=> !previousAdvancersIds.Contains(score.Bowler.Id)).ToList();

        var i = 0;

        while (cashers.Count < casherCount)
        {
            cashers.Add(scoresWithOutAdvancers[i++]);
        }

        var nonQualifiers = scores.Where(score => !advancers.Select(advancer => advancer.Bowler.Id).Contains(score.Bowler.Id)).Where(score => !cashers.Select(casher => casher.Bowler.Id).Contains(score.Bowler.Id)).ToList();
        nonQualifiers.Sort();

        return new Models.SquadResult
        {
            AdvancingScores = advancers,
            CashingScores = cashers,
            NonQualifyingScores = nonQualifiers
        };
    }
}

internal interface ICalculator
{
    Models.SquadResult Execute(List<Models.BowlerSquadScore> scores, IEnumerable<BowlerId> previousAdvancers, decimal finalsRatio, decimal cashRatio);
}
