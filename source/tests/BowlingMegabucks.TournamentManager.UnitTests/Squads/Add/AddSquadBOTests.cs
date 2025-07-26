using BowlingMegabucks.TournamentManager.Tests.Extensions;

namespace BowlingMegabucks.TournamentManager.Tests.Squads.Add;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<FluentValidation.IValidator<BowlingMegabucks.TournamentManager.Models.Squad>> _validator;
    private Mock<BowlingMegabucks.TournamentManager.Squads.Add.IDataLayer> _dataLayer;

    private BowlingMegabucks.TournamentManager.Squads.Add.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<BowlingMegabucks.TournamentManager.Models.Squad>>();
        _dataLayer = new Mock<BowlingMegabucks.TournamentManager.Squads.Add.IDataLayer>();

        _businessLogic = new BowlingMegabucks.TournamentManager.Squads.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squad, cancellationToken).ConfigureAwait(false);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.ExecuteAsync(squad.TournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getTournamentBO => getTournamentBO.ErrorDetail).Returns(error);

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_TournamentAddedToSquad()
    {
        _validator.Validate_IsValid();

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.That(squad.Tournament, Is.EqualTo(tournament));
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squad, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(squad, cancellationToken), Times.Once);
            _validator.Verify(validator => validator.ValidateAsync(It.Is<BowlingMegabucks.TournamentManager.Models.Squad>(s => s.Tournament == tournament), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("invalid");

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squad, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(squad, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

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
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
