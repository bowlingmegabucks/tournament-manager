
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Sweepers.Results;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<TournamentManager.Sweepers.Retrieve.IBusinessLogic> _retrieveSweeper;
    private Mock<IQueryHandler<GetRegistrationByIdQuery, TournamentManager.Models.Tournament>> _retrieveTournament;
    private Mock<TournamentManager.Scores.Retrieve.IBusinessLogic> _retrieveScores;

    private TournamentManager.Sweepers.Results.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _retrieveSweeper = new Mock<TournamentManager.Sweepers.Retrieve.IBusinessLogic>();
        _retrieveTournament = new Mock<IQueryHandler<GetRegistrationByIdQuery, TournamentManager.Models.Tournament>>();
        _retrieveScores = new Mock<TournamentManager.Scores.Retrieve.IBusinessLogic>();

        _businessLogic = new TournamentManager.Sweepers.Results.BusinessLogic(_retrieveSweeper.Object, _retrieveTournament.Object, _retrieveScores.Object);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RetrieveSweeperExecute_CalledCorrectly()
    {
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TournamentManager.Models.Sweeper());

        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _retrieveSweeper.Verify(retrieveSweeper => retrieveSweeper.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RetrieveSweeperExecuteHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveSweeper.SetupGet(retrieveSweeper => retrieveSweeper.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));

            _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecute_CalledCorrectly()
    {
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TournamentManager.Models.Sweeper());

        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(new[] { squadId }, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteHasError_ErrorFlow()
    {
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TournamentManager.Models.Sweeper());

        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveScores.SetupGet(retrieveScores => retrieveScores.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteReturnsNoScores_NoScoreFlow()
    {
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TournamentManager.Models.Sweeper());

        var result = await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo("No scores entered for sweeper"));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteSuccess_SweeperCutReturnedWithCorrectFields()
    {
        var squadId = SquadId.New();

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2};

        _retrieveScores.Setup(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadScores);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            CashRatio = 2
        };

        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeper);

        var result = await _businessLogic.ExecuteAsync(squadId, default).ConfigureAwait(false);

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
    public async Task ExecuteAsync_SquadId_RetrieveSweeperExecuteSuccess_RetrieveScoresExecuteSuccess_CashRatioMakesCasherCountLessThan1_SweeperCutReturnedWithCorrectFields()
    {
        var squadId = SquadId.New();

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new TournamentManager.Models.SquadScore
        {
            SquadId = squadId,
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2};

        _retrieveScores.Setup(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadScores);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            CashRatio = 500
        };

        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeper);

        var result = await _businessLogic.ExecuteAsync(squadId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result.Scores.ToList()[0].Bowler.Id, Is.EqualTo(bowler4), "1st Wrong");

            Assert.That(result.CasherCount, Is.EqualTo(1));

            Assert.That(result.CutScore, Is.EqualTo(415));
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecute_CalledCorrectly()
    {
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TournamentManager.Models.Tournament());

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _retrieveTournament.Verify(retrieveTournament => retrieveTournament.HandleAsync(It.Is<GetRegistrationByIdQuery>(query => query.Id == tournamentId), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecuteHasError_ErrorFlow()
    {
        var error = Error.Conflict();
        _retrieveTournament.Setup(query => query.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(error);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(error.Description));

            _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresExecute_CalledCorrectly()
    {
        var tournament = new TournamentManager.Models.Tournament
        {
            Sweepers = [new TournamentManager.Models.Sweeper { Id = SquadId.New() }, new TournamentManager.Models.Sweeper { Id = SquadId.New() }]
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.Is<IEnumerable<SquadId>>(squadIds => squadIds.Contains(tournament.Sweepers.First().Id) && squadIds.Contains(tournament.Sweepers.Last().Id) && squadIds.Count() == 2), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeperHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveScores.SetupGet(retrieveScores => retrieveScores.ErrorDetail).Returns(error);

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TournamentManager.Models.Tournament());

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeperReturnsNoScores_NoScoreFlow()
    {
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TournamentManager.Models.Tournament());

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo("No scores entered for sweeper"));
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeperSuccess_SweeperCutReturnedWithCorrectFields()
    {
        var tournamentId = TournamentId.New();

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2};

        var superSweeperBowlers = new[] { bowler1, bowler2, bowler3, bowler4 };
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.SuperSweeperBowlersAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(superSweeperBowlers);

        _retrieveScores.Setup(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadScores);

        var tournament = new TournamentManager.Models.Tournament
        {
            SuperSweeperCashRatio = 2
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var result = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

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
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveSuperSweeperSuccess_CashRatioMakesCasherCountLessThan1_SweeperCutReturnedWithCorrectFields()
    {

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2};

        _retrieveScores.Setup(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadScores);

        var tournament = new TournamentManager.Models.Tournament
        {
            SuperSweeperCashRatio = 500
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var superSweeperBowlers = new[] { bowler1, bowler2, bowler3, bowler4 };
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.SuperSweeperBowlersAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(superSweeperBowlers);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result.Scores.ToList()[0].Bowler.Id, Is.EqualTo(bowler4), "1st Wrong");

            Assert.That(result.CasherCount, Is.EqualTo(1));

            Assert.That(result.CutScore, Is.EqualTo(415));
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RetrieveTournamentExecuteSuccess_RetrieveScoresSuperSweeperSuccess_BowlerNotInSuperSweepers_SweeperCutReturnedWithCorrectFields()
    {
        var tournamentId = TournamentId.New();

        var bowler1 = BowlerId.New();
        var bowler1SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 1,
            Score = 201
        };
        var bowler1SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler1 },
            GameNumber = 2,
            Score = 202
        };

        var bowler2 = BowlerId.New();
        var bowler2SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 1,
            Score = 203
        };
        var bowler2SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler2 },
            GameNumber = 2,
            Score = 204
        };

        var bowler3 = BowlerId.New();
        var bowler3SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 1,
            Score = 205
        };
        var bowler3SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler3 },
            GameNumber = 2,
            Score = 206
        };

        var bowler4 = BowlerId.New();
        var bowler4SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 1,
            Score = 207
        };
        var bowler4SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler4 },
            GameNumber = 2,
            Score = 208
        };

        var bowler5 = BowlerId.New();
        var bowler5SquadScore1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler5 },
            GameNumber = 1,
            Score = 300
        };
        var bowler5SquadScore2 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowler5 },
            GameNumber = 2,
            Score = 300
        };

        var squadScores = new[] { bowler1SquadScore1, bowler1SquadScore2,
                                  bowler2SquadScore1, bowler2SquadScore2,
                                  bowler3SquadScore1, bowler3SquadScore2,
                                  bowler4SquadScore1, bowler4SquadScore2,
                                  bowler5SquadScore1, bowler5SquadScore2};

        var superSweeperBowlers = new[] { bowler1, bowler2, bowler3, bowler4 };
        _retrieveSweeper.Setup(retrieveSweeper => retrieveSweeper.SuperSweeperBowlersAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(superSweeperBowlers);

        _retrieveScores.Setup(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadScores);

        var tournament = new TournamentManager.Models.Tournament
        {
            SuperSweeperCashRatio = 2
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var result = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result.Scores.ToList()[0].Bowler.Id, Is.EqualTo(bowler4), "1st Wrong");
            Assert.That(result.Scores.ToList()[1].Bowler.Id, Is.EqualTo(bowler3), "2nd Wrong");
            Assert.That(result.Scores.ToList()[2].Bowler.Id, Is.EqualTo(bowler2), "3rd Wrong");
            Assert.That(result.Scores.ToList()[3].Bowler.Id, Is.EqualTo(bowler1), "4th Wrong");
            Assert.That(result.Scores.All(score => score.Bowler.Id != bowler5), "Bowler 5 found in Results");

            Assert.That(result.CasherCount, Is.EqualTo(2));

            Assert.That(result.CutScore, Is.EqualTo(411));
        });
    }
}
