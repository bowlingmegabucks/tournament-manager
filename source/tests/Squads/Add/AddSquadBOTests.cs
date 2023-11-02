using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Squads.Add;

[TestFixture]
internal sealed class BusinesLogic
{
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Squad>> _validator;
    private Mock<NortheastMegabuck.Squads.Add.IDataLayer> _dataLayer;

    private NortheastMegabuck.Squads.Add.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Squad>>();
        _dataLayer = new Mock<NortheastMegabuck.Squads.Add.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Squads.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var squad = new NortheastMegabuck.Models.Squad
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
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getTournamentBO => getTournamentBO.Error).Returns(error);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<NortheastMegabuck.Models.Squad>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Squad>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_TournamentAddedToSquad()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new NortheastMegabuck.Models.Squad
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

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squad, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(squad, cancellationToken), Times.Once);
            _validator.Verify(validator => validator.ValidateAsync(It.Is<NortheastMegabuck.Models.Squad>(s => s.Tournament == tournament), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("invalid");

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Squad>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new NortheastMegabuck.Models.Squad
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
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Squad>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new NortheastMegabuck.Models.Squad
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
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Squad>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
