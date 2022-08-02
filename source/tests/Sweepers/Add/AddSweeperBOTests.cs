using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Sweepers.Add;

[TestFixture]
internal class BusinesLogic
{
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Sweeper>> _validator;
    private Mock<NewEnglandClassic.Sweepers.Add.IDataLayer> _dataLayer;

    private NewEnglandClassic.Sweepers.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Sweeper>>();
        _dataLayer = new Mock<NewEnglandClassic.Sweepers.Add.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Sweepers.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var sweeper = new NewEnglandClassic.Models.Sweeper
        { 
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(sweeper);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.Execute(sweeper.TournamentId), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getTournamentBO => getTournamentBO.Error).Returns(error);

        var sweeper = new NewEnglandClassic.Models.Sweeper
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(sweeper);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<NewEnglandClassic.Models.Sweeper>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Sweeper>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_TournamentAddedToSweeper()
    {
        _validator.Validate_IsValid();

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var sweeper = new NewEnglandClassic.Models.Sweeper
        {
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(sweeper);

        Assert.That(sweeper.Tournament, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var sweeper = new NewEnglandClassic.Models.Sweeper
        {
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(sweeper);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(sweeper), Times.Once);
            _validator.Verify(validator => validator.Validate(It.Is<NewEnglandClassic.Models.Sweeper>(s => s.Tournament == tournament)), Times.Once);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("property", "invalid");

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var sweeper = new NewEnglandClassic.Models.Sweeper
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(sweeper);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Sweeper>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var sweeper = new NewEnglandClassic.Models.Sweeper
        {
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(sweeper);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(sweeper), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Sweeper>())).Throws(ex);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var sweeper = new NewEnglandClassic.Models.Sweeper
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(sweeper);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("exception");
            Assert.That(actual, Is.Null);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_ReturnsDataLayerExecute()
    {
        _validator.Validate_IsValid();

        var guid = Guid.NewGuid();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Sweeper>())).Returns(guid);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var sweeper = new NewEnglandClassic.Models.Sweeper
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(sweeper);

        Assert.That(actual, Is.EqualTo(guid));
    }
}
