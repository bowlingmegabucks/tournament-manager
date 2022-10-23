
namespace NortheastMegabuck.Tests.Scores.Retrieve;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Scores.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Scores.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Scores.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Scores.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_SquadId_DataLayerExecute_SquadId_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _businessLogic.Execute(squadId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Throws(ex);

        var result = _businessLogic.Execute(SquadId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public void Execute_SquadId_ReturnsDataLayerExecute()
    {
        var scores = new Mock<IEnumerable<NortheastMegabuck.Models.SquadScore>>();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Returns(scores.Object);

        var actual = _businessLogic.Execute(SquadId.New());

        Assert.That(actual, Is.EqualTo(scores.Object));
    }

    [Test]
    public void Execute_SquadIds_DataLayerExecute_SquadIds_CalledCorrectly()
    {
        var squadIds = new[] { SquadId.New(), SquadId.New() };

        _businessLogic.Execute(squadIds);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(squadIds), Times.Once);
    }

    [Test]
    public void Execute_SquadIds_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<IEnumerable<SquadId>>())).Throws(ex);

        var result = _businessLogic.Execute(new[] { SquadId.New(), SquadId.New() });

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public void Execute_SquadIds_ReturnsDataLayerExecute()
    {
        var scores = new Mock<IEnumerable<NortheastMegabuck.Models.SquadScore>>();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<IEnumerable<SquadId>>())).Returns(scores.Object);

        var actual = _businessLogic.Execute(new[] { SquadId.New(), SquadId.New() });

        Assert.That(actual, Is.EqualTo(scores.Object));
    }

    [Test]
    public void SuperSweeper_DataLayerSuperSweeper_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _businessLogic.SuperSweeper(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.SuperSweeper(tournamentId), Times.Once);
    }

    [Test]
    public void SuperSweeper_DataLayerSuperSweeperThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.SuperSweeper(It.IsAny<TournamentId>())).Throws(ex);

        var result = _businessLogic.SuperSweeper(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public void SuperSweeper_ReturnsDataLayerSuperSweeper()
    {
        var scores = new Mock<IEnumerable<NortheastMegabuck.Models.SquadScore>>();
        _dataLayer.Setup(dataLayer => dataLayer.SuperSweeper(It.IsAny<TournamentId>())).Returns(scores.Object);

        var actual = _businessLogic.SuperSweeper(TournamentId.New());

        Assert.That(actual, Is.EqualTo(scores.Object));
    }
}
