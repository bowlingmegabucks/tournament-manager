
namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.Seeding;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Results.IBusinessLogic> _tournamentResults;
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Seeding.ICalculator> _calculator;

    private BowlingMegabucks.TournamentManager.Tournaments.Seeding.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _tournamentResults = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Results.IBusinessLogic>();
        _calculator = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Seeding.ICalculator>();

        _businessLogic = new BowlingMegabucks.TournamentManager.Tournaments.Seeding.BusinessLogic(_tournamentResults.Object, _calculator.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentResultsExecute_CalledCorrectly()
    {
        var id = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _tournamentResults.Verify(tournamentResults => tournamentResults.ExecuteAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentResultsHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _tournamentResults.SetupGet(tournamentResults => tournamentResults.ErrorDetail).Returns(error);

        var result = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.ErrorDetail, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentResultsHasNoError_CorrectSeedingReturned()
    {
        var division1Result = new BowlingMegabucks.TournamentManager.Models.TournamentResults
        {
            Division = new BowlingMegabucks.TournamentManager.Models.Division()
        };

        var division2Result = new BowlingMegabucks.TournamentManager.Models.TournamentResults
        {
            Division = new BowlingMegabucks.TournamentManager.Models.Division()
        };

        var results = new[] { division1Result, division2Result };
        _tournamentResults.Setup(tournamentResult => tournamentResult.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(results);

        static BowlingMegabucks.TournamentManager.Models.TournamentFinalsSeeding seedingReturn(BowlingMegabucks.TournamentManager.Models.TournamentResults result) => new() { Division = result.Division };
        _calculator.Setup(calculator => calculator.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.TournamentResults>())).Returns(seedingReturn);

        var actual = await _businessLogic.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));

            Assert.That(actual.Count(result => result.Division.Id == division1Result.Division.Id), Is.EqualTo(1));
            Assert.That(actual.Count(result => result.Division.Id == division2Result.Division.Id), Is.EqualTo(1));
        });
    }
}
