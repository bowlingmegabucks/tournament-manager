using FluentValidation;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal class BusinessLogic
{
    private Mock<IValidator<NortheastMegabuck.Models.Division>> _validator;
    private Mock<NortheastMegabuck.Divisions.Add.IDataLayer> _dataLayer;

    private NortheastMegabuck.Divisions.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<IValidator<NortheastMegabuck.Models.Division>>();
        _dataLayer = new Mock<NortheastMegabuck.Divisions.Add.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Divisions.Add.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var division = new NortheastMegabuck.Models.Division();

        _businessLogic.Execute(division);

        _validator.Verify(validator => validator.Validate(division), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateFalse_ErrorFlow()
    {
        _validator.Validate_IsNotValid("property", "error");

        var division = new NortheastMegabuck.Models.Division();

        var result = _businessLogic.Execute(division);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(result, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Division>()), Times.Never);
        });
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var division = new NortheastMegabuck.Models.Division();

        _businessLogic.Execute(division);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(division), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();
        
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Division>())).Throws(ex);

        var division = new NortheastMegabuck.Models.Division();

        var result = _businessLogic.Execute(division);

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

        var divisionId = NortheastMegabuck.DivisionId.New();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Division>())).Returns(divisionId);

        var division = new NortheastMegabuck.Models.Division();

        var result = _businessLogic.Execute(division);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
