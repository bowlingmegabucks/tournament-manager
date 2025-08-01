﻿using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

namespace BowlingMegabucks.TournamentManager.UnitTests.Squads.Results;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;
    private Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>> _getTournamentByQueryHandler;
    private Mock<TournamentManager.Squads.Results.ICalculator> _squadResultCalculator;
    private Mock<TournamentManager.Scores.Retrieve.IBusinessLogic> _retrieveScores;

    private TournamentManager.Squads.Results.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _retrieveTournament = new Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic>();
        _getTournamentByQueryHandler = new Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>>();
        _squadResultCalculator = new Mock<TournamentManager.Squads.Results.ICalculator>();
        _retrieveScores = new Mock<TournamentManager.Scores.Retrieve.IBusinessLogic>();

        _businessLogic = new TournamentManager.Squads.Results.BusinessLogic(_retrieveTournament.Object, _getTournamentByQueryHandler.Object, _squadResultCalculator.Object, _retrieveScores.Object);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecute_CalledCorrectly()
    {
        var tournament = new TournamentManager.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _retrieveTournament.Verify(retrieveTournament => retrieveTournament.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecuteHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveTournament.SetupGet(retrieveTournament => retrieveTournament.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));
            Assert.That(result, Is.Empty);

            _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Never);
            _squadResultCalculator.Verify(calculator => calculator.Execute(It.IsAny<TournamentManager.Models.Squad>(), It.IsAny<TournamentManager.Models.Division>(), It.IsAny<List<TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecuteNoError_RetrieveScoresExecute_CalledWithSquadsBeforeOrOnGivenSquadDate()
    {
        var pastSquad = new TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(-1)
        };

        var currentSquad = new TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now
        };

        var futureSquad = new TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(1)
        };

        var tournament = new TournamentManager.Models.Tournament
        {
            Squads = [pastSquad, currentSquad, futureSquad]
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        CancellationToken cancellationToken = default;
        await _businessLogic.ExecuteAsync(currentSquad.Id, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.Is<IEnumerable<SquadId>>(squads => squads.Count() == 2), cancellationToken), Times.Once);

            _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.Is<IEnumerable<SquadId>>(squads => squads.Contains(pastSquad.Id)), cancellationToken), Times.Once);
            _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.Is<IEnumerable<SquadId>>(squads => squads.Contains(currentSquad.Id)), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecuteNoError_RetrieveScoresExecuteNoError_SquadResultCalculatorExecute_CalledInSequentialOrderBasedOnSquadDate()
    {
        var pastSquad = new TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(-1)
        };

        var currentSquad = new TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now
        };

        var futureSquad = new TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(1)
        };

        var tournament = new TournamentManager.Models.Tournament
        {
            Squads = [currentSquad, futureSquad, pastSquad]
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var mockResultCalculator = new Mock<TournamentManager.Squads.Results.ICalculator>(MockBehavior.Strict);
        var sequence = new MockSequence();
        mockResultCalculator.InSequence(sequence).Setup(calculator => calculator.Execute(It.Is<TournamentManager.Models.Squad>(squad => squad.Id == pastSquad.Id), It.IsAny<TournamentManager.Models.Division>(), It.IsAny<List<TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
        mockResultCalculator.InSequence(sequence).Setup(calculator => calculator.Execute(It.Is<TournamentManager.Models.Squad>(squad => squad.Id == currentSquad.Id), It.IsAny<TournamentManager.Models.Division>(), It.IsAny<List<TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
        mockResultCalculator.Setup(calculator => calculator.Execute(It.IsAny<TournamentManager.Models.Squad>(), It.IsAny<TournamentManager.Models.Division>(), It.IsAny<List<TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(new TournamentManager.Models.SquadResult());
        _businessLogic = new TournamentManager.Squads.Results.BusinessLogic(_retrieveTournament.Object, _getTournamentByQueryHandler.Object, mockResultCalculator.Object, _retrieveScores.Object);

        var pastSquadBowler1Score1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() },
            SquadId = pastSquad.Id,
            Division = new TournamentManager.Models.Division(),
            GameNumber = 1,
            Score = 200
        };

        var currentSquadBowler2Score1 = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() },
            SquadId = currentSquad.Id,
            Division = new TournamentManager.Models.Division(),
            GameNumber = 1,
            Score = 200
        };

        var scores = new[] { currentSquadBowler2Score1, pastSquadBowler1Score1 };
        _retrieveScores.Setup(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(scores);

        await _businessLogic.ExecuteAsync(currentSquad.Id, default).ConfigureAwait(false);

        mockResultCalculator.VerifyAll();
    }
}
