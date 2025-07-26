using FluentValidation;
using BowlingMegabucks.TournamentManager.Tests.Extensions;

namespace BowlingMegabucks.TournamentManager.Tests.Divisions.Add;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<IValidator<BowlingMegabucks.TournamentManager.Models.Division>> _validator;
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IDataLayer> _dataLayer;

    private BowlingMegabucks.TournamentManager.Divisions.Add.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<IValidator<BowlingMegabucks.TournamentManager.Models.Division>>();
        _dataLayer = new Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IDataLayer>();

        _businessLogic = new BowlingMegabucks.TournamentManager.Divisions.Add.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var division = new BowlingMegabucks.TournamentManager.Models.Division();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(division, cancellationToken).ConfigureAwait(true);

        _validator.Verify(validator => validator.ValidateAsync(division, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateFalse_ErrorFlow()
    {
        _validator.Validate_IsNotValid("error");

        var division = new BowlingMegabucks.TournamentManager.Models.Division();

        var result = await _businessLogic.ExecuteAsync(division, default).ConfigureAwait(true);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(result, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var division = new BowlingMegabucks.TournamentManager.Models.Division();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(division, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(division, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var division = new BowlingMegabucks.TournamentManager.Models.Division();

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

        var divisionId = BowlingMegabucks.TournamentManager.DivisionId.New();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisionId);

        var division = new BowlingMegabucks.TournamentManager.Models.Division();

        var result = await _businessLogic.ExecuteAsync(division, default).ConfigureAwait(true);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
