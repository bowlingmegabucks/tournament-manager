namespace NortheastMegabuck.Tests.Registrations.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
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
}
