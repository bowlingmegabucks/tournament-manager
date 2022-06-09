namespace NewEnglandClassic.Tests.Squads.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Squads.Retrieve.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Squads.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Squads.Retrieve.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Squads.Retrieve.Adapter(_businessLogic.Object);
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
    public void ForTournament_ReturnsSquadsFromBusinessLogic()
    {
        var divison1 = new NewEnglandClassic.Models.Squad { MaxPerPair = 1 };
        var divison2 = new NewEnglandClassic.Models.Squad { MaxPerPair = 2 };
        var squads = new[] { divison1, divison2 };

        _businessLogic.Setup(businessLogic => businessLogic.ForTournament(It.IsAny<Guid>())).Returns(squads);

        var tournamentId = Guid.NewGuid();

        var actual = _adapter.ForTournament(tournamentId).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].MaxPerPair, Is.EqualTo(divison1.MaxPerPair));
            Assert.That(actual[1].MaxPerPair, Is.EqualTo(divison2.MaxPerPair));
        });
    }
}
