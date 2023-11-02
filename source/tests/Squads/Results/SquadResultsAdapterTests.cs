
namespace NortheastMegabuck.Tests.Squads.Results;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Squads.Results.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Squads.Results.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Squads.Results.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Squads.Results.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_BusinessLogicHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var result = await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);

            Assert.That(_adapter.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_BusinessLogicNoError_ResultsReturnedCorrectly()
    {
        var squad = new NortheastMegabuck.Models.Squad { Id = SquadId.New(), Date = new DateTime(2000, 1, 2, 9, 30, 0, DateTimeKind.Unspecified) };

        var division1 = new NortheastMegabuck.Models.Division { Name = "division1" };
        var division2 = new NortheastMegabuck.Models.Division { Name = "division2" };

        var division1AdvancerScore = new NortheastMegabuck.Models.BowlerSquadScore(200, 200, 200)
        {
            SquadId = squad.Id,
            Division = division1,
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "division1AdvancerScore" } }
        };

        var division1CasherScore1 = new NortheastMegabuck.Models.BowlerSquadScore(199, 199, 199)
        {
            SquadId = squad.Id,
            Division = division1,
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "division1CasherScore1" } }
        };

        var division1CasherScore2 = new NortheastMegabuck.Models.BowlerSquadScore(198, 198, 198)
        {
            SquadId = squad.Id,
            Division = division1,
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "division1CasherScore2" } }
        };

        var division1NonQualifierScore1 = new NortheastMegabuck.Models.BowlerSquadScore(197, 197, 197)
        {
            SquadId = squad.Id,
            Division = division1,
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "division1NonQualifierScore1" } }
        };

        var division1NonQualifierScore2 = new NortheastMegabuck.Models.BowlerSquadScore(196, 196, 196)
        {
            SquadId = squad.Id,
            Division = division1,
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Name = new NortheastMegabuck.Models.PersonName { First = "division1NonQualifierScore2" }
            }
        };

        var division1NonQualifierScore3 = new NortheastMegabuck.Models.BowlerSquadScore(195, 195, 195)
        {
            SquadId = squad.Id,
            Division = division1,
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Name = new NortheastMegabuck.Models.PersonName { First = "division1NonQualifierScore3" }
            }
        };

        var division2AdvancerScore = new NortheastMegabuck.Models.BowlerSquadScore(200, 200, 200)
        {
            SquadId = squad.Id,
            Division = division2,
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Name = new NortheastMegabuck.Models.PersonName { First = "division2AdvancerScore" }
            }
        };

        var division2CasherScore1 = new NortheastMegabuck.Models.BowlerSquadScore(199, 199, 199)
        {
            SquadId = squad.Id,
            Division = division2,
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Name = new NortheastMegabuck.Models.PersonName { First = "division2CasherScore1" }
            }
        };

        var division2CasherScore2 = new NortheastMegabuck.Models.BowlerSquadScore(198, 198, 198)
        {
            SquadId = squad.Id,
            Division = division2,
            Bowler = new NortheastMegabuck.Models.Bowler
            {
                Name = new NortheastMegabuck.Models.PersonName { First = "division2CasherScore2" }
            }
        };

        var division2NonQualifierScore1 = new NortheastMegabuck.Models.BowlerSquadScore(197, 197, 197)
        {
            SquadId = squad.Id,
            Division = division2,
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "division2NonQualifierScore1" } }
        };

        var division2NonQualifierScore2 = new NortheastMegabuck.Models.BowlerSquadScore(196, 196, 196)
        {
            SquadId = squad.Id,
            Division = division2,
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "division2NonQualifierScore2" } }
        };

        var division2NonQualifierScore3 = new NortheastMegabuck.Models.BowlerSquadScore(195, 195, 195)
        {
            SquadId = squad.Id,
            Division = division2,
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "division2NonQualifierScore3" } }
        };

        var division1Result = new NortheastMegabuck.Models.SquadResult
        {
            AdvancingScores = new[] { division1AdvancerScore },
            CashingScores = new[] { division1CasherScore1, division1CasherScore2 },
            NonQualifyingScores = new[] { division1NonQualifierScore1, division1NonQualifierScore2, division1NonQualifierScore3 },
            Squad = squad,
            Division = division1
        };

        var division2Result = new NortheastMegabuck.Models.SquadResult
        {
            AdvancingScores = new[] { division2AdvancerScore },
            CashingScores = new[] { division2CasherScore1, division2CasherScore2 },
            NonQualifyingScores = new[] { division2NonQualifierScore1, division2NonQualifierScore2, division2NonQualifierScore3 },
            Squad = squad,
            Division = division2
        };

        var results = new[] { division1Result, division2Result }.GroupBy(result => result.Division);

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(results);

        var actual = await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            var division1Actual = actual.First();

            Assert.That(division1Actual.Key, Is.EqualTo(division1.Name));
            Assert.That(division1Actual.ToList()[0].Place, Is.EqualTo(1));
            Assert.That(division1Actual.ToList()[0].BowlerName.Trim(), Is.EqualTo("division1AdvancerScore"));
            Assert.That(division1Actual.ToList()[1].Place, Is.EqualTo(2));
            Assert.That(division1Actual.ToList()[1].BowlerName.Trim(), Is.EqualTo("division1CasherScore1"));
            Assert.That(division1Actual.ToList()[2].Place, Is.EqualTo(3));
            Assert.That(division1Actual.ToList()[2].BowlerName.Trim(), Is.EqualTo("division1CasherScore2"));
            Assert.That(division1Actual.ToList()[3].Place, Is.EqualTo(4));
            Assert.That(division1Actual.ToList()[3].BowlerName.Trim(), Is.EqualTo("division1NonQualifierScore1"));
            Assert.That(division1Actual.ToList()[4].Place, Is.EqualTo(5));
            Assert.That(division1Actual.ToList()[4].BowlerName.Trim(), Is.EqualTo("division1NonQualifierScore2"));
            Assert.That(division1Actual.ToList()[5].Place, Is.EqualTo(6));
            Assert.That(division1Actual.ToList()[5].BowlerName.Trim(), Is.EqualTo("division1NonQualifierScore3"));

            var division2Actual = actual.Last();

            Assert.That(division2Actual.Key, Is.EqualTo(division2.Name));
            Assert.That(division2Actual.ToList()[0].Place, Is.EqualTo(1));
            Assert.That(division2Actual.ToList()[0].BowlerName.Trim(), Is.EqualTo("division2AdvancerScore"));
            Assert.That(division2Actual.ToList()[1].Place, Is.EqualTo(2));
            Assert.That(division2Actual.ToList()[1].BowlerName.Trim(), Is.EqualTo("division2CasherScore1"));
            Assert.That(division2Actual.ToList()[2].Place, Is.EqualTo(3));
            Assert.That(division2Actual.ToList()[2].BowlerName.Trim(), Is.EqualTo("division2CasherScore2"));
            Assert.That(division2Actual.ToList()[3].Place, Is.EqualTo(4));
            Assert.That(division2Actual.ToList()[3].BowlerName.Trim(), Is.EqualTo("division2NonQualifierScore1"));
            Assert.That(division2Actual.ToList()[4].Place, Is.EqualTo(5));
            Assert.That(division2Actual.ToList()[4].BowlerName.Trim(), Is.EqualTo("division2NonQualifierScore2"));
            Assert.That(division2Actual.ToList()[5].Place, Is.EqualTo(6));
            Assert.That(division2Actual.ToList()[5].BowlerName.Trim(), Is.EqualTo("division2NonQualifierScore3"));
        });
    }
}
