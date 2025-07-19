
namespace BowlingMegabucks.TournamentManager.Tests.Bowlers.Update;

[TestFixture]
internal sealed class Adapter
{
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.IBusinessLogic> _businessLogic;

    private BowlingMegabucks.TournamentManager.Bowlers.Update.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.IBusinessLogic>();

        _adapter = new BowlingMegabucks.TournamentManager.Bowlers.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsyncINameViewModel_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var firstName = "firstName";

        var personName = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>();
        personName.SetupGet(p => p.FirstName).Returns(firstName);

        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(bowlerId, personName.Object, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(bowlerId, It.Is<BowlingMegabucks.TournamentManager.Models.PersonName>(name => name.First == "firstName"), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsyncINameViewModel_ErrorDetailSetToBusinessLogic()
    {
        var errors = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error"), 3);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var personName = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>().Object;

        await _adapter.ExecuteAsync(BowlerId.New(), personName, default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }
}
