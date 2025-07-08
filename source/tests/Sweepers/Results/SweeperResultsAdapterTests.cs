
namespace NortheastMegabuck.Tests.Sweepers.Results;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Sweepers.Results.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Sweepers.Results.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Sweepers.Results.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Sweepers.Results.Adapter(_businessLogic.Object);
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
    public async Task ExecuteAsync_SquadId_BusinessLogicExecuteHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var result = await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_adapter.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_BusinessLogicExecuteSuccess_PlacingsMappedCorrectly()
    {
        var bowlerSquadScore1 = new NortheastMegabuck.Models.BowlerSquadScore(200, 201)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "200", Last = "201" } }
        };

        var bowlerSquadScore2 = new NortheastMegabuck.Models.BowlerSquadScore(250, 251)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "250", Last = "251" } }
        };

        var bowlerSquadScore3 = new NortheastMegabuck.Models.BowlerSquadScore(190, 191)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "190", Last = "191" } }
        };

        var sweeperCut = new NortheastMegabuck.Models.SweeperResult
        {
            CasherCount = 2,
            CutScore = 401,
            Scores = [bowlerSquadScore2, bowlerSquadScore1, bowlerSquadScore3]
        };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeperCut);

        var result = (await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result[0].Place, Is.EqualTo(1));
            Assert.That(result[0].Casher, Is.True);
            Assert.That(result[0].BowlerName, Is.EqualTo(bowlerSquadScore2.Bowler.ToString()));
            Assert.That(result[0].Score, Is.EqualTo(501));

            Assert.That(result[1].Place, Is.EqualTo(2));
            Assert.That(result[1].Casher, Is.True);
            Assert.That(result[1].BowlerName, Is.EqualTo(bowlerSquadScore1.Bowler.ToString()));
            Assert.That(result[1].Score, Is.EqualTo(401));

            Assert.That(result[2].Place, Is.EqualTo(3));
            Assert.That(result[2].Casher, Is.False);
            Assert.That(result[2].BowlerName, Is.EqualTo(bowlerSquadScore3.Bowler.ToString()));
            Assert.That(result[2].Score, Is.EqualTo(381));
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecuteHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var result = await _adapter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_adapter.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecuteSuccess_PlacingsMappedCorrectly()
    {
        var bowlerSquadScore1 = new NortheastMegabuck.Models.BowlerSquadScore(200, 201)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "200", Last = "201" } }
        };

        var bowlerSquadScore2 = new NortheastMegabuck.Models.BowlerSquadScore(250, 251)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "250", Last = "251" } }
        };

        var bowlerSquadScore3 = new NortheastMegabuck.Models.BowlerSquadScore(190, 191)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Name = new NortheastMegabuck.Models.PersonName { First = "190", Last = "191" } }
        };

        var sweeperCut = new NortheastMegabuck.Models.SweeperResult
        {
            CasherCount = 2,
            CutScore = 401,
            Scores = [bowlerSquadScore2, bowlerSquadScore1, bowlerSquadScore3]
        };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeperCut);

        var result = (await _adapter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result[0].Place, Is.EqualTo(1));
            Assert.That(result[0].Casher, Is.True);
            Assert.That(result[0].BowlerName, Is.EqualTo(bowlerSquadScore2.Bowler.ToString()));
            Assert.That(result[0].Score, Is.EqualTo(501));

            Assert.That(result[1].Place, Is.EqualTo(2));
            Assert.That(result[1].Casher, Is.True);
            Assert.That(result[1].BowlerName, Is.EqualTo(bowlerSquadScore1.Bowler.ToString()));
            Assert.That(result[1].Score, Is.EqualTo(401));

            Assert.That(result[2].Place, Is.EqualTo(3));
            Assert.That(result[2].Casher, Is.False);
            Assert.That(result[2].BowlerName, Is.EqualTo(bowlerSquadScore3.Bowler.ToString()));
            Assert.That(result[2].Score, Is.EqualTo(381));
        });
    }
}
