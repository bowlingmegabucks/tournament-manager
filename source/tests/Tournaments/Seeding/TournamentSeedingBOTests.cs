
namespace NortheastMegabuck.Tests.Tournaments.Seeding;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Tournaments.Results.IBusinessLogic> _tournamentResults;
    private Mock<NortheastMegabuck.Tournaments.Seeding.ICalculator> _calculator;

    private NortheastMegabuck.Tournaments.Seeding.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _tournamentResults = new Mock<NortheastMegabuck.Tournaments.Results.IBusinessLogic>();
        _calculator = new Mock<NortheastMegabuck.Tournaments.Seeding.ICalculator>();

        _businessLogic = new NortheastMegabuck.Tournaments.Seeding.BusinessLogic(_tournamentResults.Object, _calculator.Object);
    }

    [Test]
    public void Execute_TournamentResultsExecute_CalledCorrectly()
    {
        var id = TournamentId.New();

        _businessLogic.Execute(id);

        _tournamentResults.Verify(tournamentResults => tournamentResults.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_TournamentResultsHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _tournamentResults.SetupGet(tournamentResults => tournamentResults.Error).Returns(error);

        var result = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public void Execute_TournamentResultsHasNoError_CorrectSeedingReturned()
    {
        var division1Result = new NortheastMegabuck.Models.TournamentResults
        {
            Division = new NortheastMegabuck.Models.Division()
        };

        var division2Result = new NortheastMegabuck.Models.TournamentResults
        {
            Division = new NortheastMegabuck.Models.Division()
        };

        var results = new[] { division1Result, division2Result };
        _tournamentResults.Setup(tournamentResult => tournamentResult.Execute(It.IsAny<TournamentId>())).Returns(results);

        static NortheastMegabuck.Models.TournamentFinalsSeeding seedingReturn(NortheastMegabuck.Models.TournamentResults result) => new() { Division = result.Division };
        _calculator.Setup(calculator => calculator.Execute(It.IsAny<NortheastMegabuck.Models.TournamentResults>())).Returns(seedingReturn);

        var actual = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));

            Assert.That(actual.Count(result => result.Division.Id == division1Result.Division.Id), Is.EqualTo(1));
            Assert.That(actual.Count(result => result.Division.Id == division2Result.Division.Id), Is.EqualTo(1));
        });
    }
}
