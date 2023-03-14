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
    public void Execute_TournamnetId_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamnetId_ReturnsDataLayerExecuteResults()
    {
        var squads = Enumerable.Repeat(new NortheastMegabuck.Models.Squad(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(squads);

        var tournamentId = TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.That(actual, Is.EqualTo(squads));
    }

    [Test]
    public void Execute_TournamnetId_DataLayerExecuteNoException_ErrorNull()
    {
        var squads = Enumerable.Repeat(new NortheastMegabuck.Models.Squad(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(squads);

        var tournamentId = TournamentId.New();

         _businessLogic.Execute(tournamentId);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_TournamnetId_DataLayerExecuteThrowsException_ErrorFlow()
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

    [Test]
    public void Execute_SquadId_DataLayerExecute_CalledCorrectly()
    {
        var id = SquadId.New();

        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_SquadId_ReturnsDataLayerExecuteResults()
    {
        var squad = new NortheastMegabuck.Models.Squad();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Returns(squad);

        var id = SquadId.New();

        var actual = _businessLogic.Execute(id);

        Assert.That(actual, Is.EqualTo(squad));
    }

    [Test]
    public void Execute_SquadId_DataLayerExecuteNoException_ErrorNull()
    {
        var squad = new NortheastMegabuck.Models.Squad();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Returns(squad);

        var id = SquadId.New();

        var actual = _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_SquadId_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Throws(ex);

        var id = SquadId.New();

        var actual = _businessLogic.Execute(id);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}