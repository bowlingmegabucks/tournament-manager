
namespace NortheastMegabuck.Tests.Registrations.Delete;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Registrations.Delete.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Registrations.Delete.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Registrations.Delete.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Registrations.Delete.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BowlerIdSquadId_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        _adapter.Execute(bowlerId, squadId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(bowlerId, squadId), Times.Once);
    }

    [Test]
    public void Execute_BowlerIdSquadId_ErrorSetToBusinessLogicError()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        _adapter.Execute(BowlerId.New(), SquadId.New());

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void Execute_RegistrationId_BusinessLogicExecute_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();

        _adapter.Execute(registrationId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(registrationId), Times.Once);
    }

    [Test]
    public void Execute_RegistrationId_ErrorSetToBusinessLogicError()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        _adapter.Execute(RegistrationId.New());

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }
}
