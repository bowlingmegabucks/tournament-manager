using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Tournaments.Seeding;

[TestFixture]
internal sealed class Calculator
{
    private NortheastMegabuck.Tournaments.Seeding.Calculator _calculator;

    [OneTimeSetUp]
    public void SetUp()
        => _calculator = new NortheastMegabuck.Tournaments.Seeding.Calculator();

    [Test]
    public void Execute_NoReEntries_CorrectSeedingReturned()
    {
        var division = new NortheastMegabuck.Models.Division();

        var advancingScore1 = new NortheastMegabuck.Models.BowlerSquadScore(297);
        var advancingScore2 = new NortheastMegabuck.Models.BowlerSquadScore(299);
        var atLargeScore1 = new NortheastMegabuck.Models.BowlerSquadScore(250);

        var nonQualifyingScore1 = new NortheastMegabuck.Models.BowlerSquadScore(150);
        var nonQualifyingScore2 = new NortheastMegabuck.Models.BowlerSquadScore(198);
        var nonQualifyingScore3 = new NortheastMegabuck.Models.BowlerSquadScore(197);
        var nonQualifyingScore4 = new NortheastMegabuck.Models.BowlerSquadScore(167);

        var squad1 = new NortheastMegabuck.Models.Squad();
        var squad2 = new NortheastMegabuck.Models.Squad();

        var squadResult1 = new NortheastMegabuck.Models.SquadResult
        {
            AdvancingScores = new[] { advancingScore1 },
            Division = division,
            NonQualifyingScores = new[] { nonQualifyingScore1, nonQualifyingScore2, atLargeScore1 },
            Squad = squad1
        };

        var squadResult2 = new NortheastMegabuck.Models.SquadResult
        {
            AdvancingScores = new[] { advancingScore2 },
            Division = division,
            NonQualifyingScores = new[] { nonQualifyingScore3, nonQualifyingScore4 },
            Squad = squad2
        };

        var tournamentResult = new NortheastMegabuck.Models.TournamentResults()
        {
            Division = division,
            SquadResults = new[] { squadResult1, squadResult2},
            AtLarge= new NortheastMegabuck.Models.AtLargeResults
            { 
                AdvancingScores= new[] { atLargeScore1 },
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

        var division = new NortheastMegabuck.Models.Division();

        var advancingScore1 = new NortheastMegabuck.Models.BowlerSquadScore(297);
        var advancingScore2 = new NortheastMegabuck.Models.BowlerSquadScore(299);
        var atLargeScore1 = new NortheastMegabuck.Models.BowlerSquadScore(250);

        var nonQualifyingScore1 = new NortheastMegabuck.Models.BowlerSquadScore(150);
        var nonQualifyingScore2 = new NortheastMegabuck.Models.BowlerSquadScore(bowlerId, 198);
        var nonQualifyingScore3 = new NortheastMegabuck.Models.BowlerSquadScore(197);
        var nonQualifyingScore4 = new NortheastMegabuck.Models.BowlerSquadScore(bowlerId, 167);

        var squad1 = new NortheastMegabuck.Models.Squad();
        var squad2 = new NortheastMegabuck.Models.Squad();

        var squadResult1 = new NortheastMegabuck.Models.SquadResult
        {
            AdvancingScores = new[] { advancingScore1 },
            Division = division,
            NonQualifyingScores = new[] { nonQualifyingScore1, nonQualifyingScore2, atLargeScore1 },
            Squad = squad1
        };

        var squadResult2 = new NortheastMegabuck.Models.SquadResult
        {
            AdvancingScores = new[] { advancingScore2 },
            Division = division,
            NonQualifyingScores = new[] { nonQualifyingScore3, nonQualifyingScore4 },
            Squad = squad2
        };

        var tournamentResult = new NortheastMegabuck.Models.TournamentResults()
        {
            Division = division,
            SquadResults = new[] { squadResult1, squadResult2 },
            AtLarge = new NortheastMegabuck.Models.AtLargeResults
            {
                AdvancingScores = new[] { atLargeScore1 },
                DivisionId = division.Id,
                AdvancersWhoPreviouslyCashed = new[] { BowlerId.New(), BowlerId.New(), BowlerId.New()}
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
