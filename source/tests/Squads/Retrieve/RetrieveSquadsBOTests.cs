namespace NewEnglandClassic.Tests.Squads.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Squads.Retrieve.IDataLayer> _dataLayer;

    private NewEnglandClassic.Squads.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NewEnglandClassic.Squads.Retrieve.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Squads.Retrieve.BusinessLogic(_dataLayer.Object);
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
        var squads = Enumerable.Repeat(new NewEnglandClassic.Models.Squad { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(squads);

        var tournamentId = Guid.NewGuid();

        var actual = _businessLogic.ForTournament(tournamentId);

        Assert.That(actual, Is.EqualTo(squads));
    }

    [Test]
    public void ForTournament_DataLayerForTournamentNoException_ErrorNull()
    {
        var squads = Enumerable.Repeat(new NewEnglandClassic.Models.Squad { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForTournament(It.IsAny<Guid>())).Returns(squads);

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