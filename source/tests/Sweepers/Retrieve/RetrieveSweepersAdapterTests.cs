namespace NewEnglandClassic.Tests.Sweepers.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Sweepers.Retrieve.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Sweepers.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Sweepers.Retrieve.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Sweepers.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void ForTournament_BusinessLogicForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();

        _adapter.ForTournament(tournamentId);

        _businessLogic.Verify(businessLogic => businessLogic.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void ForTournament_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var tournamentId = Guid.NewGuid();

        _adapter.ForTournament(tournamentId);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void ForTournament_ReturnsSweepersFromBusinessLogic()
    {
        var sweeper1 = new NewEnglandClassic.Models.Sweeper { MaxPerPair = 1 };
        var sweeper2 = new NewEnglandClassic.Models.Sweeper { MaxPerPair = 2 };
        var sweepers = new[] { sweeper1, sweeper2 };

        _businessLogic.Setup(businessLogic => businessLogic.ForTournament(It.IsAny<Guid>())).Returns(sweepers);

        var tournamentId = Guid.NewGuid();

        var actual = _adapter.ForTournament(tournamentId).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].MaxPerPair, Is.EqualTo(sweeper1.MaxPerPair));
            Assert.That(actual[1].MaxPerPair, Is.EqualTo(sweeper2.MaxPerPair));
        });
    }
}
