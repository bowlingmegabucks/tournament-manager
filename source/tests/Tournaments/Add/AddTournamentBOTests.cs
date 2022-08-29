using FluentValidation;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Tournaments.Add;

[TestFixture]
internal class BusinessLogic
{
    private Mock<IValidator<NortheastMegabuck.Models.Tournament>> _validator;
    private Mock<NortheastMegabuck.Tournaments.Add.IDataLayer> _dataLayer;

    private NortheastMegabuck.Tournaments.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<IValidator<NortheastMegabuck.Models.Tournament>>();
        _dataLayer = new Mock<NortheastMegabuck.Tournaments.Add.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Tournaments.Add.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var tournament = new NortheastMegabuck.Models.Tournament();

        _businessLogic.Execute(tournament);

        _validator.Verify(validator => validator.Validate(tournament), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateFalse_ErrorFlow()
    {
        _validator.Validate_IsNotValid("property", "error");

        var tournament = new NortheastMegabuck.Models.Tournament();

        var result = _businessLogic.Execute(tournament);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(result, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Tournament>()), Times.Never);
        });
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();

        _businessLogic.Execute(tournament);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(tournament), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();
        
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Tournament>())).Throws(ex);

        var tournament = new NortheastMegabuck.Models.Tournament();

        var result = _businessLogic.Execute(tournament);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
        });
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecuteReturnsId_IdReturned()
    {
        _validator.Validate_IsValid();

        var id = TournamentId.New();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Tournament>())).Returns(id);

        var tournament = new NortheastMegabuck.Models.Tournament();

        var result = _businessLogic.Execute(tournament);

        Assert.That(result, Is.EqualTo(id));
    }
}
