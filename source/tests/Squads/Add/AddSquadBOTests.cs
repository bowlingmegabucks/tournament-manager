using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Squads.Add;

[TestFixture]
internal class BusinesLogic
{
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Squad>> _validator;
    private Mock<NewEnglandClassic.Squads.Add.IDataLayer> _dataLayer;

    private NewEnglandClassic.Squads.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<NewEnglandClassic.Tournaments.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NewEnglandClassic.Models.Squad>>();
        _dataLayer = new Mock<NewEnglandClassic.Squads.Add.IDataLayer>();

        _businessLogic = new NewEnglandClassic.Squads.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var squad = new NewEnglandClassic.Models.Squad
        { 
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(squad);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.Execute(squad.TournamentId), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getTournamentBO => getTournamentBO.Error).Returns(error);

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(squad);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<NewEnglandClassic.Models.Squad>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Squad>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_TournamentAddedToSquad()
    {
        _validator.Validate_IsValid();

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(squad);

        Assert.That(squad.Tournament, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(squad);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(squad), Times.Once);
            _validator.Verify(validator => validator.Validate(It.Is<NewEnglandClassic.Models.Squad>(s => s.Tournament == tournament)), Times.Once);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("property", "invalid");

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(squad);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Squad>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.NewGuid()
        };

        _businessLogic.Execute(squad);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(squad), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Squad>())).Throws(ex);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(squad);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.HasErrorMessage("exception");
            Assert.That(actual, Is.Null);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_ReturnsDataLayerExecute()
    {
        _validator.Validate_IsValid();

        var guid = Guid.NewGuid();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NewEnglandClassic.Models.Squad>())).Returns(guid);

        var tournament = new NewEnglandClassic.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<Guid>())).Returns(tournament);

        var squad = new NewEnglandClassic.Models.Squad
        {
            TournamentId = Guid.NewGuid()
        };

        var actual = _businessLogic.Execute(squad);

        Assert.That(actual, Is.EqualTo(guid));
    }
}
