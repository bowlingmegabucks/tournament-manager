namespace NortheastMegabuck.Tests.Sweepers.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Sweepers.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Sweepers.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _adapter.Execute(tournamentId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var tournamentId = TournamentId.New();

        _adapter.Execute(tournamentId);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void Execute_ReturnsSweepersFromBusinessLogic()
    {
        var sweeper1 = new NortheastMegabuck.Models.Sweeper { MaxPerPair = 1 };
        var sweeper2 = new NortheastMegabuck.Models.Sweeper { MaxPerPair = 2 };
        var sweepers = new[] { sweeper1, sweeper2 };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<TournamentId>())).Returns(sweepers);

        var tournamentId = TournamentId.New();

        var actual = _adapter.Execute(tournamentId).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].MaxPerPair, Is.EqualTo(sweeper1.MaxPerPair));
            Assert.That(actual[1].MaxPerPair, Is.EqualTo(sweeper2.MaxPerPair));
        });
    }
}
