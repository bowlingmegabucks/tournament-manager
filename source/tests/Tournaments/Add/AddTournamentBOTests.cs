using FluentValidation;
using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Tournaments.Add;

[TestFixture]
internal class BusinessLogic
{
    private Mock<IValidator<NewEnglandClassic.Models.Tournament>> _validator;
    private Mock<NewEnglandClassic.Tournaments.Add.IDataLayer> _dataLayer;

    private NewEnglandClassic.Tournaments.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<IValidator<NewEnglandClassic.Models.Tournament>>();
        _dataLayer = new Mock<NewEnglandClassic.Tournaments.Add.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Tournaments.Add.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var tournament = new NewEnglandClassic.Models.Tournament();

        _businessLogic.Execute(tournament);

        _validator.Verify(validator => validator.Validate(tournament), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateFalse_ErrorFlow()
    {
        _validator.Validate_IsNotValid("property", "error");

        var tournament = new NewEnglandClassic.Models.Tournament();

        var result = _businessLogic.Execute(tournament);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.HasErrorMessage("error");
            Assert.That(result, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Tournament>()), Times.Never);
        });
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NewEnglandClassic.Models.Tournament();

        _businessLogic.Execute(tournament);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(tournament), Times.Once);
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();
        
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Tournament>())).Throws(ex);

        var tournament = new NewEnglandClassic.Models.Tournament();

        var result = _businessLogic.Execute(tournament);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _businessLogic.Errors.HasErrorMessage("exception");
        });
    }

    [Test]
    public void Execute_ValidatorValidateTrue_DataLayerExecuteReturnsGuid_GuidReturned()
    {
        _validator.Validate_IsValid();

        var guid = Guid.NewGuid();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Tournament>())).Returns(guid);

        var tournament = new NewEnglandClassic.Models.Tournament();

        var result = _businessLogic.Execute(tournament);

        Assert.That(result, Is.EqualTo(guid));
    }
}
