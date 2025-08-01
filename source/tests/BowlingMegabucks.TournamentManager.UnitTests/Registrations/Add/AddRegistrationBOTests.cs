﻿using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Add;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<TournamentManager.Divisions.Retrieve.IBusinessLogic> _getDivisionBO;
    private Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<TournamentManager.Bowlers.Search.IBusinessLogic> _searchBowlerBO;
    private Mock<TournamentManager.Bowlers.Update.IBusinessLogic> _updateBowlerBO;
    private Mock<FluentValidation.IValidator<TournamentManager.Models.Registration>> _validator;
    private Mock<TournamentManager.Registrations.Add.IDataLayer> _dataLayer;

    private TournamentManager.Registrations.Add.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getDivisionBO = new Mock<TournamentManager.Divisions.Retrieve.IBusinessLogic>();
        _getTournamentBO = new Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic>();
        _searchBowlerBO = new Mock<TournamentManager.Bowlers.Search.IBusinessLogic>();
        _updateBowlerBO = new Mock<TournamentManager.Bowlers.Update.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<TournamentManager.Models.Registration>>();
        _dataLayer = new Mock<TournamentManager.Registrations.Add.IDataLayer>();

        _businessLogic = new TournamentManager.Registrations.Add.BusinessLogic(_getDivisionBO.Object, _getTournamentBO.Object, _searchBowlerBO.Object, _updateBowlerBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecute_Model_CalledCorrectly()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var divisionId = DivisionId.New();

        var registration = new TournamentManager.Models.Registration
        {
            Division = new TournamentManager.Models.Division { Id = divisionId }
        };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        _getDivisionBO.Verify(getDivisionBO => getDivisionBO.ExecuteAsync(divisionId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteHasErrors_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _getDivisionBO.SetupGet(getDivisionBO => getDivisionBO.ErrorDetail).Returns(error);

        var registration = new TournamentManager.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _searchBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>()), Times.Never);
            _getTournamentBO.Verify(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>()), Times.Never);
            _validator.Verify(validator => validator.Validate(It.IsAny<TournamentManager.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionId_CalledCorrectly()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var registration = new TournamentManager.Models.Registration();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.ExecuteAsync(division.Id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionIdHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getDivisionBO => getDivisionBO.ErrorDetail).Returns(error);

        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var registration = new TournamentManager.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _searchBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>()), Times.Never);
            _validator.Verify(validator => validator.Validate(It.IsAny<TournamentManager.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionIdSuccessful_ValueSetOnRegistration()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament { Start = DateOnly.FromDateTime(DateTime.Today), Sweepers = Enumerable.Repeat(new TournamentManager.Models.Sweeper(), 4) };
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var registration = new TournamentManager.Models.Registration();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(registration.TournamentStartDate, Is.EqualTo(tournament.Start));

            _validator.Verify(validator => validator.ValidateAsync(It.Is<TournamentManager.Models.Registration>(r => r.TournamentStartDate == tournament.Start && registration.TournamentSweeperCount == 4), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_ValidationAndDataLayerCalledWithReturnedDivision()
    {
        var division = new TournamentManager.Models.Division();
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament { Start = DateOnly.FromDateTime(DateTime.Today) };
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var registration = new TournamentManager.Models.Registration();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(It.Is<TournamentManager.Models.Registration>(r => r.Division == division), cancellationToken), Times.Once);
            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.Is<TournamentManager.Models.Registration>(r => r.Division == division), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_SearchBowlerBOExecute_CalledCorrectly()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament
        {
            Id = TournamentId.New()
        };
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var bowlerId = BowlerId.New();

        var registration = new TournamentManager.Models.Registration
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowlerId }
        };

        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        _searchBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(searchCriteria => searchCriteria.BowlerId == bowlerId && searchCriteria.RegisteredInTournament.Value == tournament.Id), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_BowlerIdEmpty_SearchBowlerBOExecute_Model_NotCalled()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var bowlerId = BowlerId.Empty;

        var registration = new TournamentManager.Models.Registration
        {
            Bowler = new TournamentManager.Models.Bowler { Id = bowlerId }
        };

        await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        _searchBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_ValidatorIsValid_False_ErrorFlow()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsNotValid("errorMessage");

        var registration = new TournamentManager.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("errorMessage");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_ValidationIsValid_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>())).Throws(ex);

        var registration = new TournamentManager.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_ValidationIsValid_ReturnsDataLayerExecute()
    {
        var division = new TournamentManager.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var tournament = new TournamentManager.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var id = RegistrationId.New();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var registration = new TournamentManager.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_DataLayerExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(bowlerId, squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_ReturnsDataLayerExecute()
    {
        var registration = new TournamentManager.Models.Registration();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registration);

        var actual = await _businessLogic.ExecuteAsync(BowlerId.New(), SquadId.New(), default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(registration));
    }
}
