namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Results;

[TestFixture]
internal sealed class Calculator
{
    private TournamentManager.Tournaments.Results.Calculator _calculator;

    [OneTimeSetUp]
    public void SetUp()
        => _calculator = new TournamentManager.Tournaments.Results.Calculator();

    [Test]
    public void Execute_TotalFinalsSpotsLessThanSquadFinalistCount_ReturnsEmptyAtLargeResults()
    {
        var division = new TournamentManager.Models.Division();
        var squadResult = new TournamentManager.Models.SquadResult
        {
            AdvancingScores = Enumerable.Repeat(new TournamentManager.Models.BowlerSquadScore(), 3),
            Division = division,
            CashingScores = Enumerable.Repeat(new TournamentManager.Models.BowlerSquadScore(), 1)
        };
        var finalsRatio = 2m;
        var squadResults = new[] { squadResult };

        var result = _calculator.Execute(division.Id, squadResults, finalsRatio);

        Assert.Multiple(() =>
        {
            Assert.That(result.DivisionId, Is.EqualTo(division.Id));
            Assert.That(result.AdvancingScores, Is.Empty);
            Assert.That(result.AdvancersWhoPreviouslyCashed, Is.Empty);
        });
    }

    [Test]
    public void Execute_TotalFinalsSpotsEqualToSquadFinalistCount_ReturnsEmptyAtLargeResults()
    {
        var division = new TournamentManager.Models.Division();
        var squadResult = new TournamentManager.Models.SquadResult
        {
            AdvancingScores = Enumerable.Repeat(new TournamentManager.Models.BowlerSquadScore(), 2),
            Division = division,
            CashingScores = Enumerable.Repeat(new TournamentManager.Models.BowlerSquadScore(), 2)
        };
        var finalsRatio = 2m;
        var squadResults = new[] { squadResult };

        var result = _calculator.Execute(division.Id, squadResults, finalsRatio);

        Assert.Multiple(() =>
        {
            Assert.That(result.DivisionId, Is.EqualTo(division.Id));
            Assert.That(result.AdvancingScores, Is.Empty);
            Assert.That(result.AdvancersWhoPreviouslyCashed, Is.Empty);
        });
    }

    [Test]
    public void Execute_TotalFinalsSpotsGreaterThanSquadFinalistCount_NoAdvancersWhoPreviouslyCashed_ReturnsCorrectResults()
    {
        var division = new TournamentManager.Models.Division();
        var squadResult = new TournamentManager.Models.SquadResult
        {
            Division = division,
            AdvancingScores =
            [
                new TournamentManager.Models.BowlerSquadScore(250),
                new TournamentManager.Models.BowlerSquadScore(249)
            ],
            CashingScores = [],
            NonQualifyingScores =
            [
                new TournamentManager.Models.BowlerSquadScore(200),
                new TournamentManager.Models.BowlerSquadScore(199),
                new TournamentManager.Models.BowlerSquadScore(198),
                new TournamentManager.Models.BowlerSquadScore(197)
            ]
        };
        var finalsRatio = 2m;
        var squadResults = new[] { squadResult };

        var result = _calculator.Execute(division.Id, squadResults, finalsRatio);

        Assert.Multiple(() =>
        {
            Assert.That(result.DivisionId, Is.EqualTo(division.Id));

            Assert.That(result.AdvancingScores.Count(), Is.EqualTo(1));
            Assert.That(result.AdvancingScores.Single().Score, Is.EqualTo(200));

            Assert.That(result.AdvancersWhoPreviouslyCashed, Is.Empty);
        });
    }

    [Test]
    public void Execute_TotalFinalsSpotsGreaterThanSquadFinalistCount_AdvancersHavePreviouslyCashed_ReturnsCorrectResults()
    {
        var casherId = BowlerId.New();

        var division = new TournamentManager.Models.Division();
        var squadResult = new TournamentManager.Models.SquadResult
        {
            Division = division,
            AdvancingScores =
            [
                new TournamentManager.Models.BowlerSquadScore(250),
                new TournamentManager.Models.BowlerSquadScore(249)
            ],
            CashingScores =
            [
                new TournamentManager.Models.BowlerSquadScore(casherId, 200)
            ],
            NonQualifyingScores =
            [
                new TournamentManager.Models.BowlerSquadScore(199),
                new TournamentManager.Models.BowlerSquadScore(198),
                new TournamentManager.Models.BowlerSquadScore(197)
            ]
        };
        var finalsRatio = 2m;
        var squadResults = new[] { squadResult };

        var result = _calculator.Execute(division.Id, squadResults, finalsRatio);

        Assert.Multiple(() =>
        {
            Assert.That(result.DivisionId, Is.EqualTo(division.Id));

            Assert.That(result.AdvancingScores.Count(), Is.EqualTo(1));
            Assert.That(result.AdvancingScores.Single().Score, Is.EqualTo(200));

            Assert.That(result.AdvancersWhoPreviouslyCashed.Count(), Is.EqualTo(1));
            Assert.That(result.AdvancersWhoPreviouslyCashed.Single(), Is.EqualTo(casherId));
        });
    }
}
