using FluentValidation;
using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Divisions.Add;

[TestFixture]
internal class BusinessLogic
{
    private Mock<IValidator<NewEnglandClassic.Models.Division>> _validator;
    private Mock<NewEnglandClassic.Divisions.Add.IDataLayer> _dataLayer;

    private NewEnglandClassic.Divisions.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<IValidator<NewEnglandClassic.Models.Division>>();
        _dataLayer = new Mock<NewEnglandClassic.Divisions.Add.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Divisions.Add.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var division = new NewEnglandClassic.Models.Division();

        _businessLogic.Execute(division);

        _validator.Verify(validator => validator.Validate(division), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateFalse_ErrorFlow()
    {
        _validator.Validate_IsNotValid("property", "error");

        var division = new NewEnglandClassic.Models.Division();

        var result = _businessLogic.Execute(division);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(result, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Division>()), Times.Never);
        });
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var division = new NewEnglandClassic.Models.Division();

        _businessLogic.Execute(division);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(division), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();
        
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Division>())).Throws(ex);

        var division = new NewEnglandClassic.Models.Division();

        var result = _businessLogic.Execute(division);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
        });
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecuteReturnsGuid_GuidReturned()
    {
        _validator.Validate_IsValid();

        var guid = Guid.NewGuid();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Division>())).Returns(guid);

        var division = new NewEnglandClassic.Models.Division();

        var result = _businessLogic.Execute(division);

        Assert.That(result, Is.EqualTo(guid));
    }
}
