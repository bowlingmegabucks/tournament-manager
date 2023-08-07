using FluentValidation;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal sealed class BusinessLogic
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
    public async Task ExecuteAsync_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var division = new NortheastMegabuck.Models.Division();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(division, cancellationToken).ConfigureAwait(true);

        _validator.Verify(validator => validator.ValidateAsync(division, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateFalse_ErrorFlow()
    {
        _validator.Validate_IsNotValid("error");

        var division = new NortheastMegabuck.Models.Division();

        var result = await _businessLogic.ExecuteAsync(division, default).ConfigureAwait(true);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(result, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var division = new NortheastMegabuck.Models.Division();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(division, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(division, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();
        
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var division = new NortheastMegabuck.Models.Division();

        var result = await _businessLogic.ExecuteAsync(division, default).ConfigureAwait(true);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
        });
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateTrue_DataLayerExecuteReturnsId_IdReturned()
    {
        _validator.Validate_IsValid();

        var divisionId = NortheastMegabuck.DivisionId.New();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisionId);

        var division = new NortheastMegabuck.Models.Division();

        var result = await _businessLogic.ExecuteAsync(division, default).ConfigureAwait(true);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
