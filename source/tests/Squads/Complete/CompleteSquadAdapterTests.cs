
namespace NortheastMegabuck.Tests.Squads.Complete;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Squads.Complete.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Squads.Complete.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Squads.Complete.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Squads.Complete.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _adapter.Execute(squadId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_ErrorSetToBusinessLogicError()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        _adapter.Execute(SquadId.New());

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }
}
