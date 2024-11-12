
namespace NortheastMegabuck.Tournaments.Results;

internal class Calculator : ICalculator
{
    public Models.AtLargeResults Execute(DivisionId divisionId, ICollection<Models.SquadResult> squadResults, decimal finalsRatio)
    {
        var totalEntries = squadResults.Sum(squadResult => squadResult.Entries);
        var totalFinalsSpots = Convert.ToInt32(Math.Floor(totalEntries / finalsRatio));
        var squadFinalistCount = squadResults.Sum(squadResult => squadResult.AdvancingScores.Count());

        if (totalFinalsSpots <= squadFinalistCount) //No At Large
        {
            return new()
            {
                DivisionId = divisionId,
                AdvancingScores = Enumerable.Empty<Models.BowlerSquadScore>(),
                AdvancersWhoPreviouslyCashed = Enumerable.Empty<BowlerId>(),
            };
        }

        var finalistBowlerIds = squadResults.SelectMany(squadResult => squadResult.AdvancingScores.Select(score => score.Bowler.Id)).ToList();

        var eligibleScores = squadResults.SelectMany(squadResult => squadResult.AtLargeEligibleScores)
            .Where(score => !finalistBowlerIds.Contains(score.Bowler.Id))
            .GroupBy(score => score.Bowler)
            .Select(bowlerScore => bowlerScore.MaxBy(bowlerScore => bowlerScore.Score))
            .Order().ToList();

        var advancingScores = eligibleScores.Take(totalFinalsSpots - squadFinalistCount).ToList()!;
        var previousCashers = squadResults.SelectMany(squadResult => squadResult.CashingScores).Select(cashScore => cashScore.Bowler.Id);

        return new()
        {
            DivisionId = divisionId,
            AdvancingScores = advancingScores!,
            AdvancersWhoPreviouslyCashed = advancingScores.Where(score => previousCashers.Contains(score!.Bowler.Id)).Select(score => score!.Bowler.Id).ToList(),
        };

    }
}

internal interface ICalculator
{
    Models.AtLargeResults Execute(DivisionId divisionId, ICollection<Models.SquadResult> squadResults, decimal finalsRatio);
}