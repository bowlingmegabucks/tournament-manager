using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Tournaments.Add;

[TestFixture]
internal class Adapter
{
    private Mock<NewEnglandClassic.Tournaments.Add.IBusinessLogic> _businessLogic;

    private NewEnglandClassic.Tournaments.Add.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NewEnglandClassic.Tournaments.Add.IBusinessLogic>();

        _adapter = new NewEnglandClassic.Tournaments.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new NewEnglandClassic.Tournaments.ViewModel
        { 
            TournamentName = "name"
        };

        _adapter.Execute(viewModel);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(It.Is<NewEnglandClassic.Models.Tournament>(tournament => tournament.Name == "name")), Times.Once);
    }

    [Test]
    public void Execute_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new NewEnglandClassic.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel();

        _adapter.Execute(viewModel);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public void Execute_ReturnsBusinessLogicGuid()
    {
        var guid = Guid.NewGuid();
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<NewEnglandClassic.Models.Tournament>())).Returns(guid);

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel();

        var result = _adapter.Execute(viewModel);

        Assert.That(result, Is.EqualTo(guid));
    }
}
