
namespace NortheastMegabuck.Tests.Bowlers.Update;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Bowlers.Update.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Bowlers.Update.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Bowlers.Update.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Bowlers.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_INameViewModel_BusinessLogicExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var firstName = "firstName";

        var personName = new Mock<NortheastMegabuck.Bowlers.Update.INameViewModel>();
        personName.SetupGet(p => p.Id).Returns(bowlerId);
        personName.SetupGet(p => p.FirstName).Returns(firstName);

        _adapter.Execute(personName.Object);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(bowlerId, It.Is<NortheastMegabuck.Models.PersonName>(name => name.First == "firstName")), Times.Once);
    }

    [Test]
    public void Execute_INameViewModel_ErrorDetailSetToBusinessLogic()
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 3);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var personName = new Mock<NortheastMegabuck.Bowlers.Update.INameViewModel>().Object;

        _adapter.Execute(personName);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }
}
