using System.Linq.Expressions;
using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;
using ErrorOr;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.UpdateRegistration;

[TestFixture]
public sealed class UpdateRegistrationCommandHandlerTests
{
    private Mock<TournamentManager.Registrations.IRepository> _mockRegistrationRepository;
    private Mock<TournamentManager.Scores.IRepository> _mockScoresRepository;
    private IValidator<UpdateRegistrationRecord> _registrationValidator;
    private Mock<TournamentManager.Tournaments.IRepository> _mockTournamentRepository;
    private TournamentManager.Registrations.IPaymentEntityMapper _paymentEntityMapper;

    private UpdateRegistrationCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockRegistrationRepository = new Mock<TournamentManager.Registrations.IRepository>();
        _mockScoresRepository = new Mock<TournamentManager.Scores.IRepository>();
        _registrationValidator = new Validator();
        _mockTournamentRepository = new Mock<TournamentManager.Tournaments.IRepository>();
        _paymentEntityMapper = new TournamentManager.Registrations.PaymentEntityMapper();

        _handler = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommandHandler(
            _mockRegistrationRepository.Object,
            _mockScoresRepository.Object,
            _mockTournamentRepository.Object,
            _paymentEntityMapper,
            _registrationValidator);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenRegistrationIsNotFound()
    {
        // Arrange
        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = RegistrationId.New()
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Registration)null);

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.NotFound));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.NotFound"));
        Assert.That(result.FirstError.Description, Is.EqualTo($"Registration with ID {command.Id} not found."));

        _mockRegistrationRepository.Verify(repository => repository.RetrieveAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenASquadIdProvidedDoesNotBelongToTheTournament()
    {
        // Arrange
        var validSquad1 = SquadId.New();
        var validSquad2 = SquadId.New();
        var validSweeper1 = SquadId.New();
        var validSweeper2 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads = [.. new List<SquadId> { validSquad1, validSquad2 }
                .Select(squadId => new TournamentSquad { Id = squadId })],
            Sweepers = [.. new List<SquadId> { validSweeper1, validSweeper2 }
                .Select(squadId => new SweeperSquad { Id = squadId })],
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads = []
        };
        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var invalidSquadId1 = SquadId.New();
        var invalidSquadId2 = SquadId.New();
        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SquadIds = [validSquad1, invalidSquadId1, invalidSquadId2] // Invalid squad ID
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.InvalidSquadIds"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Invalid squad IDs"));
        Assert.That(result.FirstError.Metadata["InvalidSquadIds"], Is.EqualTo($"{invalidSquadId1}, {invalidSquadId2}"));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenASweeperIdProvidedDoesNotBelongToTheTournament()
    {
        // Arrange
        var validSquad1 = SquadId.New();
        var validSquad2 = SquadId.New();
        var validSweeper1 = SquadId.New();
        var validSweeper2 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads = [.. new List<SquadId> { validSquad1, validSquad2 }
                .Select(squadId => new TournamentSquad { Id = squadId })],
            Sweepers = [.. new List<SquadId> { validSweeper1, validSweeper2 }
                .Select(squadId => new SweeperSquad { Id = squadId })],
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads = []
        };
        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>());

        var invalidSweeperId1 = SquadId.New();
        var invalidSweeperId2 = SquadId.New();
        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SweeperIds = [validSweeper1, invalidSweeperId1, invalidSweeperId2] // Invalid sweeper IDs
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.InvalidSweeperIds"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Invalid sweeper IDs"));
        Assert.That(result.FirstError.Metadata["InvalidSweeperIds"], Is.EqualTo($"{invalidSweeperId1}, {invalidSweeperId2}"));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenAddedSquadHasAlreadyBeenCompleted()
    {
        // Arrange
        var completedSquadId1 = SquadId.New();
        var completedSquadId2 = SquadId.New();

        var nonCompletedSquadId = SquadId.New();
        var otherTournamentSquad = SquadId.New();
        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads = [.. new List<SquadId> { completedSquadId1, completedSquadId2, nonCompletedSquadId, otherTournamentSquad }
                .Select(squadId => new TournamentSquad { Id = squadId, Complete = new[] {completedSquadId1,completedSquadId2}.Contains(squadId) })],
            Sweepers = [.. new List<SquadId> { SquadId.New(), SquadId.New() }
                .Select(squadId => new SweeperSquad { Id = squadId, Complete = false })],
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads = [new SquadRegistration { SquadId = otherTournamentSquad, RegistrationId = registrationId }]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SquadIds = [completedSquadId1, completedSquadId2, nonCompletedSquadId, otherTournamentSquad]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.InvalidSquadIds"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Cannot add squad(s) that are already complete."));
        Assert.That(result.FirstError.Metadata["InvalidSquadIds"], Is.EqualTo($"{completedSquadId1}, {completedSquadId2}"));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenAddedSweeperHasAlreadyBeenCompleted()
    {
        // Arrange
        var completedSquadId1 = SquadId.New();
        var completedSquadId2 = SquadId.New();

        var nonCompletedSquadId = SquadId.New();
        var otherTournamentSquad = SquadId.New();
        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads = [.. new List<SquadId> { SquadId.New(), SquadId.New() }
                .Select(squadId => new TournamentSquad { Id = squadId })],
            Sweepers = [.. new List<SquadId> { completedSquadId1, completedSquadId2, nonCompletedSquadId, otherTournamentSquad }
                .Select(squadId => new SweeperSquad { Id = squadId, Complete = new[] {completedSquadId1,completedSquadId2}.Contains(squadId) })]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads = [new SquadRegistration { SquadId = otherTournamentSquad, RegistrationId = registrationId }]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SweeperIds = [completedSquadId1, completedSquadId2, nonCompletedSquadId, otherTournamentSquad]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.InvalidSweeperIds"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Cannot add sweeper(s) that are already complete."));
        Assert.That(result.FirstError.Metadata["InvalidSweeperIds"], Is.EqualTo($"{completedSquadId1}, {completedSquadId2}"));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerHasBowledInARemovedSquad()
    {
        // Arrange
        var bowledSquadId = SquadId.New();
        var squadIdToAdd = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads = [.. new List<SquadId> { bowledSquadId, squadIdToAdd }
                .Select(squadId => new TournamentSquad { Id = squadId })],
            Sweepers = [.. new List<SquadId> { SquadId.New()}
                .Select(squadId => new SweeperSquad { Id = squadId })]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads = [new SquadRegistration { SquadId = bowledSquadId, Squad = new TournamentSquad { Id = bowledSquadId }, RegistrationId = registrationId }]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([.. new List<SquadScore> { new() { SquadId = bowledSquadId, BowlerId = existingRegistration.BowlerId, Score = 200, Game = 1 } }]);

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SquadIds = [squadIdToAdd]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.BowlerHasBowled"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Bowler has already bowled in removed squads."));
        Assert.That(result.FirstError.Metadata["RemovedSquadIds"], Is.EqualTo($"{bowledSquadId}"));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerHasBowledInARemovedSweeper()
    {
        // Arrange
        var bowledSquadId = SquadId.New();
        var squadIdToAdd = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Sweepers = [.. new List<SquadId> { bowledSquadId, squadIdToAdd }
                .Select(squadId => new SweeperSquad { Id = squadId })],
            Squads = [.. new List<SquadId> { SquadId.New()}
                .Select(squadId => new TournamentSquad { Id = squadId })]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads = [new SquadRegistration { SquadId = bowledSquadId, Squad = new SweeperSquad { Id = bowledSquadId }, RegistrationId = registrationId }]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.SetupSequence(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>())
            .ReturnsAsync([.. new List<SquadScore> { new() { SquadId = bowledSquadId, BowlerId = existingRegistration.BowlerId, Score = 200, Game = 1 } }]);

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SweeperIds = [squadIdToAdd]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.BowlerHasBowled"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Bowler has already bowled in removed sweeper(s)."));
        Assert.That(result.FirstError.Metadata["RemovedSweeperIds"], Is.EqualTo($"{bowledSquadId}"));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateRegistration_WhenOnlySquadsAreChangedAndIsAValidChange()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var squadId3 = SquadId.New();

        var squadId4 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 },
                new() { Id = squadId2 },
                new() { Id = squadId3 }
            ],
            Sweepers =
            [
                new() { Id = squadId4 },
                new() { Id = SquadId.New() }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = squadId4, Squad = new SweeperSquad { Id = squadId4 }, RegistrationId = registrationId }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SquadIds = [squadId2, squadId3]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validSquadIds =
            registration => registration.Squads.Select(s => s.SquadId).Count() == 3
                && registration.Squads.Any(s => s.SquadId == squadId2)
                && registration.Squads.Any(s => s.SquadId == squadId3)
                && registration.Squads.Any(s => s.SquadId == squadId4);

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.Is(validSquadIds), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenNotAllSweepersAreRegisteredAndSuperSweeperWasTrue()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var squadId3 = SquadId.New();

        var squadId4 = SquadId.New();
        var squadId5 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 },
                new() { Id = squadId2 },
                new() { Id = squadId3 }
            ],
            Sweepers =
            [
                new() { Id = squadId4 },
                new() { Id = squadId5 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },

                new SquadRegistration { SquadId = squadId4, Squad = new SweeperSquad { Id = squadId4 }, RegistrationId = registrationId }
            ],
            SuperSweeper = true
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SweeperIds = [squadId4]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.NotAllSweepersRegistered"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Super Sweeper cannot be enrolled when not all sweepers are registered."));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenNotAllUpdatedSweepersAreRegistered([Values] bool superSweeper)
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var squadId3 = SquadId.New();

        var squadId4 = SquadId.New();
        var squadId5 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 },
                new() { Id = squadId2 },
                new() { Id = squadId3 }
            ],
            Sweepers =
            [
                new() { Id = squadId4 },
                new() { Id = squadId5 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },

                new SquadRegistration { SquadId = squadId4, Squad = new SweeperSquad { Id = squadId4 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = squadId5, Squad = new SweeperSquad { Id = squadId5 }, RegistrationId = registrationId }
            ],
            SuperSweeper = superSweeper
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SweeperIds = [squadId4],
            SuperSweeper = true
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.NotAllSweepersRegistered"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Super Sweeper cannot be enrolled when not all sweepers are registered."));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateRegistration_WhenOnlySweepersAreChangedAndIsAValidChange()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var squadId3 = SquadId.New();

        var squadId4 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Sweepers =
            [
                new() { Id = squadId1 },
                new() { Id = squadId2 },
                new() { Id = squadId3 }
            ],
            Squads =
            [
                new() { Id = squadId4 },
                new() { Id = SquadId.New() }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new SweeperSquad { Id = squadId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = squadId4, Squad = new TournamentSquad { Id = squadId4 }, RegistrationId = registrationId }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SweeperIds = [squadId2, squadId3]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validSquadIds =
            registration => registration.Squads.Select(s => s.SquadId).Count() == 3
                && registration.Squads.Any(s => s.SquadId == squadId2)
                && registration.Squads.Any(s => s.SquadId == squadId3)
                && registration.Squads.Any(s => s.SquadId == squadId4);

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.Is(validSquadIds), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateRegistration_WhenSquadsAndSweepersAreChanged()
    {
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var squadId3 = SquadId.New();

        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();
        var sweeperId3 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 },
                new() { Id = squadId2 },
                new() { Id = squadId3 }
            ],
            Sweepers =
            [
                new() { Id = sweeperId1 },
                new() { Id = sweeperId2 },
                new() { Id = sweeperId3 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = squadId2, Squad = new TournamentSquad { Id = squadId2 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId1, Squad = new SweeperSquad { Id = sweeperId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId2, Squad = new SweeperSquad { Id = sweeperId2 }, RegistrationId = registrationId }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SquadIds = [squadId2, squadId3],
            SweeperIds = [sweeperId2, sweeperId3]
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validSquadIds =
            registration => registration.Squads.Select(s => s.SquadId).Count() == 4
                && registration.Squads.Any(s => s.SquadId == squadId2)
                && registration.Squads.Any(s => s.SquadId == squadId3)
                && registration.Squads.Any(s => s.SquadId == sweeperId2)
                && registration.Squads.Any(s => s.SquadId == sweeperId3);

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.Is(validSquadIds), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenTryingToUpdateSuperSweeperWhenSweeperScoresHaveBeenBowled([Values] bool superSweeper)
    {
        // Arrange
        var squadId1 = SquadId.New();

        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 }
            ],
            Sweepers =
            [
                new() { Id = sweeperId1 },
                new() { Id = sweeperId2 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId1, Squad = new SweeperSquad { Id = sweeperId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId2, Squad = new SweeperSquad { Id = sweeperId2 }, RegistrationId = registrationId }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.Retrieve(It.Is<SquadId[]>(squadIds => squadIds.Contains(sweeperId1) || squadIds.Contains(sweeperId2))))
            .Returns(new List<SquadScore> {
            new()
            {
                SquadId = sweeperId1,
                BowlerId = existingRegistration.BowlerId,
                Score = 200,
                Game = 1
            }}.AsQueryable());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SuperSweeper = superSweeper
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.SuperSweeperScoresExist"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Cannot change Super Sweeper when sweeper scores have been recorded."));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateRegistration_WhenSuperSweeperIsSetToFalse([Values] bool superSweeper)
    {
        // Arrange
        var squadId1 = SquadId.New();

        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 }
            ],
            Sweepers =
            [
                new() { Id = sweeperId1 },
                new() { Id = sweeperId2 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId1, Squad = new SweeperSquad { Id = sweeperId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId2, Squad = new SweeperSquad { Id = sweeperId2 }, RegistrationId = registrationId }
            ],
            SuperSweeper = superSweeper
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.Retrieve(It.Is<SquadId[]>(squadIds => squadIds.Contains(sweeperId1) || squadIds.Contains(sweeperId2))))
            .Returns(new List<SquadScore>().AsQueryable());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SuperSweeper = false
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validSquadIds =
            registration => registration.Squads.Select(s => s.SquadId).Count() == 3
                && registration.Squads.Any(s => s.SquadId == squadId1)
                && registration.Squads.Any(s => s.SquadId == sweeperId1)
                && registration.Squads.Any(s => s.SquadId == sweeperId2)
                && !registration.SuperSweeper;

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.Is(validSquadIds), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenSuperSweeperIsSetToTrueAndNotAllSweepersArePreviouslyRegistered()
    {
        // Arrange
        var squadId1 = SquadId.New();

        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 }
            ],
            Sweepers =
            [
                new() { Id = sweeperId1 },
                new() { Id = sweeperId2 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId1, Squad = new SweeperSquad { Id = sweeperId1 }, RegistrationId = registrationId }
            ],
            SuperSweeper = true
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.Retrieve(It.Is<SquadId[]>(squadIds => squadIds.Contains(sweeperId1) || squadIds.Contains(sweeperId2))))
            .Returns(new List<SquadScore>().AsQueryable());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,

            SuperSweeper = true
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.InvalidSuperSweeper"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Cannot set Super Sweeper when not all sweepers are registered."));

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateRegistration_WhenSuperSweeperIsSetToTrueAndSweeperUpdateDoesContainAllSweepers()
    {
        // Arrange
        var squadId1 = SquadId.New();

        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 }
            ],
            Sweepers =
            [
                new() { Id = sweeperId1 },
                new() { Id = sweeperId2 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId1, Squad = new SweeperSquad { Id = sweeperId1 }, RegistrationId = registrationId },
            ],
            SuperSweeper = false
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SweeperIds = new[] { sweeperId1, sweeperId2 },
            SuperSweeper = true
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validSquadIds =
            registration => registration.Squads.Select(s => s.SquadId).Count() == 3
                && registration.Squads.Any(s => s.SquadId == squadId1)
                && registration.Squads.Any(s => s.SquadId == sweeperId1)
                && registration.Squads.Any(s => s.SquadId == sweeperId2)
                && registration.SuperSweeper;

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.Is(validSquadIds), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateRegistration_WhenSuperSweeperIsSetToTrueAndAllSweepersAreAlreadyRegistered()
    {
        // Arrange
        var squadId1 = SquadId.New();

        var sweeperId1 = SquadId.New();
        var sweeperId2 = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new() { Id = squadId1 }
            ],
            Sweepers =
            [
                new() { Id = sweeperId1 },
                new() { Id = sweeperId2 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads =
            [
                new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId },

                new SquadRegistration { SquadId = sweeperId1, Squad = new SweeperSquad { Id = sweeperId1 }, RegistrationId = registrationId },
                new SquadRegistration { SquadId = sweeperId2, Squad = new SweeperSquad { Id = sweeperId2 }, RegistrationId = registrationId }
            ],
            SuperSweeper = false
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockScoresRepository.Setup(repo => repo.BowlerScoresForSquads(existingRegistration.BowlerId, It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(new List<SquadScore>());

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            SuperSweeper = true
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validSquadIds =
            registration => registration.Squads.Select(s => s.SquadId).Count() == 3
                && registration.Squads.Any(s => s.SquadId == squadId1)
                && registration.Squads.Any(s => s.SquadId == sweeperId1)
                && registration.Squads.Any(s => s.SquadId == sweeperId2)
                && registration.SuperSweeper;

        _mockTournamentRepository.Verify(repository => repository.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.Is(validSquadIds), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenDivisionIdDoesNotExistForTournament()
    {
        // Assert
        var tournamentDivisionId1 = DivisionId.New();
        var tournamentDivisionId2 = DivisionId.New();

        var tournament = new Tournament
        {
            Divisions =
            [
                new () { Id = tournamentDivisionId1 },
                new () { Id = tournamentDivisionId2 }
            ]
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division
            {
                Id = tournamentDivisionId1,
                TournamentId = tournament.Id,
                Tournament = tournament
            },
            Squads = [],
            SuperSweeper = false
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(existingRegistration.Division.TournamentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var command = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = DivisionId.New()
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.InvalidDivisionId"));
        Assert.That(result.FirstError.Description, Is.EqualTo("Division ID is not valid for the tournament."));
        Assert.That(result.FirstError.Metadata["ValidDivisionIds"], Is.EqualTo($"{tournamentDivisionId1}, {tournamentDivisionId2}"));

        _mockRegistrationRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenExistingAverageIsNullAndDivisionHasMinAverage()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ]
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        existingRegistration.Average = null;
        newDivision.MinimumAverage = 200;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Average is required for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenExistingAverageIsNullAndDivisionHasMaxAverage()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ]
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        existingRegistration.Average = null;
        newDivision.MaximumAverage = 200;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Average is required for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenExistingAverageIsNullAndDivisionHasMinAndMaxAverage()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ]
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        existingRegistration.Average = null;
        newDivision.MinimumAverage = 200;
        newDivision.MaximumAverage = 300;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Average is required for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenExistingAverageDoesNotMeetTheMinimumAverageRequirement()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ]
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        existingRegistration.Average = 200;
        newDivision.MinimumAverage = 201;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Minimum average requirement for division not met"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenExistingAverageDoesNotMeetTheMaximumAverageRequirement()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ]
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        existingRegistration.Average = 201;
        newDivision.MaximumAverage = 200;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Maximum average requirement for division not met"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerHasNoDateOfBirthWithMinimumAgeAndNoGenderInclusion()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ]
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.DateOfBirth = null;
        newDivision.MinimumAge = 50;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Date of birth required for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerHasNoDateOfBirthWithMaximumAgeAndNoGenderInclusion()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ]
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.DateOfBirth = null;
        newDivision.MaximumAge = 50;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Date of birth required for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerIsTooYoungAndNoGenderInclusion()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ],
            Start = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.DateOfBirth = tournament.Start.AddYears(-20);
        newDivision.MinimumAge = 21;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Bowler too young for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerIsTooOldAndNoGenderInclusion()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ],
            Start = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.DateOfBirth = tournament.Start.AddYears(-20);
        newDivision.MaximumAge = 19;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Bowler too old for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerDoesNotHaveUsbcIdNumberForHandicapDivision()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ],
            Start = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New(),
            USBCId = "123-45678"
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.USBCId = "";
        newDivision.HandicapPercentage = .8m;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "USBC Id is required for Handicap Divisions"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerDoesNotHaveGenderInGenderDivision()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ],
            Start = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New(),
            USBCId = "123-45678"
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.Gender = null;
        newDivision.Gender = TournamentManager.Models.Gender.Female;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Gender is required for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerIsWrongGenderInGenderDivision()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ],
            Start = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New(),
            USBCId = "123-45678"
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.Gender = TournamentManager.Models.Gender.Male;
        newDivision.Gender = TournamentManager.Models.Gender.Female;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.All(e => e.Type == ErrorType.Validation), Is.True);
        Assert.That(result.Errors.Any(e => e.Description == "Invalid gender for selected division"), Is.True);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateRegistration_WhenBowlerMeetsDivisionRequirements()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperId = SquadId.New();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
                new () { Id = squadId2 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId }
            ],
            Start = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var currentDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();

        var currentDivision = new Division
        {
            Id = currentDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };
        var newDivision = new Division
        {
            Id = newDivisionId,
            TournamentId = tournament.Id,
            Tournament = tournament
        };

        tournament.Divisions =
        [
            currentDivision,
            newDivision
        ];

        var bowler = new Bowler
        {
            Id = BowlerId.New()
        };

        var registrationId = RegistrationId.New();
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = bowler.Id,
            Bowler = bowler,
            Division = currentDivision,
            Squads = [.. new[] {squadId1, squadId2}.Select(s => new SquadRegistration
            {
                SquadId = s,
                Squad = new TournamentSquad { Id = s },
                RegistrationId = registrationId
            })],
            SuperSweeper = false,
            Average = 200
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        bowler.Gender = TournamentManager.Models.Gender.Male;
        bowler.DateOfBirth = tournament.Start.AddYears(-19);
        bowler.USBCId = "123-45678";

        newDivision.Gender = TournamentManager.Models.Gender.Female;
        newDivision.MinimumAge = 18;
        newDivision.MaximumAge = 20;
        newDivision.MinimumAverage = 150;
        newDivision.MaximumAverage = 152;

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            DivisionId = newDivisionId,
            Average = 151
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validRegistration =
            registration => registration.DivisionId == newDivisionId
                && registration.Average == 151;

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.Is(validRegistration), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldAddPaymentConfirmation_WhenNewPaymentConfirmationIsProvided()
    {
        // Arrange
        var squadId1 = SquadId.New();
        var sweeperId1 = new SquadId();

        var tournament = new Tournament
        {
            Id = TournamentId.New(),
            Squads =
            [
                new () { Id = squadId1 },
            ],
            Sweepers =
            [
                new () { Id = sweeperId1 }
            ],
            Start = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        };

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var registrationId = RegistrationId.New();
        var existingPayment = new Payment
        {
            Id = PaymentId.New(),
            RegistrationId = registrationId,
            Amount = 100,
            CreatedAtUtc = DateTime.UtcNow.AddDays(-4),
            ConfirmationCode = "12345"
        };
        var existingRegistration = new Registration
        {
            Id = registrationId,
            BowlerId = BowlerId.New(),
            Division = new Division { TournamentId = tournament.Id },
            Squads = [new SquadRegistration { SquadId = squadId1, Squad = new TournamentSquad { Id = squadId1 }, RegistrationId = registrationId }],
            SuperSweeper = false,
            Average = 200,
            Payments =
            [
                existingPayment
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(registrationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        var newPayment = new TournamentManager.Models.Payment
        {
            Amount = 200,
            ConfirmationCode = "67890"
        };

        var command = new UpdateRegistrationCommand
        {
            Id = registrationId,
            Payment = newPayment
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(Result.Updated));

        Expression<Func<Registration, bool>> validRegistration =
            registration => registration.Payments.All(p => p.ConfirmationCode == newPayment.ConfirmationCode || p.ConfirmationCode == existingPayment.ConfirmationCode)
            && registration.Payments.Count == 2
            && registration.Payments.All(payment => payment.CreatedAtUtc != DateTime.MinValue)
            && registration.Payments.All(payment => payment.RegistrationId == registrationId);

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.Is(validRegistration), It.IsAny<CancellationToken>()), Times.Once);
    }
}