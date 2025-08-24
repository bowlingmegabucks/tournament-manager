using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Registrations.CreateRegistration;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.UnitTests.Extensions;
using ErrorOr;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.CreateRegistration;

[TestFixture]
public sealed class CreateRegistrationCommandHandlerTest
{
    private Mock<TournamentManager.Divisions.Retrieve.IBusinessLogic> _mockGetDivision;
    private Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>> _mockGetTournament;
    private Mock<IValidator<TournamentManager.Models.Registration>> _mockValidator;
    private Mock<TournamentManager.Bowlers.Search.IBusinessLogic> _mockSearchBowlers;
    private Mock<TournamentManager.Bowlers.Update.IBusinessLogic> _mockUpdateBowlers;
    private Mock<TournamentManager.Registrations.IEntityMapper> _mockEntityMapper;
    private Mock<TournamentManager.Registrations.IRepository> _mockRepository;
    private Mock<ILogger<CreateRegistrationCommandHandler>> _mockLogger;

    private CreateRegistrationCommandHandler _commandHandler;

    [SetUp]
    public void SetUp()
    {
        _mockGetDivision = new Mock<TournamentManager.Divisions.Retrieve.IBusinessLogic>();
        _mockGetTournament = new Mock<IQueryHandler<GetTournamentByIdQuery, TournamentManager.Models.Tournament>>();
        _mockValidator = new Mock<IValidator<TournamentManager.Models.Registration>>();
        _mockSearchBowlers = new Mock<TournamentManager.Bowlers.Search.IBusinessLogic>();
        _mockUpdateBowlers = new Mock<TournamentManager.Bowlers.Update.IBusinessLogic>();
        _mockEntityMapper = new Mock<TournamentManager.Registrations.IEntityMapper>();
        _mockRepository = new Mock<TournamentManager.Registrations.IRepository>();
        _mockLogger = new Mock<ILogger<CreateRegistrationCommandHandler>>();

        _commandHandler = new CreateRegistrationCommandHandler(
            _mockGetDivision.Object,
            _mockGetTournament.Object,
            _mockValidator.Object,
            _mockSearchBowlers.Object,
            _mockUpdateBowlers.Object,
            _mockEntityMapper.Object,
            _mockRepository.Object,
            _mockLogger.Object
        );
    }

    [Test]
    public void HandleAsync_ShouldThrowArgumentNullException_WhenCommandIsNull()
    {
        // Arrange
        CreateRegistrationCommand command = null;

        // Act & Assert
        // ReSharper disable once AssignNullToNotNullAttribute (this is intentional)
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _commandHandler.HandleAsync(command, CancellationToken.None));
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenBowlerIsAlreadyRegisteredForTheTournament()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(searchCriteria
            => searchCriteria.UsbcId == command.Bowler.USBCId
                && searchCriteria.RegisteredInTournament == command.TournamentId), It.IsAny<CancellationToken>()))
                .ReturnsAsync([new TournamentManager.Models.Bowler()]);

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);

        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Conflict));
        Assert.That(result.FirstError.Description, Is.EqualTo("Bowler is already registered for this tournament."));

        _mockRepository.Verify(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenCheckingForExistingRegistrationFails()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.SetupGet(searchBowlers => searchBowlers.ErrorDetail).Returns(new TournamentManager.Models.ErrorDetail("Database error"));

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Failure));
        Assert.That(result.FirstError.Description, Is.EqualTo("Database error"));

        _mockRepository.Verify(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenGettingDivisionInformationFails()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([
                ]);

        _mockGetDivision.SetupGet(getDivision => getDivision.ErrorDetail).Returns(new TournamentManager.Models.ErrorDetail("Division error"));

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Failure));
        Assert.That(result.FirstError.Description, Is.EqualTo("Division error"));

        _mockRepository.Verify(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenGettingTournamentInformationFails()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([
                ]);

        _mockGetDivision.Setup(getDivision => getDivision
            .ExecuteAsync(
                It.Is<DivisionId>(id => id == command.DivisionId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Models.Division { Id = command.DivisionId });

        _mockGetTournament.Setup(getTournament => getTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Error.Failure(description: "Tournament Error"));

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Failure));
        Assert.That(result.FirstError.Description, Is.EqualTo("Tournament Error"));

        _mockRepository.Verify(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenValidationFails()
    {
        // Arrange
        var squad1Id = SquadId.New();
        var squad2Id = SquadId.New();

        var sweeper1Id = SquadId.New();
        var sweeper2Id = SquadId.New();

        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [squad1Id, squad2Id],
            Sweepers = [sweeper1Id, sweeper2Id],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            },
            SuperSweeper = true
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([
                ]);

        var mockDivision = new TournamentManager.Models.Division { Id = command.DivisionId };
        _mockGetDivision.Setup(getDivision => getDivision
            .ExecuteAsync(
                It.Is<DivisionId>(id => id == command.DivisionId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockDivision);

        var mockTournament = new TournamentManager.Models.Tournament
        {
            Id = command.TournamentId,
            Start = new DateOnly(2025, 1, 2),
            Sweepers = [new TournamentManager.Models.Sweeper(), new TournamentManager.Models.Sweeper(), new TournamentManager.Models.Sweeper()]
        };

        _mockGetTournament.Setup(getTournament => getTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockTournament);

        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Validation Error"));

        _mockValidator.Setup(validator => validator.ValidateAsync(It.Is<TournamentManager.Models.Registration>(registration =>
            registration.Bowler == command.Bowler &&
            registration.Division == mockDivision &&
            registration.TournamentStartDate == mockTournament.Start &&
            registration.TournamentSweeperCount == 3 &&
            registration.Squads.Select(squad => squad.Id).Contains(squad1Id) &&
            registration.Squads.Select(squad => squad.Id).Contains(squad2Id) &&
            registration.Sweepers.Select(sweeper => sweeper.Id).Contains(sweeper1Id) &&
            registration.Sweepers.Select(sweeper => sweeper.Id).Contains(sweeper2Id) &&
            registration.SuperSweeper == command.SuperSweeper), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Description, Is.EqualTo("Validation Error"));

        _mockRepository.Verify(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateBowlerInformation_WhenTheBowlerAlreadyExists()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
                .ReturnsAsync([
                ]);

        _mockGetDivision.Setup(getDivision => getDivision
            .ExecuteAsync(
                It.Is<DivisionId>(id => id == command.DivisionId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Models.Division { Id = command.DivisionId });
        _mockGetTournament.Setup(getTournament => getTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new TournamentManager.Models.Tournament { Id = command.TournamentId });
        _mockValidator.Validate_IsValid();
        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => !criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
            .ReturnsAsync([new TournamentManager.Models.Bowler { Id = BowlerId.New() }]);

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);

        Assert.That(command.Bowler.Id, Is.Not.EqualTo(BowlerId.Empty));

        _mockUpdateBowlers.Verify(updateBowler => updateBowler.ExecuteAsync(It.Is<TournamentManager.Models.Bowler>(bowler => bowler == command.Bowler), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenSearchingForExistingBowlerFails()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
                .ReturnsAsync([
                ]);

        _mockGetDivision.Setup(getDivision => getDivision
            .ExecuteAsync(
                It.Is<DivisionId>(id => id == command.DivisionId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Models.Division { Id = command.DivisionId });
        _mockGetTournament.Setup(getTournament => getTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new TournamentManager.Models.Tournament { Id = command.TournamentId });
        _mockValidator.Validate_IsValid();
        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => !criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
            .ReturnsAsync([
            ]);
        
        var errorDetail = new TournamentManager.Models.ErrorDetail("Database error 2");
        var callCount = 0;
        _mockSearchBowlers.SetupGet(searchBowlers => searchBowlers.ErrorDetail)
            .Returns(() => callCount++ == 0 ? null : errorDetail);

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Failure));
        Assert.That(result.FirstError.Description, Is.EqualTo("Database error 2"));
        
        _mockSearchBowlers.Verify(searchBowlers => searchBowlers.ExecuteAsync(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>()), Times.Exactly(2));

        _mockUpdateBowlers.Verify(updateBowler => updateBowler.ExecuteAsync(It.IsAny<TournamentManager.Models.Bowler>(), It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Test]
    public async Task HandleAsync_ShouldReturnAnError_WhenUpdatingBowlerFails()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
                .ReturnsAsync([
                ]);

        _mockGetDivision.Setup(getDivision => getDivision
            .ExecuteAsync(
                It.Is<DivisionId>(id => id == command.DivisionId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Models.Division { Id = command.DivisionId });
        _mockGetTournament.Setup(getTournament => getTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new TournamentManager.Models.Tournament { Id = command.TournamentId });
        _mockValidator.Validate_IsValid();
        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => !criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
            .ReturnsAsync([new TournamentManager.Models.Bowler { Id = BowlerId.New() }]);

        _mockUpdateBowlers.SetupGet(updateBowler => updateBowler.Errors)
            .Returns([new TournamentManager.Models.ErrorDetail("Update bowler error")]);

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Failure));
        Assert.That(result.FirstError.Description, Is.EqualTo("Update bowler error"));
        
        _mockUpdateBowlers.Verify(updateBowler => updateBowler.ExecuteAsync(It.Is<TournamentManager.Models.Bowler>(bowler => bowler == command.Bowler && bowler.Id != BowlerId.Empty), It.IsAny<CancellationToken>()), Times.Once);

        _mockRepository.Verify(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>()), Times.Never);
    }
    
    // test when bowler doesn't exist that we don't call update bowler
    [Test]
    public async Task HandleAsync_ShouldNotCallUpdateBowler_WhenBowlerDoesNotExist()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
                .ReturnsAsync([
                ]);

        _mockGetDivision.Setup(getDivision => getDivision
            .ExecuteAsync(
                It.Is<DivisionId>(id => id == command.DivisionId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Models.Division { Id = command.DivisionId });
        _mockGetTournament.Setup(getTournament => getTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new TournamentManager.Models.Tournament { Id = command.TournamentId });
        _mockValidator.Validate_IsValid();
        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => !criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);

        _mockUpdateBowlers.Verify(updateBowler => updateBowler.ExecuteAsync(It.IsAny<TournamentManager.Models.Bowler>(), It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Test]
    public async Task HandleAsync_ShouldAddRegistrationToRepository_WhenAllConditionsAreMet()
    {
        // Arrange
        var command = new CreateRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = [SquadId.New()],
            Payment = new TournamentManager.Models.Payment
            {
                ConfirmationCode = "CONFIRM123",
                Amount = 100.00m
            }
        };

        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
            .ReturnsAsync([
            ]);

        _mockGetDivision.Setup(getDivision => getDivision
            .ExecuteAsync(
                It.Is<DivisionId>(id => id == command.DivisionId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Models.Division { Id = command.DivisionId });
        _mockGetTournament.Setup(getTournament => getTournament.HandleAsync(It.IsAny<GetTournamentByIdQuery>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new TournamentManager.Models.Tournament { Id = command.TournamentId });
        _mockValidator.Validate_IsValid();
        _mockSearchBowlers.Setup(searchBowlers => searchBowlers.ExecuteAsync(It.Is<TournamentManager.Models.BowlerSearchCriteria>(criteria => !criteria.RegisteredInTournament.HasValue), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var registrationEntity = new TournamentManager.Database.Entities.Registration();
        _mockEntityMapper.Setup(mapper => mapper.Execute(It.
                Is<TournamentManager.Models.Registration>(registration => registration.Bowler == command.Bowler)))
            .Returns(registrationEntity);
        
        _mockRepository.Setup(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(RegistrationId.New());

        // Act
        var result = await _commandHandler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.Not.EqualTo(RegistrationId.Empty));

        _mockRepository.Verify(repository => repository.AddAsync(registrationEntity, It.IsAny<CancellationToken>()), Times.Once);
    }
}