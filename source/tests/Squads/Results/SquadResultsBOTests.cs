namespace BowlingMegabucks.TournamentManager.Tests.Squads.Results;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;
    private Mock<BowlingMegabucks.TournamentManager.Squads.Results.ICalculator> _squadResultCalculator;
    private Mock<BowlingMegabucks.TournamentManager.Scores.Retrieve.IBusinessLogic> _retrieveScores;

    private BowlingMegabucks.TournamentManager.Squads.Results.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _retrieveTournament = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IBusinessLogic>();
        _squadResultCalculator = new Mock<BowlingMegabucks.TournamentManager.Squads.Results.ICalculator>();
        _retrieveScores = new Mock<BowlingMegabucks.TournamentManager.Scores.Retrieve.IBusinessLogic>();

        _businessLogic = new BowlingMegabucks.TournamentManager.Squads.Results.BusinessLogic(_retrieveTournament.Object, _squadResultCalculator.Object, _retrieveScores.Object);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecute_CalledCorrectly()
    {
        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _retrieveTournament.Verify(retrieveTournament => retrieveTournament.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecuteHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _retrieveTournament.SetupGet(retrieveTournament => retrieveTournament.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));
            Assert.That(result, Is.Empty);

            _retrieveScores.Verify(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Never);
            _squadResultCalculator.Verify(calculator => calculator.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>(), It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<List<BowlingMegabucks.TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_RetrieveTournamentExecuteNoError_RetrieveScoresExecute_CalledWithSquadsBeforeOrOnGivenSquadDate()
    {
        var pastSquad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(-1)
        };

        var currentSquad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now
        };

        var futureSquad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(1)
        };

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
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
        var pastSquad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(-1)
        };

        var currentSquad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now
        };

        var futureSquad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(1)
        };

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            Squads = [currentSquad, futureSquad, pastSquad]
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var mockResultCalculator = new Mock<BowlingMegabucks.TournamentManager.Squads.Results.ICalculator>(MockBehavior.Strict);
        var sequence = new MockSequence();
        mockResultCalculator.InSequence(sequence).Setup(calculator => calculator.Execute(It.Is<BowlingMegabucks.TournamentManager.Models.Squad>(squad => squad.Id == pastSquad.Id), It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<List<BowlingMegabucks.TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
        mockResultCalculator.InSequence(sequence).Setup(calculator => calculator.Execute(It.Is<BowlingMegabucks.TournamentManager.Models.Squad>(squad => squad.Id == currentSquad.Id), It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<List<BowlingMegabucks.TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
        mockResultCalculator.Setup(calculator => calculator.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>(), It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<List<BowlingMegabucks.TournamentManager.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(new BowlingMegabucks.TournamentManager.Models.SquadResult());
        _businessLogic = new BowlingMegabucks.TournamentManager.Squads.Results.BusinessLogic(_retrieveTournament.Object, mockResultCalculator.Object, _retrieveScores.Object);

        var pastSquadBowler1Score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = BowlerId.New() },
            SquadId = pastSquad.Id,
            Division = new BowlingMegabucks.TournamentManager.Models.Division(),
            GameNumber = 1,
            Score = 200
        };

        var currentSquadBowler2Score1 = new BowlingMegabucks.TournamentManager.Models.SquadScore
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Id = BowlerId.New() },
            SquadId = currentSquad.Id,
            Division = new BowlingMegabucks.TournamentManager.Models.Division(),
            GameNumber = 1,
            Score = 200
        };

        var scores = new[] { currentSquadBowler2Score1, pastSquadBowler1Score1 };
        _retrieveScores.Setup(retrieveScores => retrieveScores.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(scores);

        await _businessLogic.ExecuteAsync(currentSquad.Id, default).ConfigureAwait(false);

        mockResultCalculator.VerifyAll();
    }
}
