namespace NortheastMegabuck.Tests.Squads.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Squads.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Squads.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Squads.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Squads.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDataLayerExecuteResults()
    {
        var squads = Enumerable.Repeat(new NortheastMegabuck.Models.Squad { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(squads);

        var tournamentId = TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.That(actual, Is.EqualTo(squads));
    }

    [Test]
    public void Execute_DataLayerExecuteNoException_ErrorNull()
    {
        var squads = Enumerable.Repeat(new NortheastMegabuck.Models.Squad { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(squads);

        var tournamentId = TournamentId.New();

         _businessLogic.Execute(tournamentId);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Throws(ex);

        var tournamentId = TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}