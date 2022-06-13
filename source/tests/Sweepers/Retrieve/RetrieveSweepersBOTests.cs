namespace NewEnglandClassic.Tests.Sweepers.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Sweepers.Retrieve.IDataLayer> _dataLayer;

    private NewEnglandClassic.Sweepers.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NewEnglandClassic.Sweepers.Retrieve.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Sweepers.Retrieve.BusinessLogic(_dataLayer.Object);
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
        var sweepers = Enumerable.Repeat(new NewEnglandClassic.Models.Sweeper { Id = Guid.NewGuid() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(sweepers);

        var tournamentId = Guid.NewGuid();

        var actual = _businessLogic.ForTournament(tournamentId);

        Assert.That(actual, Is.EqualTo(sweepers));
    }

    [Test]
    public void ForTournament_DataLayerForTournamentNoException_ErrorNull()
    {
        var sweepers = Enumerable.Repeat(new NewEnglandClassic.Models.Sweeper { Id = Guid.NewGuid() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(sweepers);

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
}