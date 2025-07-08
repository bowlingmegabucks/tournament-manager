
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
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ErrorSetToBusinessLogicError([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var tournamentId = TournamentId.New();

        await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsRegistrations()
    {
        var registrations = new[]
        {
            new NortheastMegabuck.Models.Registration{ Id = RegistrationId.New()},
            new NortheastMegabuck.Models.Registration{ Id = RegistrationId.New()}
        };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrations);

        var tournamentId = TournamentId.New();

        var actual = await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual.First().Id, Is.EqualTo(registrations[0].Id));
            Assert.That(actual.Last().Id, Is.EqualTo(registrations[registrations.Length - 1].Id));
        });
    }
}
