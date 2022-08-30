namespace NortheastMegabuck.Tests.Squads.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Squads.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Squads.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Squads.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Squads.Retrieve.Adapter(_businessLogic.Object);
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
    public void Execute_ReturnsSquadsFromBusinessLogic()
    {
        var squad1 = new NortheastMegabuck.Models.Squad { MaxPerPair = 1 };
        var squad2 = new NortheastMegabuck.Models.Squad { MaxPerPair = 2 };
        var squads = new[] { squad1, squad2 };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<TournamentId>())).Returns(squads);

        var tournamentId = TournamentId.New();

        var actual = _adapter.Execute(tournamentId).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].MaxPerPair, Is.EqualTo(squad1.MaxPerPair));
            Assert.That(actual[1].MaxPerPair, Is.EqualTo(squad2.MaxPerPair));
        });
    }
}
