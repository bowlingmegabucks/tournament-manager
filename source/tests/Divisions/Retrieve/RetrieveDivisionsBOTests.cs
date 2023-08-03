namespace NortheastMegabuck.Tests.Divisions.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Divisions.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Divisions.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Divisions.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Divisions.Retrieve.BusinessLogic(_dataLayer.Object);
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
        var divisions = Enumerable.Repeat(new NortheastMegabuck.Models.Division(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(divisions);

        var tournamentId = TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.That(actual, Is.EqualTo(divisions));
    }

    [Test]
    public void Execute_DataLayerExecuteNoException_ErrorNull()
    {
        var divisions = Enumerable.Repeat(new NortheastMegabuck.Models.Division(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(divisions);

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

    [Test]
    public void Execute_DivisionId_DataLayerExecute_CalledCorrectly()
    {
        var divisionId = NortheastMegabuck.DivisionId.New();

        _businessLogic.Execute(divisionId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(divisionId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDataLayerExecuteResult()
    {
        var division = new NortheastMegabuck.Models.Division();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Returns(division);

        var divisionId = NortheastMegabuck.DivisionId.New();

        var actual = _businessLogic.Execute(divisionId);

        Assert.That(actual, Is.EqualTo(division));
    }

    [Test]
    public void Execute_DataLayerExecutetNoException_ErrorNull()
    {
        var division = new NortheastMegabuck.Models.Division();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Returns(division);

        var divisionId = NortheastMegabuck.DivisionId.New();

        _businessLogic.Execute(divisionId);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DivisionId_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Throws(ex);

        var divisionId = NortheastMegabuck.DivisionId.New();

        var actual = _businessLogic.Execute(divisionId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}