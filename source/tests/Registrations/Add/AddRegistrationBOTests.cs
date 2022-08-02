using NewEnglandClassic.Tests.Extensions;
using FluentValidation.TestHelper;

namespace NewEnglandClassic.Tests.Registrations.Add;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Divisions.Retrieve.IBusinessLogic> _getDivisionBO;
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<NewEnglandClassic.Bowlers.Retrieve.IBusinessLogic> _getBowlerBO;
    private Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Registration>> _validator;
    private Mock<NewEnglandClassic.Registrations.Add.IDataLayer> _dataLayer;

    private NewEnglandClassic.Registrations.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getDivisionBO = new Mock<NewEnglandClassic.Divisions.Retrieve.IBusinessLogic>();
        _getTournamentBO = new Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic>();
        _getBowlerBO = new Mock<NewEnglandClassic.Bowlers.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Registration>>();
        _dataLayer = new Mock<NewEnglandClassic.Registrations.Add.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Registrations.Add.BusinessLogic(_getDivisionBO.Object, _getTournamentBO.Object, _getBowlerBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_GetDivisionBOExecute_CalledCorrectly()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var divisionId = Guid.NewGuid();

        var registration = new NewEnglandClassic.Models.Registration
        {
            Division = new NewEnglandClassic.Models.Division { Id = divisionId }
        };

        _businessLogic.Execute(registration);

        _getDivisionBO.Verify(getDivisionBO => getDivisionBO.Execute(divisionId), Times.Once);
    }

    [Test]
    public void Execute_GetDivisionBOExecuteHasErrors_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _getDivisionBO.SetupGet(getDivisionBO => getDivisionBO.Error).Returns(error);

        var registration = new NewEnglandClassic.Models.Registration();

        var actual = _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _getBowlerBO.Verify(getBowlerBO => getBowlerBO.Execute(It.IsAny<BowlerId>()), Times.Never);
            _getTournamentBO.Verify(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>()), Times.Never);
            _validator.Verify(validator => validator.Validate(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionId_CalledCorrectly()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid()};
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();
        
        var registration = new NewEnglandClassic.Models.Registration();

        _businessLogic.Execute(registration);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.FromDivisionId(division.Id), Times.Once);
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionIdHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getDivisionBO => getDivisionBO.Error).Returns(error);

        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var registration = new NewEnglandClassic.Models.Registration();

        var actual = _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _getBowlerBO.Verify(getBowlerBO => getBowlerBO.Execute(It.IsAny<BowlerId>()), Times.Never);
            _validator.Verify(validator => validator.Validate(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_GetTournamentFromDivisionIdSuccessful_ValueSetOnRegistration()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament { Start = DateOnly.FromDateTime(DateTime.Today)};
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var registration = new NewEnglandClassic.Models.Registration();

        _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            Assert.That(registration.TournamentStartDate, Is.EqualTo(tournament.Start));

            _validator.Verify(validator => validator.Validate(It.Is<NewEnglandClassic.Models.Registration>(r => r.TournamentStartDate == tournament.Start)), Times.Once);
        });
    }

    [Test]
    public void Execute_GetDivisonBOExecuteSuccessful_ValiationAndDataLayerCalledWithReturnedDivision()
    {
        var division = new NewEnglandClassic.Models.Division();
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament { Start = DateOnly.FromDateTime(DateTime.Today) };
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var registration = new NewEnglandClassic.Models.Registration();

        _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(It.Is<NewEnglandClassic.Models.Registration>(r => r.Division == division)), Times.Once);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<NewEnglandClassic.Models.Registration>(r => r.Division == division)), Times.Once);
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_GetBowlerBOExecute_CalledCorrecctly()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var bowlerId = BowlerId.New();

        var registration = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = bowlerId }
        };

        _businessLogic.Execute(registration);

        _getBowlerBO.Verify(getBowlerBO => getBowlerBO.Execute(bowlerId), Times.Once);
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_BowlerIdEmpty_GetBowlerBOExecute_NotCalled()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var bowlerId = BowlerId.Empty;

        var registration = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = bowlerId }
        };

        _businessLogic.Execute(registration);

        _getBowlerBO.Verify(getBowlerBO => getBowlerBO.Execute(It.IsAny<BowlerId>()), Times.Never);
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_GetBowlerBOExecuteHasError_ErrorFlow()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _getBowlerBO.SetupGet(getBowlerBO => getBowlerBO.Error).Returns(error);

        var bowlerId = BowlerId.New();

        var registration = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = bowlerId }
        };

        var actual = _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_ValidatorAndDataLayerCalledWithReturnedBowler()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var bowler = new NewEnglandClassic.Models.Bowler();
        _getBowlerBO.Setup(getBowlerBO => getBowlerBO.Execute(It.IsAny<BowlerId>())).Returns(bowler);

        var bowlerId = BowlerId.New();

        var registration = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = bowlerId }
        };

        _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(It.Is<NewEnglandClassic.Models.Registration>(r => r.Bowler == bowler)), Times.Once);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<NewEnglandClassic.Models.Registration>(r => r.Bowler == bowler)), Times.Once);
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_BowlerIdEmpty_ValidatorAndDataLayerNotCalledWithAResponseFromGetBowlerBO()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var bowler = new NewEnglandClassic.Models.Bowler();
        _getBowlerBO.Setup(getBowlerBO => getBowlerBO.Execute(It.IsAny<BowlerId>())).Returns(bowler);

        var bowlerId = BowlerId.Empty;

        var registration = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = bowlerId }
        };

        _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(It.Is<NewEnglandClassic.Models.Registration>(r => r.Bowler == bowler)), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.Is<NewEnglandClassic.Models.Registration>(r => r.Bowler == bowler)), Times.Never);
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_ValidatorIsValid_False_ErrorFlow()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsNotValid("propertyName", "errorMessage");

        var registration = new NewEnglandClassic.Models.Registration();

        var actual = _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("errorMessage");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_ValidationIsValid_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>())).Throws(ex);

        var registration = new NewEnglandClassic.Models.Registration();

        var actual = _businessLogic.Execute(registration);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
        });
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_ValidationIsValid_ReeturnsDataLayerExecute()
    {
        var division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() };
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.FromDivisionId(It.IsAny<Guid>())).Returns(tournament);

        _validator.Validate_IsValid();

        var id = Guid.NewGuid();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>())).Returns(id);

        var registration = new NewEnglandClassic.Models.Registration();

        var actual = _businessLogic.Execute(registration);

        Assert.That(actual, Is.EqualTo(id));
    }
}
