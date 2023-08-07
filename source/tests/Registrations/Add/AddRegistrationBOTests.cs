using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Registrations.Add;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Divisions.Retrieve.IBusinessLogic> _getDivisionBO;
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic> _getBowlerBO;
    private Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Registration>> _validator;
    private Mock<NortheastMegabuck.Registrations.Add.IDataLayer> _dataLayer;

    private NortheastMegabuck.Registrations.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getDivisionBO = new Mock<NortheastMegabuck.Divisions.Retrieve.IBusinessLogic>();
        _getTournamentBO = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();
        _getBowlerBO = new Mock<NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Registration>>();
        _dataLayer = new Mock<NortheastMegabuck.Registrations.Add.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Registrations.Add.BusinessLogic(_getDivisionBO.Object, _getTournamentBO.Object, _getBowlerBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecute_Model_CalledCorrectly()
    {
        var division = new NortheastMegabuck.Models.Division { Id = DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var divisionId = NortheastMegabuck.DivisionId.New();

        var registration = new NortheastMegabuck.Models.Registration
        {
            Division = new NortheastMegabuck.Models.Division { Id = divisionId }
        };

        await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        _getDivisionBO.Verify(getDivisionBO => getDivisionBO.Execute(divisionId), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteHasErrors_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _getDivisionBO.SetupGet(getDivisionBO => getDivisionBO.Error).Returns(error);

        var registration = new NortheastMegabuck.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _getBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>()), Times.Never);
            _getTournamentBO.Verify(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>()), Times.Never);
            _validator.Verify(validator => validator.Validate(It.IsAny<NortheastMegabuck.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionId_CalledCorrectly()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();
        
        var registration = new NortheastMegabuck.Models.Registration();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.ExecuteAsync(division.Id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionIdHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getDivisionBO => getDivisionBO.Error).Returns(error);

        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var registration = new NortheastMegabuck.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _getBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>()), Times.Never);
            _validator.Verify(validator => validator.Validate(It.IsAny<NortheastMegabuck.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionIdSuccessful_ValueSetOnRegistration()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament { Start = DateOnly.FromDateTime(DateTime.Today), Sweepers = Enumerable.Repeat(new NortheastMegabuck.Models.Sweeper(), 4)};
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var registration = new NortheastMegabuck.Models.Registration();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(registration.TournamentStartDate, Is.EqualTo(tournament.Start));

            _validator.Verify(validator => validator.ValidateAsync(It.Is<NortheastMegabuck.Models.Registration>(r => r.TournamentStartDate == tournament.Start && registration.SweeperCount == 4), cancellationToken), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisonBOExecuteSuccessful_ValiationAndDataLayerCalledWithReturnedDivision()
    {
        var division = new NortheastMegabuck.Models.Division();
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament { Start = DateOnly.FromDateTime(DateTime.Today) };
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var registration = new NortheastMegabuck.Models.Registration();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(It.Is<NortheastMegabuck.Models.Registration>(r => r.Division == division), cancellationToken), Times.Once);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<NortheastMegabuck.Models.Registration>(r => r.Division == division)), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_GetBowlerBOExecute_Model_CalledCorrecctly()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var bowlerId = BowlerId.New();

        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowlerId }
        };

        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        _getBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(bowlerId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_BowlerIdEmpty_GetBowlerBOExecute_Model_NotCalled()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var bowlerId = BowlerId.Empty;

        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowlerId }
        };

        await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        _getBowlerBO.Verify(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_GetBowlerBOExecuteHasError_ErrorFlow()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _getBowlerBO.SetupGet(getBowlerBO => getBowlerBO.Error).Returns(error);

        var bowlerId = BowlerId.New();

        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowlerId }
        };

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<NortheastMegabuck.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_ValidatorAndDataLayerCalledWithReturnedBowler()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var bowler = new NortheastMegabuck.Models.Bowler();
        _getBowlerBO.Setup(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler);

        var bowlerId = BowlerId.New();

        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowlerId }
        };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.ValidateAsync(It.Is<NortheastMegabuck.Models.Registration>(r => r.Bowler == bowler), cancellationToken), Times.Once);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<NortheastMegabuck.Models.Registration>(r => r.Bowler == bowler)), Times.Once);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_BowlerIdEmpty_ValidatorAndDataLayerNotCalledWithAResponseFromGetBowlerBO()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var bowler = new NortheastMegabuck.Models.Bowler();
        _getBowlerBO.Setup(getBowlerBO => getBowlerBO.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler);

        var bowlerId = BowlerId.Empty;

        var registration = new NortheastMegabuck.Models.Registration
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = bowlerId }
        };

        await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(It.Is<NortheastMegabuck.Models.Registration>(r => r.Bowler == bowler)), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<NortheastMegabuck.Models.Registration>(r => r.Bowler == bowler)), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_ValidatorIsValid_False_ErrorFlow()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsNotValid("errorMessage");

        var registration = new NortheastMegabuck.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("errorMessage");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_ValidationIsValid_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Registration>())).Throws(ex);

        var registration = new NortheastMegabuck.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
        });
    }

    [Test]
    public async Task ExecuteAsync_Model_GetDivisionBOExecuteSuccessful_ValidationIsValid_ReeturnsDataLayerExecute()
    {
        var division = new NortheastMegabuck.Models.Division { Id = NortheastMegabuck.DivisionId.New() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<DivisionId>())).Returns(division);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        _validator.Validate_IsValid();

        var id = RegistrationId.New();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Registration>())).Returns(id);

        var registration = new NortheastMegabuck.Models.Registration();

        var actual = await _businessLogic.ExecuteAsync(registration, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }

    [Test]
    public void Execute_BowlerIdSquadId_DataLayerExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        _businessLogic.Execute(bowlerId, squadId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(bowlerId, squadId), Times.Once);
    }

    [Test]
    public void Execute_BowlerIdSquadId_ReturnsDataLayerExecute()
    {
        var registration = new NortheastMegabuck.Models.Registration();
        _dataLayer.Setup(dataLayer=> dataLayer.Execute(It.IsAny<BowlerId>(), It.IsAny<SquadId>())).Returns(registration);

        var actual = _businessLogic.Execute(BowlerId.New(), SquadId.New());

        Assert.That(actual, Is.EqualTo(registration));
    }
}
