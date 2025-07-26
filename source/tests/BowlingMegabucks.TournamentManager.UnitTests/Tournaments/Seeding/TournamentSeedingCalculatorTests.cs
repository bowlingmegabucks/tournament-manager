namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Seeding;

[TestFixture]
internal sealed class Calculator
{
    private TournamentManager.Tournaments.Seeding.Calculator _calculator;

    [OneTimeSetUp]
    public void SetUp()
        => _calculator = new TournamentManager.Tournaments.Seeding.Calculator();

    [Test]
    public void Execute_NoReEntries_CorrectSeedingReturned()
    {
        var division = new TournamentManager.Models.Division();

        var advancingScore1 = new TournamentManager.Models.BowlerSquadScore(297);
        var advancingScore2 = new TournamentManager.Models.BowlerSquadScore(299);
        var atLargeScore1 = new TournamentManager.Models.BowlerSquadScore(250);

        var nonQualifyingScore1 = new TournamentManager.Models.BowlerSquadScore(150);
        var nonQualifyingScore2 = new TournamentManager.Models.BowlerSquadScore(198);
        var nonQualifyingScore3 = new TournamentManager.Models.BowlerSquadScore(197);
        var nonQualifyingScore4 = new TournamentManager.Models.BowlerSquadScore(167);

        var squad1 = new TournamentManager.Models.Squad();
        var squad2 = new TournamentManager.Models.Squad();

        var squadResult1 = new TournamentManager.Models.SquadResult
        {
            AdvancingScores = [advancingScore1],
            Division = division,
            NonQualifyingScores = [nonQualifyingScore1, nonQualifyingScore2, atLargeScore1],
            Squad = squad1
        };

        var squadResult2 = new TournamentManager.Models.SquadResult
        {
            AdvancingScores = [advancingScore2],
            Division = division,
            NonQualifyingScores = [nonQualifyingScore3, nonQualifyingScore4],
            Squad = squad2
        };

        var tournamentResult = new TournamentManager.Models.TournamentResults()
        {
            Division = division,
            SquadResults = [squadResult1, squadResult2],
            AtLarge = new TournamentManager.Models.AtLargeResults
            {
                AdvancingScores = [atLargeScore1],
                DivisionId = division.Id
            }
        };

        var seeding = _calculator.Execute(tournamentResult);

        Assert.Multiple(() =>
        {
            Assert.That(seeding.Division, Is.EqualTo(division));

            Assert.That(seeding.Qualifiers.Count(), Is.EqualTo(3));
            Assert.That(seeding.NonQualifiers.Count(), Is.EqualTo(4));

            Assert.That(seeding.Qualifiers.ToList()[0].Score, Is.EqualTo(299));
            Assert.That(seeding.Qualifiers.ToList()[1].Score, Is.EqualTo(297));
            Assert.That(seeding.Qualifiers.ToList()[2].Score, Is.EqualTo(250));

            Assert.That(seeding.NonQualifiers.ToList()[0].Score, Is.EqualTo(198));
            Assert.That(seeding.NonQualifiers.ToList()[1].Score, Is.EqualTo(197));
            Assert.That(seeding.NonQualifiers.ToList()[2].Score, Is.EqualTo(167));
            Assert.That(seeding.NonQualifiers.ToList()[3].Score, Is.EqualTo(150));
        });
    }

    [Test]
    public void Execute_ReEntries_CorrectSeedingReturned()
    {
        var bowlerId = BowlerId.New();

        var division = new TournamentManager.Models.Division();

        var advancingScore1 = new TournamentManager.Models.BowlerSquadScore(297);
        var advancingScore2 = new TournamentManager.Models.BowlerSquadScore(299);
        var atLargeScore1 = new TournamentManager.Models.BowlerSquadScore(250);

        var nonQualifyingScore1 = new TournamentManager.Models.BowlerSquadScore(150);
        var nonQualifyingScore2 = new TournamentManager.Models.BowlerSquadScore(bowlerId, 198);
        var nonQualifyingScore3 = new TournamentManager.Models.BowlerSquadScore(197);
        var nonQualifyingScore4 = new TournamentManager.Models.BowlerSquadScore(bowlerId, 167);

        var squad1 = new TournamentManager.Models.Squad();
        var squad2 = new TournamentManager.Models.Squad();

        var squadResult1 = new TournamentManager.Models.SquadResult
        {
            AdvancingScores = [advancingScore1],
            Division = division,
            NonQualifyingScores = [nonQualifyingScore1, nonQualifyingScore2, atLargeScore1],
            Squad = squad1
        };

        var squadResult2 = new TournamentManager.Models.SquadResult
        {
            AdvancingScores = [advancingScore2],
            Division = division,
            NonQualifyingScores = [nonQualifyingScore3, nonQualifyingScore4],
            Squad = squad2
        };

        var tournamentResult = new TournamentManager.Models.TournamentResults()
        {
            Division = division,
            SquadResults = [squadResult1, squadResult2],
            AtLarge = new TournamentManager.Models.AtLargeResults
            {
                AdvancingScores = [atLargeScore1],
                DivisionId = division.Id,
                AdvancersWhoPreviouslyCashed = [BowlerId.New(), BowlerId.New(), BowlerId.New()]
            }
        };

        var seeding = _calculator.Execute(tournamentResult);

        Assert.Multiple(() =>
        {
            Assert.That(seeding.Division, Is.EqualTo(division));

            Assert.That(seeding.Qualifiers.Count(), Is.EqualTo(3));
            Assert.That(seeding.NonQualifiers.Count(), Is.EqualTo(3));

            Assert.That(seeding.Qualifiers.ToList()[0].Score, Is.EqualTo(299));
            Assert.That(seeding.Qualifiers.ToList()[1].Score, Is.EqualTo(297));
            Assert.That(seeding.Qualifiers.ToList()[2].Score, Is.EqualTo(250));

            Assert.That(seeding.NonQualifiers.ToList()[0].Score, Is.EqualTo(198));
            Assert.That(seeding.NonQualifiers.ToList()[1].Score, Is.EqualTo(197));
            Assert.That(seeding.NonQualifiers.ToList()[2].Score, Is.EqualTo(150));

            Assert.That(seeding.AtLargeCashers, Is.EqualTo(tournamentResult.AtLarge.AdvancersWhoPreviouslyCashed));
        });
    }
}
