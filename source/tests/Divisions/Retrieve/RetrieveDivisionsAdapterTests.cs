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
    public void ForTournament_ErrorsSetToBusinessLogicErrors([Range(0, 2)] int errorCount)
    {
        var errors = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("test"), errorCount);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var tournamentId = Guid.NewGuid();

        _adapter.ForTournament(tournamentId);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void ForTournament_ReturnsDivisionsFromBusinessLogic()
    {
        var divison1 = new NewEnglandClassic.Models.Division { Name = "Division 1" };
        var divison2 = new NewEnglandClassic.Models.Division { Name = "Division 2" };
        var divisions = new[] { divison1, divison2 };

        _businessLogic.Setup(businessLogic => businessLogic.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var tournamentId = Guid.NewGuid();

        var actual = _adapter.ForTournament(tournamentId).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].DivisionName, Is.EqualTo(divison1.Name));
            Assert.That(actual[1].DivisionName, Is.EqualTo(divison2.Name));
        });
    }
}
