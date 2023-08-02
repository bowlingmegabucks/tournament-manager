
namespace NortheastMegabuck.Tests.Sweepers.Complete;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Sweepers.Complete.IDataLayer> _dataLayer;

    private NortheastMegabuck.Sweepers.Complete.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Sweepers.Complete.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Sweepers.Complete.BusinessLogic(_dataLayer.Object);
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
