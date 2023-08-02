
namespace NortheastMegabuck.Tests.Squads.Complete;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Squads.Complete.IDataLayer> _dataLayer;

    private NortheastMegabuck.Squads.Complete.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Squads.Complete.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Squads.Complete.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_DataLayerExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _businessLogic.Execute(squadId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("ex");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Throws(ex);

        _businessLogic.Execute(SquadId.New());

        Assert.That(_businessLogic.Error.Message, Is.EqualTo("ex"));
    }
}
