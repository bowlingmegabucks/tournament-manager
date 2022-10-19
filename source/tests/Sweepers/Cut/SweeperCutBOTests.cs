
namespace NortheastMegabuck.Tests.Sweepers.Cut;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic> _retrieveSweeper;
    private Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic> _retrieveScores;

    private NortheastMegabuck.Sweepers.Cut.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _retrieveSweeper = new Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic>();
        _retrieveScores = new Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic>();

        _businessLogic = new NortheastMegabuck.Sweepers.Cut.BusinessLogic(_retrieveSweeper.Object, _retrieveScores.Object);
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _businessLogic.Execute(squadId);

        _retrieveSweeper.Verify(retrieveSweeper => retrieveSweeper.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecuteHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveSweeper.SetupGet(retrieveSweeper => retrieveSweeper.Error).Returns(error);

        var result = _businessLogic.Execute(SquadId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.Error, Is.EqualTo(error));

            _retrieveScores.Verify(retrieveScores => retrieveScores.Execute(It.IsAny<SquadId>()), Times.Never);
        });
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _businessLogic.Execute(squadId);

        _retrieveScores.Verify(retrieveScores => retrieveScores.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveScores.SetupGet(retrieveScores => retrieveScores.Error).Returns(error);

        var result = _businessLogic.Execute(SquadId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteReturnsNoScores_NoScoreFlow()
    {
        var result = _businessLogic.Execute(SquadId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("No scores entered for sweeper"));
        });
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteSuccess_SweeperCutReturnedWithCorrectFields()
    {
        var squadId = SquadId.New();

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2};

        _retrieveScores.Setup(retrieveScores => retrieveScores.Execute(It.IsAny<SquadId>())).Returns(squadScores);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            CashRatio = 2
        };

        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.Execute(It.IsAny<SquadId>())).Returns(sweeper);

        var result = _businessLogic.Execute(squadId);

        Assert.Multiple(() =>
        {
            Assert.That(result.Scores.ToList()[0].Bowler.Id, Is.EqualTo(bowler4), "1st Wrong");
            Assert.That(result.Scores.ToList()[1].Bowler.Id, Is.EqualTo(bowler3), "2nd Wrong");
            Assert.That(result.Scores.ToList()[2].Bowler.Id, Is.EqualTo(bowler2), "3rd Wrong");
            Assert.That(result.Scores.ToList()[3].Bowler.Id, Is.EqualTo(bowler1), "4th Wrong");

            Assert.That(result.SquadId, Is.EqualTo(squadId));

            Assert.That(result.CasherCount, Is.EqualTo(2));

            Assert.That(result.CutScore, Is.EqualTo(411));
        });
    }
}
