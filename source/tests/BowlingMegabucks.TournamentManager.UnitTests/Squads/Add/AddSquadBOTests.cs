﻿using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.UnitTests.Extensions;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Squads.Add;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>> _getTournamentBO;
    private Mock<FluentValidation.IValidator<TournamentManager.Models.Squad>> _validator;
    private Mock<TournamentManager.Squads.Add.IDataLayer> _dataLayer;

    private TournamentManager.Squads.Add.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>>();
        _validator = new Mock<FluentValidation.IValidator<TournamentManager.Models.Squad>>();
        _dataLayer = new Mock<TournamentManager.Squads.Add.IDataLayer>();

        _businessLogic = new TournamentManager.Squads.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var squad = new TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squad, cancellationToken).ConfigureAwait(false);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.HandleAsync(It.Is<GetTournamentByIdQuery>(q => q.Id == squad.TournamentId), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOHasError_ErrorFlow()
    {
        var error = Error.Conflict();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(error);

        var squad = new TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage(error.Description);
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<TournamentManager.Models.Squad>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_TournamentAddedToSquad()
    {
        _validator.Validate_IsValid();

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new TournamentManager.Models.Squad
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

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squad, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(squad, cancellationToken), Times.Once);
            _validator.Verify(validator => validator.ValidateAsync(It.Is<TournamentManager.Models.Squad>(s => s.Tournament == tournament), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("invalid");

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new TournamentManager.Models.Squad
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
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new TournamentManager.Models.Squad
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
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Squad>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var squad = new TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = await _businessLogic.ExecuteAsync(squad, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
