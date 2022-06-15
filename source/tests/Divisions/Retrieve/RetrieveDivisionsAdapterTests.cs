namespace NewEnglandClassic.Tests.Divisions.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Divisions.Retrieve.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Divisions.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Divisions.Retrieve.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Divisions.Retrieve.Adapter(_businessLogic.Object);
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
    public void ForTournament_ReturnsDivisionsFromBusinessLogic()
    {
        var division1 = new NewEnglandClassic.Models.Division { Name = "Division 1" };
        var division2 = new NewEnglandClassic.Models.Division { Name = "Division 2" };
        var divisions = new[] { division1, division2 };

        _businessLogic.Setup(businessLogic => businessLogic.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var tournamentId = Guid.NewGuid();

        var actual = _adapter.ForTournament(tournamentId).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].DivisionName, Is.EqualTo(division1.Name));
            Assert.That(actual[1].DivisionName, Is.EqualTo(division2.Name));
        });
    }
}
