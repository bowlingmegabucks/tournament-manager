using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Sweepers.Add;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<FluentValidation.IValidator<TournamentManager.Models.Sweeper>> _validator;
    private Mock<TournamentManager.Sweepers.Add.IDataLayer> _dataLayer;

    private TournamentManager.Sweepers.Add.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<TournamentManager.Models.Sweeper>>();
        _dataLayer = new Mock<TournamentManager.Sweepers.Add.IDataLayer>();

        _businessLogic = new TournamentManager.Sweepers.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(sweeper, cancellationToken).ConfigureAwait(false);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.ExecuteAsync(sweeper.TournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getTournamentBO => getTournamentBO.ErrorDetail).Returns(error);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(sweeper, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.ValidateAsync(It.IsAny<TournamentManager.Models.Sweeper>(), It.IsAny<CancellationToken>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Sweeper>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_TournamentAddedToSweeper()
    {
        _validator.Validate_IsValid();

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        await _businessLogic.ExecuteAsync(sweeper, default).ConfigureAwait(false);

        Assert.That(sweeper.Tournament, Is.EqualTo(tournament));
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(sweeper, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(sweeper, cancellationToken), Times.Once);
            _validator.Verify(validator => validator.ValidateAsync(It.Is<TournamentManager.Models.Sweeper>(s => s.Tournament == tournament), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("invalid");

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(sweeper, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Sweeper>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        CancellationToken cancellationToken = default;
        await _businessLogic.ExecuteAsync(sweeper, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(sweeper, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Sweeper>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(sweeper, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
            Assert.That(actual, Is.Null);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorIsValid_ReturnsDataLayerExecute()
    {
        _validator.Validate_IsValid();

        var id = SquadId.New();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Sweeper>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var sweeper = new TournamentManager.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(sweeper, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
