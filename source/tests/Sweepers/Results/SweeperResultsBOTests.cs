
namespace NortheastMegabuck.Tests.Sweepers.Results;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic> _retrieveSweeper;
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;
    private Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic> _retrieveScores;

    private NortheastMegabuck.Sweepers.Results.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _retrieveSweeper = new Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic>();
        _retrieveTournament = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();
        _retrieveScores = new Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic>();

        _businessLogic = new NortheastMegabuck.Sweepers.Results.BusinessLogic(_retrieveSweeper.Object, _retrieveTournament.Object, _retrieveScores.Object);
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecute_CalledCorrectly()
    {
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.Execute(It.IsAny<SquadId>())).Returns(new NortheastMegabuck.Models.Sweeper());

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
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.Execute(It.IsAny<SquadId>())).Returns(new NortheastMegabuck.Models.Sweeper());

        var squadId = SquadId.New();

        _businessLogic.Execute(squadId);

        _retrieveScores.Verify(retrieveScores => retrieveScores.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteHasError_ErrorFlow()
    {
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.Execute(It.IsAny<SquadId>())).Returns(new NortheastMegabuck.Models.Sweeper());

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
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.Execute(It.IsAny<SquadId>())).Returns(new NortheastMegabuck.Models.Sweeper());

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

            Assert.That(result.CasherCount, Is.EqualTo(2));

            Assert.That(result.CutScore, Is.EqualTo(411));
        });
    }

    [Test]
    public void Execute_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteSuccess_CashRatioMakesCasherCountLessThan1_SweeperCutReturnedWithCorrectFields()
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
            CashRatio = 500
        };

        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.Execute(It.IsAny<SquadId>())).Returns(sweeper);

        var result = _businessLogic.Execute(squadId);

        Assert.Multiple(() =>
        {
            Assert.That(result.Scores.ToList()[0].Bowler.Id, Is.EqualTo(bowler4), "1st Wrong");

            Assert.That(result.CasherCount, Is.EqualTo(1));

            Assert.That(result.CutScore, Is.EqualTo(415));
        });
    }

    [Test]
    public void Execute_TournamentId_RetrieveTournamentExecute_CalledCorrectly()
    {
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(new NortheastMegabuck.Models.Tournament());

        var tournamentId = TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _retrieveTournament.Verify(retrieveTournament => retrieveTournament.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_RetrieveTournamentExecuteHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveTournament.SetupGet(retrieveTournament => retrieveTournament.Error).Returns(error);

        var result = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.Error, Is.EqualTo(error));

            _retrieveScores.Verify(retrieveScores => retrieveScores.SuperSweeper(It.IsAny<TournamentId>()), Times.Never);
        });
    }

    [Test]
    public void Execute_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeper_CalledCorrectly()
    {
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(new NortheastMegabuck.Models.Tournament());

        var tournamentId = TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _retrieveScores.Verify(retrieveScores => retrieveScores.SuperSweeper(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeperHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveScores.SetupGet(retrieveScores => retrieveScores.Error).Returns(error);

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(new NortheastMegabuck.Models.Tournament());

        var result = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public void Execute_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeperReturnsNoScores_NoScoreFlow()
    {
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(new NortheastMegabuck.Models.Tournament());

        var result = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("No scores entered for sweeper"));
        });
    }

    [Test]
    public void Execute_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeperSuccess_SweeperCutReturnedWithCorrectFields()
    {
        var tournamentId = TournamentId.New();

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2};

        _retrieveScores.Setup(retrieveScores => retrieveScores.SuperSweeper(It.IsAny<TournamentId>())).Returns(squadScores);

        var tournament = new NortheastMegabuck.Models.Tournament
        {
            SuperSweeperCashRatio = 2
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var result = _businessLogic.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(result.Scores.ToList()[0].Bowler.Id, Is.EqualTo(bowler4), "1st Wrong");
            Assert.That(result.Scores.ToList()[1].Bowler.Id, Is.EqualTo(bowler3), "2nd Wrong");
            Assert.That(result.Scores.ToList()[2].Bowler.Id, Is.EqualTo(bowler2), "3rd Wrong");
            Assert.That(result.Scores.ToList()[3].Bowler.Id, Is.EqualTo(bowler1), "4th Wrong");

            Assert.That(result.CasherCount, Is.EqualTo(2));

            Assert.That(result.CutScore, Is.EqualTo(411));
        });
    }

    [Test]
    public void Execute_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveSuperSweeperSuccess_CashRatioMakesCasherCountLessThan1_SweeperCutReturnedWithCorrectFields()
    {

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2};

        _retrieveScores.Setup(retrieveScores => retrieveScores.SuperSweeper(It.IsAny<TournamentId>())).Returns(squadScores);

        var tournament = new NortheastMegabuck.Models.Tournament
        {
            SuperSweeperCashRatio = 500
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var result = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result.Scores.ToList()[0].Bowler.Id, Is.EqualTo(bowler4), "1st Wrong");

            Assert.That(result.CasherCount, Is.EqualTo(1));

            Assert.That(result.CutScore, Is.EqualTo(415));
        });
    }
}
