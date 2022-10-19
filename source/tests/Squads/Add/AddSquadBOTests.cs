using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Squads.Add;

[TestFixture]
internal class BusinesLogic
{
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Squad>> _validator;
    private Mock<NortheastMegabuck.Squads.Add.IDataLayer> _dataLayer;

    private NortheastMegabuck.Squads.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Squad>>();
        _dataLayer = new Mock<NortheastMegabuck.Squads.Add.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Squads.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var squad = new NortheastMegabuck.Models.Squad
        { 
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(squad);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.Execute(squad.TournamentId), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getTournamentBO => getTournamentBO.Error).Returns(error);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = _businessLogic.Execute(squad);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<NortheastMegabuck.Models.Squad>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Squad>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_TournamentAddedToSquad()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(squad);

        Assert.That(squad.Tournament, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(squad);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(squad), Times.Once);
            _validator.Verify(validator => validator.Validate(It.Is<NortheastMegabuck.Models.Squad>(s => s.Tournament == tournament)), Times.Once);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("invalid");

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = _businessLogic.Execute(squad);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Squad>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(squad);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(squad), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Squad>())).Throws(ex);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = _businessLogic.Execute(squad);

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

        var id = SquadId.New();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Squad>())).Returns(id);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var squad = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var actual = _businessLogic.Execute(squad);

        Assert.That(actual, Is.EqualTo(id));
    }
}
