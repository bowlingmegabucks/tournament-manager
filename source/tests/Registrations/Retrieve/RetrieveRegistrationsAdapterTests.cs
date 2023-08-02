
namespace NortheastMegabuck.Tests.Registrations.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Registrations.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Registrations.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Registrations.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Registrations.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_TournamentId_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = NortheastMegabuck.TournamentId.New();

        _adapter.Execute(tournamentId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_ErrorSetToBusinessLogicError([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var tournamentId = TournamentId.New();

        _adapter.Execute(tournamentId);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void Execute_TournamentId_ReturnsRegistrations()
    {
        var registrations = new[]
        {
            new NortheastMegabuck.Models.Registration{ Id = RegistrationId.New()},
            new NortheastMegabuck.Models.Registration{ Id = RegistrationId.New()}
        };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NortheastMegabuck.TournamentId>())).Returns(registrations);

        var tournamentId = TournamentId.New();

        var actual = _adapter.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.First().Id, Is.EqualTo(registrations.First().Id));
            Assert.That(actual.Last().Id, Is.EqualTo(registrations.Last().Id));
        });
    }
}
