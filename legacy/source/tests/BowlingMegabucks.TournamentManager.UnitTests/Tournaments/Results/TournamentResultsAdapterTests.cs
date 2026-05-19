namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Results;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Tournaments.Results.IBusinessLogic> _businessLogic;

    private TournamentManager.Tournaments.Results.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Tournaments.Results.IBusinessLogic>();

        _adapter = new TournamentManager.Tournaments.Results.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task AtLargeAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.AtLargeAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task AtLargeAsync_BusinessLogicHasError_ErrorMapped()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var result = await _adapter.AtLargeAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_adapter.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task AtLargeAsync_NoError_ResultsMapped()
    {
        var previousCasherId = BowlerId.New();

        var division1Result = new TournamentManager.Models.AtLargeResults
        {
            DivisionId = DivisionId.New(),
            AdvancingScores =
            [
                new TournamentManager.Models.BowlerSquadScore(200),
                new TournamentManager.Models.BowlerSquadScore(199)
            ]
        };

        var division2Result = new TournamentManager.Models.AtLargeResults
        {
            DivisionId = DivisionId.New(),
            AdvancingScores =
            [
                new TournamentManager.Models.BowlerSquadScore(198),
                new TournamentManager.Models.BowlerSquadScore(previousCasherId, 197),
                new TournamentManager.Models.BowlerSquadScore(196)
            ],
            AdvancersWhoPreviouslyCashed = [previousCasherId]
        };

        var tournamentResults = new[]
        {
            new TournamentManager.Models.TournamentResults { AtLarge = division1Result },
            new TournamentManager.Models.TournamentResults {AtLarge = division2Result }
         };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournamentResults);

        var results = (await _adapter.AtLargeAsync(TournamentId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(results, Has.Count.EqualTo(5));

            Assert.That(results.Count(result => result.Place == 1), Is.EqualTo(2));
            Assert.That(results.Count(result => result.Place == 2), Is.EqualTo(2));
            Assert.That(results.Count(result => result.Place == 3), Is.EqualTo(1));

            Assert.That(results.Count(result => result.PreviousCasher), Is.EqualTo(1));
        });
    }
}
