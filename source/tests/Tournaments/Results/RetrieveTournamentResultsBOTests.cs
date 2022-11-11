
namespace NortheastMegabuck.Tests.Tournaments.Results;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Tournaments.Results.ICalculator> _calculator;
    private Mock<NortheastMegabuck.Squads.Results.IBusinessLogic> _retrieveSquadResults;
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;

    private NortheastMegabuck.Tournaments.Results.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _calculator = new Mock<NortheastMegabuck.Tournaments.Results.ICalculator>();
        _retrieveSquadResults = new Mock<NortheastMegabuck.Squads.Results.IBusinessLogic>();
        _retrieveTournament = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();

        _businessLogic = new NortheastMegabuck.Tournaments.Results.BusinessLogic(_calculator.Object, _retrieveSquadResults.Object, _retrieveTournament.Object);
    }

    [Test]
    public void Execute_RetrieveTournamentExecute_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var tournamentId = TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _retrieveTournament.Verify(retrieveTournament=> retrieveTournament.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_RetrieveTournamentHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveTournament.SetupGet(retrieveTournament => retrieveTournament.Error).Returns(error);

        var result = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.Error, Is.EqualTo(error));

            _retrieveSquadResults.Verify(retrieveSquadResults => retrieveSquadResults.Execute(It.IsAny<TournamentId>()), Times.Never);
            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public void Execute_RetrieveTournamentNoError_RetrieveSquadResultsExecute_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var tournamentId = TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _retrieveSquadResults.Verify(retrieveSquadResults => retrieveSquadResults.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_RetrieveTournamentNoError_RetrieveSquadResultsHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveSquadResults.SetupGet(retrieveSquadResults => retrieveSquadResults.Error).Returns(error);

        var result = _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.Error, Is.EqualTo(error));

            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public void Execute_RetrieveTournamentNoError_RetrieveSquadResultsNoError_Calculator_CalledCorrectly()
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
        _retrieveSquadResults.Setup(retrieveSquadResult => retrieveSquadResult.Execute(It.IsAny<TournamentId>())).Returns(squadResults);

        var tournament = new NortheastMegabuck.Models.Tournament
        { 
            FinalsRatio = 5m
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        _businessLogic.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            _calculator.Verify(calculator => calculator.Execute(It.IsAny<DivisionId>(), It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>()), Times.Exactly(2));

            _calculator.Verify(calculator => calculator.Execute(division1.Id, It.Is<IEnumerable<NortheastMegabuck.Models.SquadResult>>(squadResults => squadResults.Single() == squadResult1), 5), Times.Once);
            _calculator.Verify(calculator => calculator.Execute(division2.Id, It.Is<IEnumerable<NortheastMegabuck.Models.SquadResult>>(squadResults => squadResults.Single() == squadResult2), 5), Times.Once);
        });
    }

    [Test]
    public void Execute_RetrieveTournamentNoError_RetrieveSquadResultsNoError_Calculator_ReturnsAtLargeModel()
    {
        var division1 = new NortheastMegabuck.Models.Division();
        var division2 = new NortheastMegabuck.Models.Division();

        var squadResult1 = new NortheastMegabuck.Models.SquadResult
        {
            Division = division1,
            AdvancingScores = Enumerable.Repeat(new NortheastMegabuck.Models.BowlerSquadScore(200),5)
        };
        var squadResult2 = new NortheastMegabuck.Models.SquadResult
        {
            Division = division2,
            AdvancingScores = Enumerable.Repeat(new NortheastMegabuck.Models.BowlerSquadScore(200), 6)
        };

        var squadResults = new[] { squadResult1, squadResult2 }.GroupBy(squadResult => squadResult.Division);
        _retrieveSquadResults.Setup(retrieveSquadResult => retrieveSquadResult.Execute(It.IsAny<TournamentId>())).Returns(squadResults);

        var tournament = new NortheastMegabuck.Models.Tournament
        {
            FinalsRatio = 5m
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var division1AtLarge = new NortheastMegabuck.Models.AtLargeResults { DivisionId = division1.Id };
        var division2AtLarge = new NortheastMegabuck.Models.AtLargeResults { DivisionId = division2.Id };

        _calculator.Setup(calculator => calculator.Execute(division1.Id, It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>())).Returns(division1AtLarge);
        _calculator.Setup(calculator => calculator.Execute(division2.Id, It.IsAny<IEnumerable<NortheastMegabuck.Models.SquadResult>>(), It.IsAny<decimal>())).Returns(division2AtLarge);

        var result = _businessLogic.Execute(TournamentId.New());

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
