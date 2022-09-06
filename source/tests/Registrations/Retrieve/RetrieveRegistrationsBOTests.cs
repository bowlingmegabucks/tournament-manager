namespace NortheastMegabuck.Tests.Registrations.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Registrations.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Registrations.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Registrations.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Registrations.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_TournamentId_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = NortheastMegabuck.TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_ReturnsDataLayerExecute()
    {
        var registrations = Enumerable.Repeat(new NortheastMegabuck.Models.Registration(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.TournamentId>())).Returns(registrations);

        var tournamentId = NortheastMegabuck.TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.That(actual, Is.EqualTo(registrations));
    }

    [Test]
    public void Execute_TournamentId_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.TournamentId>())).Throws(new Exception("exception"));

        var tournamentId = NortheastMegabuck.TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public void ForSquad_DataLayerForSquad_CalledCorrectly()
    {
        var squadId = NortheastMegabuck.SquadId.New();

        _businessLogic.ForSquad(squadId);

        _dataLayer.Verify(dataLayer => dataLayer.ForSquad(squadId), Times.Once);
    }

    [Test]
    public void ForSquad_ReturnsDataLayerForSquad()
    {
        var registrations = Enumerable.Repeat(new NortheastMegabuck.Models.SquadRegistration(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ForSquad(It.IsAny<SquadId>())).Returns(registrations);

        var squadId = NortheastMegabuck.SquadId.New();

        var actual = _businessLogic.ForSquad(squadId);

        Assert.That(actual, Is.EqualTo(registrations));
    }

    [Test]
    public void ForSquad_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _dataLayer.Setup(dataLayer => dataLayer.ForSquad(It.IsAny<NortheastMegabuck.SquadId>())).Throws(new Exception("exception"));

        var squadId = NortheastMegabuck.SquadId.New();

        var actual = _businessLogic.ForSquad(squadId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}
