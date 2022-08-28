
namespace NewEnglandClassic.Tests.Registrations.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Registrations.Retrieve.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Registrations.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Registrations.Retrieve.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Registrations.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_TournamentId_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = NewEnglandClassic.TournamentId.New();

        _adapter.Execute(tournamentId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_ErrorSetToBusinessLogicError([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
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
            new NewEnglandClassic.Models.Registration{ Id = RegistrationId.New()},
            new NewEnglandClassic.Models.Registration{ Id = RegistrationId.New()}
        };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.TournamentId>())).Returns(registrations);

        var tournamentId = TournamentId.New();

        var actual = _adapter.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.First().Id, Is.EqualTo(registrations.First().Id));
            Assert.That(actual.Last().Id, Is.EqualTo(registrations.Last().Id));
        });
    }
}
