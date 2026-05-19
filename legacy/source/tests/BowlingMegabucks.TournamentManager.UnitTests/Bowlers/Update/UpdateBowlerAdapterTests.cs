
namespace BowlingMegabucks.TournamentManager.UnitTests.Bowlers.Update;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Bowlers.Update.IBusinessLogic> _businessLogic;

    private TournamentManager.Bowlers.Update.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Bowlers.Update.IBusinessLogic>();

        _adapter = new TournamentManager.Bowlers.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsyncINameViewModel_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var firstName = "firstName";

        var personName = new Mock<TournamentManager.Bowlers.Update.INameViewModel>();
        personName.SetupGet(p => p.FirstName).Returns(firstName);

        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(bowlerId, personName.Object, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(bowlerId, It.Is<TournamentManager.Models.PersonName>(name => name.First == "firstName"), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsyncINameViewModel_ErrorDetailSetToBusinessLogic()
    {
        var errors = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("error"), 3);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var personName = new Mock<TournamentManager.Bowlers.Update.INameViewModel>().Object;

        await _adapter.ExecuteAsync(BowlerId.New(), personName, default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }
}
