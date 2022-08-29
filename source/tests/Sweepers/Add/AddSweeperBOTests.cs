using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Sweepers.Add;

[TestFixture]
internal class BusinesLogic
{
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Sweeper>> _validator;
    private Mock<NortheastMegabuck.Sweepers.Add.IDataLayer> _dataLayer;

    private NortheastMegabuck.Sweepers.Add.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _getTournamentBO = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();
        _validator = new Mock<FluentValidation.IValidator<NortheastMegabuck.Models.Sweeper>>();
        _dataLayer = new Mock<NortheastMegabuck.Sweepers.Add.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Sweepers.Add.BusinessLogic(_getTournamentBO.Object, _validator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_GetTournamentBOExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();
        
        var sweeper = new NortheastMegabuck.Models.Sweeper
        { 
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(sweeper);

        _getTournamentBO.Verify(getTournamentBO => getTournamentBO.Execute(sweeper.TournamentId), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _getTournamentBO.SetupGet(getTournamentBO => getTournamentBO.Error).Returns(error);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var actual = _businessLogic.Execute(sweeper);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("error");
            Assert.That(actual, Is.Null);

            _validator.Verify(validator => validator.Validate(It.IsAny<NortheastMegabuck.Models.Sweeper>()), Times.Never);
            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Sweeper>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_TournamentAddedToSweeper()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(sweeper);

        Assert.That(sweeper.Tournament, Is.EqualTo(tournament));
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidate_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(sweeper);

        Assert.Multiple(() =>
        {
            _validator.Verify(validator => validator.Validate(sweeper), Times.Once);
            _validator.Verify(validator => validator.Validate(It.Is<NortheastMegabuck.Models.Sweeper>(s => s.Tournament == tournament)), Times.Once);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorValidateFails_ErrorFlow()
    {
        _validator.Validate_IsNotValid("property", "invalid");

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var actual = _businessLogic.Execute(sweeper);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("invalid");
            Assert.That(actual, Is.Null);

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Sweeper>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecute_CalledCorrectly()
    {
        _validator.Validate_IsValid();

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        _businessLogic.Execute(sweeper);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(sweeper), Times.Once);
    }

    [Test]
    public void Execute_GetTournamentBOSuccessful_ValidatorIsValid_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _validator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Sweeper>())).Throws(ex);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
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

        var id = SquadId.New();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<NortheastMegabuck.Models.Sweeper>())).Returns(id);

        var tournament = new NortheastMegabuck.Models.Tournament();
        _getTournamentBO.Setup(getTournamentBO => getTournamentBO.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var sweeper = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var actual = _businessLogic.Execute(sweeper);

        Assert.That(actual, Is.EqualTo(id));
    }
}
