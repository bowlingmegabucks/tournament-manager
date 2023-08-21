using FluentValidation;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Tournaments.Add;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<IValidator<NortheastMegabuck.Models.Tournament>> _validator;
    private Mock<NortheastMegabuck.Tournaments.Add.IDataLayer> _dataLayer;

    private NortheastMegabuck.Tournaments.Add.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<IValidator<NortheastMegabuck.Models.Tournament>>();
        _dataLayer = new Mock<NortheastMegabuck.Tournaments.Add.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Tournaments.Add.BusinessLogic(_validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var tournament = new NortheastMegabuck.Models.Tournament();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournament, cancellationToken).ConfigureAwait(false);

        _validator.Verify(validator => validator.ValidateAsync(tournament, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateFalse_ErrorFlow()
    {
        _validator.Validate_IsNotValid("error");

        var tournament = new NortheastMegabuck.Models.Tournament();

        var result = await _businessLogic.ExecuteAsync(tournament, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(result, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Tournament>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournament, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(tournament, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();
        
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Tournament>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var tournament = new NortheastMegabuck.Models.Tournament();

        var result = await _businessLogic.ExecuteAsync(tournament, default).ConfigureAwait(false);

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

        var id = TournamentId.New();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Tournament>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var tournament = new NortheastMegabuck.Models.Tournament();

        var result = await _businessLogic.ExecuteAsync(tournament, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(id));
    }
}
