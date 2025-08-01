﻿
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Results;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<TournamentManager.Tournaments.Results.ICalculator> _calculator;
    private Mock<TournamentManager.Squads.Results.IBusinessLogic> _retrieveSquadResults;
    private Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>> _retrieveTournament;

    private TournamentManager.Tournaments.Results.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _calculator = new Mock<TournamentManager.Tournaments.Results.ICalculator>();
        _retrieveSquadResults = new Mock<TournamentManager.Squads.Results.IBusinessLogic>();
        _retrieveTournament = new Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>>();

        _businessLogic = new TournamentManager.Tournaments.Results.BusinessLogic(_calculator.Object, _retrieveSquadResults.Object, _retrieveTournament.Object);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecute_CalledCorrectly()
    {
        var tournament = new TournamentManager.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _retrieveTournament.Verify(retrieveTournament => retrieveTournament.HandleAsync(It.Is<GetTournamentByIdQuery>(query => query.Id == tournamentId), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentHasError_ErrorFlow()
    {
        var error = Error.Conflict();
        _retrieveTournament.Setup(query => query.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(error);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo(error.Description));

            _retrieveSquadResults.Verify(retrieveSquadResults => retrieveSquadResults.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()), Times.Never);
            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<ICollection<TournamentManager.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsExecute_CalledCorrectly()
    {
        var tournament = new TournamentManager.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _retrieveSquadResults.Verify(retrieveSquadResults => retrieveSquadResults.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveSquadResults.SetupGet(retrieveSquadResults => retrieveSquadResults.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));

            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<ICollection<TournamentManager.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsNoError_Calculator_CalledCorrectly()
    {
        var division1 = new TournamentManager.Models.Division();
        var division2 = new TournamentManager.Models.Division();

        var squadResult1 = new TournamentManager.Models.SquadResult
        {
            Division = division1
        };
        var squadResult2 = new TournamentManager.Models.SquadResult
        {
            Division = division2
        };

        var squadResults = new[] { squadResult1, squadResult2 }.GroupBy(squadResult => squadResult.Division);
        _retrieveSquadResults.Setup(retrieveSquadResult => retrieveSquadResult.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadResults);

        var tournament = new TournamentManager.Models.Tournament
        {
            FinalsRatio = 5m
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<ICollection<TournamentManager.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Exactly(2));

            _calculator.Verify(calculator => calculator.Execute(division1.Id, It.Is<ICollection<TournamentManager.Models.SquadResult>>(squadResults => squadResults.Single() == squadResult1), 5), Times.Once);
            _calculator.Verify(calculator => calculator.Execute(division2.Id, It.Is<ICollection<TournamentManager.Models.SquadResult>>(squadResults => squadResults.Single() == squadResult2), 5), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsNoError_Calculator_ReturnsAtLargeModel()
    {
        var division1 = new TournamentManager.Models.Division();
        var division2 = new TournamentManager.Models.Division();

        var squadResult1 = new TournamentManager.Models.SquadResult
        {
            Division = division1,
            AdvancingScores = Enumerable.Repeat(new TournamentManager.Models.BowlerSquadScore(200), 5)
        };
        var squadResult2 = new TournamentManager.Models.SquadResult
        {
            Division = division2,
            AdvancingScores = Enumerable.Repeat(new TournamentManager.Models.BowlerSquadScore(200), 6)
        };

        var squadResults = new[] { squadResult1, squadResult2 }.GroupBy(squadResult => squadResult.Division);
        _retrieveSquadResults.Setup(retrieveSquadResult => retrieveSquadResult.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadResults);

        var tournament = new TournamentManager.Models.Tournament
        {
            FinalsRatio = 5m
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var division1AtLarge = new TournamentManager.Models.AtLargeResults { DivisionId = division1.Id };
        var division2AtLarge = new TournamentManager.Models.AtLargeResults { DivisionId = division2.Id };

        _calculator.Setup(calculator => calculator.Execute(division1.Id, It.IsAny<ICollection<TournamentManager.Models.SquadResult>>(), It.IsAny<decimal>())).Returns(division1AtLarge);
        _calculator.Setup(calculator => calculator.Execute(division2.Id, It.IsAny<ICollection<TournamentManager.Models.SquadResult>>(), It.IsAny<decimal>())).Returns(division2AtLarge);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));

            Assert.That(result.First().AtLarge, Is.EqualTo(division1AtLarge));
            Assert.That(result.First().Entries, Is.EqualTo(5));
            Assert.That(result.First().SquadResults.Single(), Is.EqualTo(squadResult1));

            Assert.That(result.Last().AtLarge, Is.EqualTo(division2AtLarge));
            Assert.That(result.Last().Entries, Is.EqualTo(6));
            Assert.That(result.Last().SquadResults.Single(), Is.EqualTo(squadResult2));
        });
    }
}
