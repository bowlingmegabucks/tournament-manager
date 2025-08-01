using FluentValidation;
using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Bowlers.Update;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<IValidator<TournamentManager.Models.PersonName>> _nameValidator;
    private Mock<TournamentManager.Bowlers.Update.IUpdateBowlerValidator> _bowlerValidator;

    private Mock<TournamentManager.Bowlers.Update.IDataLayer> _dataLayer;

    private TournamentManager.Bowlers.Update.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _nameValidator = new Mock<IValidator<TournamentManager.Models.PersonName>>();
        _bowlerValidator = new Mock<TournamentManager.Bowlers.Update.IUpdateBowlerValidator>();
        _dataLayer = new Mock<TournamentManager.Bowlers.Update.IDataLayer>();

        _businessLogic = new TournamentManager.Bowlers.Update.BusinessLogic(_nameValidator.Object, _bowlerValidator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdPersonName_NameValidatorValidate_CalledCorrectly()
    {
        _nameValidator.Validate_IsValid();

        var bowlerId = BowlerId.New();
        var name = new TournamentManager.Models.PersonName();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(bowlerId, name, cancellationToken).ConfigureAwait(false);

        _nameValidator.Verify(validator => validator.ValidateAsync(name, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdPersonName_NameValidatorValidateFalse_ErrorFlow()
    {
        _nameValidator.Validate_IsNotValid("errorMessage");

        var bowlerId = BowlerId.New();
        var name = new TournamentManager.Models.PersonName();

        await _businessLogic.ExecuteAsync(bowlerId, name, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("errorMessage");

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<TournamentManager.Models.PersonName>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdPersonName_NameValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _nameValidator.Validate_IsValid();

        var bowlerId = BowlerId.New();
        var name = new TournamentManager.Models.PersonName();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(bowlerId, name, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(bowlerId, name, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdPersonName_NameValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _nameValidator.Validate_IsValid();

        var ex = new InvalidOperationException("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<TournamentManager.Models.PersonName>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var bowlerId = BowlerId.New();
        var name = new TournamentManager.Models.PersonName();

        await _businessLogic.ExecuteAsync(bowlerId, name, default).ConfigureAwait(false);

        _businessLogic.Errors.Assert_HasErrorMessage("exception");
    }
}
