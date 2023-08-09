
namespace NortheastMegabuck.Tests.Registrations.Delete;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Registrations.Delete.IDataLayer> _dataLayer;

    private NortheastMegabuck.Registrations.Delete.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Registrations.Delete.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Registrations.Delete.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_BowlerIdSquadId_DataLayerExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        _businessLogic.Execute(bowlerId, squadId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(bowlerId, squadId), Times.Once);
    }

    [Test]
    public void Execute_BowlerIdSquadId_DataLayerExecuteThrowsException_ErrorMapped()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<BowlerId>(), It.IsAny<SquadId>())).Throws(ex);

        _businessLogic.Execute(BowlerId.New(), SquadId.New());

        Assert.That(_businessLogic.Error.Message, Is.EqualTo(ex.Message));
    }

    [Test]
    public void Execute_RegistrationId_DataLayerExecute_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();

        _businessLogic.Execute(registrationId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(registrationId), Times.Once);
    }

    [Test]
    public void Execute_RegistrationId_DataLayerExecuteThrowsException_ErrorMapped()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<RegistrationId>())).Throws(ex);

        _businessLogic.Execute(RegistrationId.New());

        Assert.That(_businessLogic.Error.Message, Is.EqualTo(ex.Message));
    }
}
