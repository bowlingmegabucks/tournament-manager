
namespace NortheastMegabuck.Squads.Results;

internal class Calculator : ICalculator
{
    public Models.SquadResult Execute(Models.Squad squad, Models.Division division, List<Models.BowlerSquadScore> scores, IEnumerable<BowlerId> previousAdvancersIds, decimal finalsRatio, decimal cashRatio)
    {
        var advancerCount = Convert.ToInt16(Math.Floor(scores.Count / finalsRatio));

        var casherCount = advancerCount == 0 ? 1 : Math.Max(Convert.ToInt16(Math.Floor(scores.Count / cashRatio)) - advancerCount, 0);

        scores.Sort();

        var eligibleBowlers = scores.Where(score => !previousAdvancersIds.Contains(score.Bowler.Id)).ToList(); //Bowlers who didn't previously qualify

        eligibleBowlers.Sort();

        var advancers = eligibleBowlers.Take(advancerCount).ToList();

        var previousAdvancersScores = from score in scores
                                      from bowlerId in previousAdvancersIds
                                      where score.Bowler.Id == bowlerId
                                      select score;

        var cashers = previousAdvancersScores.Where(score => score.Score >= advancers.Last().Score).ToList(); //start cashers with list of would be advancers

        var scoresWithOutAdvancersFromPreviousOrCurrentSquad = scores.Where(score => !advancers.Select(advance => advance.Bowler.Id).Contains(score.Bowler.Id)).Where(score => !previousAdvancersIds.Contains(score.Bowler.Id)).ToList();

        var i = 0;

        while (cashers.Count < casherCount)
        {
            cashers.Add(scoresWithOutAdvancersFromPreviousOrCurrentSquad[i++]);
        }

        var nonQualifiers = scores.Where(score => !advancers.Select(advancer => advancer.Bowler.Id).Contains(score.Bowler.Id)).Where(score => !cashers.Select(casher => casher.Bowler.Id).Contains(score.Bowler.Id)).ToList();
        nonQualifiers.Sort();

        return new Models.SquadResult
        {
            Squad = squad,
            Division = division,
            AdvancingScores = advancers,
            CashingScores = cashers,
            NonQualifyingScores = nonQualifiers
        };
    }

    //todo: look into leveraging this functionality for at large
    //      only advancers carry over for at large (not cashers)
    //      bring in collection of squad cashers as well to know who to indicate that we need to get back their cash spot
    //      when doing at large, it's possible a bowler qualifies twice, make sure we only look at top score for each bowler (lookup?)
}

internal interface ICalculator
{
    /// <summary>
    /// Calculating Squad Results
    /// </summary>
    /// <param name="squad"></param>
    /// <param name="division"></param>
    /// <param name="scores"></param>
    /// <param name="previousAdvancersIds"></param>
    /// <param name="finalsRatio"></param>
    /// <param name="cashRatio"></param>
    /// <returns></returns>
    Models.SquadResult Execute(Models.Squad squad, Models.Division division, List<Models.BowlerSquadScore> scores, IEnumerable<BowlerId> previousAdvancersIds, decimal finalsRatio, decimal cashRatio);
}
