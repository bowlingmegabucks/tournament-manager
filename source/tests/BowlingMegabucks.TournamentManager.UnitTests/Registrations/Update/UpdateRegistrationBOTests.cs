using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Update;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<TournamentManager.Registrations.Update.IDataLayer> _dataLayer;
    private Mock<IQueryHandler<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery, Registration?>> _getRegistrationByIdQueryHandler;
    private Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic> _retrieveTournamentBusinessLogic;
    private Mock<TournamentManager.Scores.IRepository> _scoresRepository;

    private TournamentManager.Registrations.Update.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<TournamentManager.Registrations.Update.IDataLayer>();
        _getRegistrationByIdQueryHandler = new Mock<IQueryHandler<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery, Registration?>>();
        _retrieveTournamentBusinessLogic = new Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic>();
        _scoresRepository = new Mock<TournamentManager.Scores.IRepository>();

        _businessLogic = new TournamentManager.Registrations.Update.BusinessLogic(
            _dataLayer.Object,
            _getRegistrationByIdQueryHandler.Object,
            _retrieveTournamentBusinessLogic.Object,
            null!,
            null!,
            null!,
            _scoresRepository.Object);
    }

    #region AddSuperSweeperAsync Tests

    [Test]
    public async Task AddSuperSweeperAsync_DataLayerExecute_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        var registration = new Registration { SuperSweeper = false };
        var tournament = new Tournament { Sweepers = [new Sweeper()] };

        _getRegistrationByIdQueryHandler.Setup(h => h.HandleAsync(It.IsAny<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Registration?>.Success(registration));
        _retrieveTournamentBusinessLogic.Setup(bo => bo.ExecuteAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        await _businessLogic.AddSuperSweeperAsync(registrationId, default).ConfigureAwait(false);

        _dataLayer.Verify(dl => dl.ExecuteAsync(registrationId, true, default), Times.Once);
    }

    [Test]
    public async Task AddSuperSweeperAsync_RegistrationAlreadyHasSuperSweeper_ErrorSet()
    {
        var registrationId = RegistrationId.New();
        var registration = new Registration { SuperSweeper = true };

        _getRegistrationByIdQueryHandler.Setup(h => h.HandleAsync(It.IsAny<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Registration?>.Success(registration));

        await _businessLogic.AddSuperSweeperAsync(registrationId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Errors, Has.Count.EqualTo(1));
            Assert.That(_businessLogic.Errors.First().Message, Is.EqualTo("Bowler is already registered for the super sweeper."));
            _dataLayer.Verify(dl => dl.ExecuteAsync(It.IsAny<RegistrationId>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    #endregion

    #region RemoveSuperSweeperAsync Tests

    [Test]
    public async Task RemoveSuperSweeperAsync_DataLayerExecute_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        var registration = new Registration { SuperSweeper = true };

        _getRegistrationByIdQueryHandler.Setup(h => h.HandleAsync(It.IsAny<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Registration?>.Success(registration));
        _scoresRepository.Setup(repo => repo.DoesBowlerHaveAnySweeperScoresAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        await _businessLogic.RemoveSuperSweeperAsync(registrationId, default).ConfigureAwait(false);

        _dataLayer.Verify(dl => dl.ExecuteAsync(registrationId, false, default), Times.Once);
    }

    [Test]
    public async Task RemoveSuperSweeperAsync_RegistrationNotSuperSweeper_ErrorSet()
    {
        var registrationId = RegistrationId.New();
        var registration = new Registration { SuperSweeper = false };

        _getRegistrationByIdQueryHandler.Setup(h => h.HandleAsync(It.IsAny<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Registration?>.Success(registration));

        await _businessLogic.RemoveSuperSweeperAsync(registrationId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Errors, Has.Count.EqualTo(1));
            Assert.That(_businessLogic.Errors.First().Message, Is.EqualTo("Bowler is not registered for the super sweeper."));
            _dataLayer.Verify(dl => dl.ExecuteAsync(It.IsAny<RegistrationId>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task RemoveSuperSweeperAsync_BowlerHasSweeperScores_ErrorSet()
    {
        var registrationId = RegistrationId.New();
        var registration = new Registration { SuperSweeper = true };

        _getRegistrationByIdQueryHandler.Setup(h => h.HandleAsync(It.IsAny<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Registration?>.Success(registration));
        _scoresRepository.Setup(repo => repo.DoesBowlerHaveAnySweeperScoresAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await _businessLogic.RemoveSuperSweeperAsync(registrationId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Errors, Has.Count.EqualTo(1));
            Assert.That(_businessLogic.Errors.First().Message, Is.EqualTo("Cannot remove super sweeper when sweeper scores have been recorded."));
            _dataLayer.Verify(dl => dl.ExecuteAsync(It.IsAny<RegistrationId>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    [Test]
    public async Task RemoveSuperSweeperAsync_RegistrationNotFound_ErrorSet()
    {
        var registrationId = RegistrationId.New();
        var error = new ErrorDetail("Registration not found");

        _getRegistrationByIdQueryHandler.Setup(h => h.HandleAsync(It.IsAny<TournamentManager.Registrations.GetRegistrationById.GetRegistrationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Registration?>.Error(error));

        await _businessLogic.RemoveSuperSweeperAsync(registrationId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Errors, Has.Count.EqualTo(1));
            Assert.That(_businessLogic.Errors.First().Message, Is.EqualTo("Registration not found"));
            _dataLayer.Verify(dl => dl.ExecuteAsync(It.IsAny<RegistrationId>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        });
    }

    #endregion
}
