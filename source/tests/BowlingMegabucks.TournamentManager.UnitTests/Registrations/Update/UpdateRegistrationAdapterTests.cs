namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Update;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Registrations.Update.IBusinessLogic> _businessLogic;

    private TournamentManager.Registrations.Update.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Registrations.Update.IBusinessLogic>();

        _adapter = new TournamentManager.Registrations.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task AddSuperSweeperAsync_BusinessLogic_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _adapter.AddSuperSweeperAsync(registrationId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(bl => bl.AddSuperSweeperAsync(registrationId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task RemoveSuperSweeperAsync_BusinessLogic_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _adapter.RemoveSuperSweeperAsync(registrationId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(bl => bl.RemoveSuperSweeperAsync(registrationId, cancellationToken), Times.Once);
    }

    [Test]
    public void Errors_ReturnsBusinessLogicErrors()
    {
        var errors = new List<TournamentManager.Models.ErrorDetail>
        {
            new("Error 1"),
            new("Error 2")
        };

        _businessLogic.SetupGet(bl => bl.Errors).Returns(errors);

        var result = _adapter.Errors;

        Assert.That(result, Is.EqualTo(errors));
    }
}
