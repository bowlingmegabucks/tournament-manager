namespace NewEnglandClassic.Tests.Divisions.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Divisions.Retrieve.IDataLayer> _dataLayer;

    private NewEnglandClassic.Divisions.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NewEnglandClassic.Divisions.Retrieve.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Divisions.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void ForTournament_DataLayerForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();

        _businessLogic.ForTournament(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void ForTournament_ReturnsDataLayerForTournamentResults()
    {
        var divisions = Enumerable.Repeat(new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var tournamentId = Guid.NewGuid();

        var actual = _businessLogic.ForTournament(tournamentId);

        Assert.That(actual, Is.EqualTo(divisions));
    }

    [Test]
    public void ForTournament_DataLayerForTournamentNoException_ErrorNull()
    {
        var divisions = Enumerable.Repeat(new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var tournamentId = Guid.NewGuid();

         _businessLogic.ForTournament(tournamentId);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void ForTournament_DataLayerForTournamentThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Throws(ex);

        var tournamentId = Guid.NewGuid();

        var actual = _businessLogic.ForTournament(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public void Execute_DataLayerExecute_CalledCorrectly()
    {
        var divisionId = Guid.NewGuid();

        _businessLogic.Execute(divisionId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(divisionId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDataLayerExecuteResult()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Returns(division);

        var divisionId = Guid.NewGuid();

        var actual = _businessLogic.Execute(divisionId);

        Assert.That(actual, Is.EqualTo(division));
    }

    [Test]
    public void Execute_DataLayerExecutetNoException_ErrorNull()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Returns(division);

        var divisionId = Guid.NewGuid();

        _businessLogic.Execute(divisionId);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<Guid>())).Throws(ex);

        var divisionId = Guid.NewGuid();

        var actual = _businessLogic.Execute(divisionId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}