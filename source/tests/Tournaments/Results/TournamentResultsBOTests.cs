
namespace NortheastMegabuck.Tests.Tournaments.Results;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Tournaments.Results.ICalculator> _calculator;
    private Mock<NortheastMegabuck.Squads.Results.IBusinessLogic> _retrieveSquadResults;
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;

    private NortheastMegabuck.Tournaments.Results.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _calculator = new Mock<NortheastMegabuck.Tournaments.Results.ICalculator>();
        _retrieveSquadResults = new Mock<NortheastMegabuck.Squads.Results.IBusinessLogic>();
        _retrieveTournament = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();

        _businessLogic = new NortheastMegabuck.Tournaments.Results.BusinessLogic(_calculator.Object, _retrieveSquadResults.Object, _retrieveTournament.Object);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecute_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _retrieveTournament.Verify(retrieveTournament => retrieveTournament.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveTournament.SetupGet(retrieveTournament => retrieveTournament.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));

            _retrieveSquadResults.Verify(retrieveSquadResults => retrieveSquadResults.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()), Times.Never);
            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<ICollection<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsExecute_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _retrieveSquadResults.Verify(retrieveSquadResults => retrieveSquadResults.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveSquadResults.SetupGet(retrieveSquadResults => retrieveSquadResults.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));

            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<ICollection<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsNoError_Calculator_CalledCorrectly()
    {
        var division1 = new NortheastMegabuck.Models.Division();
        var division2 = new NortheastMegabuck.Models.Division();

        var squadResult1 = new NortheastMegabuck.Models.SquadResult
        {
            Division = division1
        };
        var squadResult2 = new NortheastMegabuck.Models.SquadResult
        {
            Division = division2
        };

        var squadResults = new[] { squadResult1, squadResult2 }.GroupBy(squadResult => squadResult.Division);
        _retrieveSquadResults.Setup(retrieveSquadResult => retrieveSquadResult.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadResults);

        var tournament = new NortheastMegabuck.Models.Tournament
        {
            FinalsRatio = 5m
        };

        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<ICollection<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Exactly(2));

            _calculator.Verify(calculator => calculator.Execute(division1.Id, It.Is<ICollection<NortheastMegabuck.Models.SquadResult>>(squadResults => squadResults.Single() == squadResult1), 5), Times.Once);
            _calculator.Verify(calculator => calculator.Execute(division2.Id, It.Is<ICollection<NortheastMegabuck.Models.SquadResult>>(squadResults => squadResults.Single() == squadResult2), 5), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentNoError_RetrieveSquadResultsNoError_Calculator_ReturnsAtLargeModel()
    {
        var division1 = new NortheastMegabuck.Models.Division();
        var division2 = new NortheastMegabuck.Models.Division();

        var squadResult1 = new NortheastMegabuck.Models.SquadResult
        {
            Division = division1,
            AdvancingScores = Enumerable.Repeat(new NortheastMegabuck.Models.BowlerSquadScore(200), 5)
        };
        var squadResult2 = new NortheastMegabuck.Models.SquadResult
        {
            Division = division2,
            AdvancingScores = Enumerable.Repeat(new NortheastMegabuck.Models.BowlerSquadScore(200), 6)
        };

        var squadResults = new[] { squadResult1, squadResult2 }.GroupBy(squadResult => squadResult.Division);
        _retrieveSquadResults.Setup(retrieveSquadResult => retrieveSquadResult.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadResults);

        var tournament = new NortheastMegabuck.Models.Tournament
        {
            FinalsRatio = 5m
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var division1AtLarge = new NortheastMegabuck.Models.AtLargeResults { DivisionId = division1.Id };
        var division2AtLarge = new NortheastMegabuck.Models.AtLargeResults { DivisionId = division2.Id };

        _calculator.Setup(calculator => calculator.Execute(division1.Id, It.IsAny<ICollection<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>())).Returns(division1AtLarge);
        _calculator.Setup(calculator => calculator.Execute(division2.Id, It.IsAny<ICollection<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>())).Returns(division2AtLarge);

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
