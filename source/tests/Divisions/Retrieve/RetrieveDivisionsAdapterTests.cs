namespace NortheastMegabuck.Tests.Divisions.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Divisions.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Divisions.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Divisions.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Divisions.Retrieve.Adapter(_businessLogic.Object);
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
    public void Execute_ReturnsDivisionsFromBusinessLogic()
    {
        var division1 = new NortheastMegabuck.Models.Division { Name = "Division 1" };
        var division2 = new NortheastMegabuck.Models.Division { Name = "Division 2" };
        var divisions = new[] { division1, division2 };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<TournamentId>())).Returns(divisions);

        var tournamentId = TournamentId.New();

        var actual = _adapter.Execute(tournamentId).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].DivisionName, Is.EqualTo(division1.Name));
            Assert.That(actual[1].DivisionName, Is.EqualTo(division2.Name));
        });
    }
}
