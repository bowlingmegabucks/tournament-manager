using BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.DeleteRegistration;

[TestFixture]
public sealed class DeleteRegistrationCommandHandlerTests
{
    private Mock<TournamentManager.Registrations.IRepository> _registrationRepository = null!;
    private Mock<TournamentManager.Scores.IRepository> _scoresRepository = null!;

    private DeleteRegistrationCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _registrationRepository = new Mock<TournamentManager.Registrations.IRepository>(MockBehavior.Strict);
        _scoresRepository = new Mock<TournamentManager.Scores.IRepository>(MockBehavior.Strict);

        _handler = new DeleteRegistrationCommandHandler(_registrationRepository.Object, _scoresRepository.Object);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenRegistrationNotFound()
    {
        // Arrange
        _registrationRepository
            .Setup(repository => repository.RetrieveAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TournamentManager.Database.Entities.Registration)null);

        var command = new DeleteRegistrationCommand { Id = RegistrationId.New() };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        _registrationRepository.Verify(repository => repository.RetrieveAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        Assert.That(result.IsError);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("RegistrationNotFound"));
        Assert.That(result.FirstError.Description, Is.EqualTo($"Registration with ID {command.Id} not found."));

        _registrationRepository.VerifyNoOtherCalls();
    }

    [Test]
    public async Task Handle_ShouldReturnAnError_WhenBowlerHasScoresForTheTournament()
    {
        // Arrange
        var registration = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        _registrationRepository
            .Setup(repository => repository.RetrieveAsync(registration.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(registration);

        _scoresRepository
            .Setup(repository => repository.DoesBowlerHaveAnyScoresForTournamentAsync(registration.Id, registration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new DeleteRegistrationCommand { Id = registration.Id };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("RegistrationHasScores"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Cannot delete registration with scores."));

        _registrationRepository
            .Verify(repository => repository.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Handle_ShouldReturnDeleted_WhenBowlerHasNotBowledYet()
    {
        // Arrange
        var registration = new TournamentManager.Database.Entities.Registration
        {
            Id = RegistrationId.New(),
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        _registrationRepository
            .Setup(repository => repository.RetrieveAsync(registration.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(registration);

        _registrationRepository
            .Setup(r => r.DeleteAsync(It.IsAny<RegistrationId>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _scoresRepository
            .Setup(repository => repository.DoesBowlerHaveAnyScoresForTournamentAsync(registration.Id, registration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new DeleteRegistrationCommand { Id = registration.Id };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Deleted));

        _registrationRepository
            .Verify(repository => repository.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }
}