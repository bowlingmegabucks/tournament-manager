using NewEnglandClassic.Tests.Extensions;
using FluentValidation.TestHelper;

namespace NewEnglandClassic.Tests.Registrations.Add;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NewEnglandClassic.Divisions.Retrieve.IBusinessLogic> _getDivisionBO;
    private Mock<NewEnglandClassic.Bowlers.Retrieve.IBusinessLogic> _getBowlerBO;
    private Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Registration>> _validator;
    private Mock<NewEnglandClassic.Registrations.Add.IDataLayer> _dataLayer;

    private NewEnglandClassic.Registrations.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getDivisionBO = new Mock<NewEnglandClassic.Divisions.Retrieve.IBusinessLogic>();
        _getBowlerBO = new Mock<NewEnglandClassic.Bowlers.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Registration>>();
        _dataLayer = new Mock<NewEnglandClassic.Registrations.Add.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Registrations.Add.BusinessLogic(_getDivisionBO.Object, _getBowlerBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_GetDivisionBOExecute_CalledCorrectly()
    {
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

            _getBowlerBO.Verify(getBowlerBO => getBowlerBO.Execute(It.IsAny<Guid>()), Times.Never);
            _validator.Verify(validator => validator.Validate(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetDivisonBOExecuteSuccessful_ValiationAndDataLayerCalledWithReturnedDivision()
    {
        var division = new NewEnglandClassic.Models.Division();
        _getDivisionBO.Setup(getDivisionBO => getDivisionBO.Execute(It.IsAny<Guid>())).Returns(division);

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
        _validator.Validate_IsValid();

        var bowlerId = Guid.NewGuid();

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
        _validator.Validate_IsValid();

        var bowlerId = Guid.Empty;

        var registration = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = bowlerId }
        };

        _businessLogic.Execute(registration);

        _getBowlerBO.Verify(getBowlerBO => getBowlerBO.Execute(It.IsAny<Guid>()), Times.Never);
    }

    [Test]
    public void Execute_GetDivisionBOExecuteSuccessful_BowlerIdNotEmpty_GetBowlerBOExecuteHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _getBowlerBO.SetupGet(getBowlerBO => getBowlerBO.Error).Returns(error);

        var bowlerId = Guid.NewGuid();

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
        _validator.Validate_IsValid();

        var bowler = new NewEnglandClassic.Models.Bowler();
        _getBowlerBO.Setup(getBowlerBO => getBowlerBO.Execute(It.IsAny<Guid>())).Returns(bowler);

        var bowlerId = Guid.NewGuid();

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
        _validator.Validate_IsValid();

        var bowler = new NewEnglandClassic.Models.Bowler();
        _getBowlerBO.Setup(getBowlerBO => getBowlerBO.Execute(It.IsAny<Guid>())).Returns(bowler);

        var bowlerId = Guid.Empty;

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
        _validator.Validate_IsValid();

        var id = Guid.NewGuid();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Registration>())).Returns(id);

        var registration = new NewEnglandClassic.Models.Registration();

        var actual = _businessLogic.Execute(registration);

        Assert.That(actual, Is.EqualTo(id));
    }
}
