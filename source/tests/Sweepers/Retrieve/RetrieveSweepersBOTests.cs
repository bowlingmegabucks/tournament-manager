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
    public void Execute_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _businessLogic.Execute(tournamentId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDataLayerExecuteResults()
    {
        var sweepers = Enumerable.Repeat(new NewEnglandClassic.Models.Sweeper { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(sweepers);

        var tournamentId = TournamentId.New();

        var actual = _businessLogic.Execute(tournamentId);

        Assert.That(actual, Is.EqualTo(sweepers));
    }

    [Test]
    public void Execute_DataLayerExecuteNoException_ErrorNull()
    {
        var sweepers = Enumerable.Repeat(new NewEnglandClassic.Models.Sweeper { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<TournamentId>())).Returns(sweepers);

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