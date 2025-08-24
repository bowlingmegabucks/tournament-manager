using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.Registrations.AppendRegistration;
using ErrorOr;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.AppendRegistration;

[TestFixture]
public sealed class AppendRegistrationCommandHandlerTests
{
    private Mock<TournamentManager.Registrations.IRepository> _mockRegistrationRepository;
    private Mock<TournamentManager.Tournaments.IRepository> _mockTournamentRepository;
    private Mock<TournamentManager.Divisions.IRepository> _mockDivisionRepository;
    private Mock<TournamentManager.Squads.IRepository> _mockSquadRepository;
    private Mock<TournamentManager.Sweepers.IRepository> _mockSweeperRepository;
    private Mock<IValidator<TournamentManager.Models.Registration>> _mockRegistrationValidator;
    private Mock<TournamentManager.Bowlers.IEntityMapper> _mockBowlerEntityMapper;
    private Mock<TournamentManager.Registrations.IPaymentEntityMapper> _mockPaymentEntityMapper;

    private AppendRegistrationCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockRegistrationRepository = new Mock<TournamentManager.Registrations.IRepository>();
        _mockTournamentRepository = new Mock<TournamentManager.Tournaments.IRepository>();
        _mockDivisionRepository = new Mock<TournamentManager.Divisions.IRepository>();
        _mockSquadRepository = new Mock<TournamentManager.Squads.IRepository>();
        _mockSweeperRepository = new Mock<TournamentManager.Sweepers.IRepository>();
        _mockRegistrationValidator = new Mock<IValidator<TournamentManager.Models.Registration>>();
        _mockBowlerEntityMapper = new Mock<TournamentManager.Bowlers.IEntityMapper>();
        _mockPaymentEntityMapper = new Mock<TournamentManager.Registrations.IPaymentEntityMapper>();

        _handler = new AppendRegistrationCommandHandler(
            _mockRegistrationRepository.Object,
            _mockTournamentRepository.Object,
            _mockDivisionRepository.Object,
            _mockSquadRepository.Object,
            _mockSweeperRepository.Object,
            _mockRegistrationValidator.Object,
            _mockBowlerEntityMapper.Object,
            _mockPaymentEntityMapper.Object);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnUnexpected_WhenRegistrationCannotBeFound()
    {
        // Arrange
        Registration existingRegistration = null;
        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = new List<SquadId>(),
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Unexpected));
        Assert.That(result.FirstError.Code, Is.EqualTo("Registration.NotFound"));
        Assert.That(result.FirstError.Description, Is.EqualTo("The specified registration does not exist."));

        _mockRegistrationRepository.Verify(repo => repo.RetrieveAsync(command.Bowler.USBCId, command.TournamentId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(),
            It.IsAny<Bowler>(),
            It.IsAny<DivisionId>(),
            It.IsAny<int?>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<bool?>(),
            It.IsAny<Payment>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnNotFound_WhenDivisionIdIsNotValid()
    {
        // Arrange
        var existingRegistration = new Registration
        {
            Id = RegistrationId.New(),
            Bowler = new Bowler { USBCId = "12345" },
            DivisionId = DivisionId.New(), // Set the DivisionId property directly
            Division = new Division { Id = DivisionId.New() },
            Squads = [],
            Payments = []
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 1),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockDivisionRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Division)null);

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = DivisionId.New(),
            Squads = new List<SquadId>(),
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.NotFound));
        Assert.That(result.FirstError.Code, Is.EqualTo("Division.NotFound"));
        Assert.That(result.FirstError.Description, Is.EqualTo("The specified division does not exist."));

        _mockDivisionRepository.Verify(repo => repo.RetrieveAsync(command.DivisionId, It.IsAny<CancellationToken>()), Times.Once);
        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(),
            It.IsAny<Bowler>(),
            It.IsAny<DivisionId>(),
            It.IsAny<int?>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<bool?>(),
            It.IsAny<Payment>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldNotCallDivisionRepository_WhenDivisionIsSameAsExisting()
    {
        // Arrange
        var divisionId = DivisionId.New();
        var existingRegistration = new Registration
        {
            Id = RegistrationId.New(),
            Bowler = new Bowler { USBCId = "12345" },
            DivisionId = divisionId, // Set the DivisionId property directly
            Division = new Division { Id = divisionId },
            Squads = [],
            Payments = []
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 1),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockRegistrationValidator.Setup(validator => validator.ValidateAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = divisionId, // Same as existing registration
            Squads = new List<SquadId>(),
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        _mockDivisionRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnValidationError_WhenSquadsOverlapWithExistingRegistration()
    {
        // Arrange
        var existingSquadId1 = SquadId.New();
        var existingSquadId2 = SquadId.New();
        var overlappingSquadId = SquadId.New();
        var divisionId = DivisionId.New();

        var existingRegistration = new Registration
        {
            Id = RegistrationId.New(),
            Bowler = new Bowler { USBCId = "12345" },
            DivisionId = divisionId, // Use same division to avoid division lookup
            Division = new Division { Id = divisionId },
            Squads =
            [
                new() { SquadId = existingSquadId1 },
                new() { SquadId = existingSquadId2 },
                new() { SquadId = overlappingSquadId }
            ],
            Payments = []
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 1),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = divisionId, // Same division to skip division lookup
            Squads = new List<SquadId> { SquadId.New(), overlappingSquadId }, // One overlapping squad
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Squad.Conflict"));
        Assert.That(result.FirstError.Description, Is.EqualTo("The specified squads are already registered."));

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(),
            It.IsAny<Bowler>(),
            It.IsAny<DivisionId>(),
            It.IsAny<int?>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<bool?>(),
            It.IsAny<Payment>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnValidationError_WhenSweepersOverlapWithExistingRegistration()
    {
        // Arrange
        var existingSweeperSquadId1 = SquadId.New();
        var existingSweeperSquadId2 = SquadId.New();
        var overlappingSweeperSquadId = SquadId.New();
        var divisionId = DivisionId.New();

        var existingRegistration = new Registration
        {
            Id = RegistrationId.New(),
            Bowler = new Bowler { USBCId = "12345" },
            DivisionId = divisionId, // Use same division to avoid division lookup
            Division = new Division { Id = divisionId },
            Squads =
            [
                new() { SquadId = existingSweeperSquadId1 },
                new() { SquadId = existingSweeperSquadId2 },
                new() { SquadId = overlappingSweeperSquadId }
            ],
            Payments = []
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 1),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345" },
            TournamentId = TournamentId.New(),
            DivisionId = divisionId, // Same division to skip division lookup
            Squads = new List<SquadId>(), // Empty squads to focus on sweepers
            Sweepers = new List<SquadId> { SquadId.New(), overlappingSweeperSquadId }, // One overlapping sweeper
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.FirstError.Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(result.FirstError.Code, Is.EqualTo("Sweeper.Conflict"));
        Assert.That(result.FirstError.Description, Is.EqualTo("The specified sweepers are already registered."));

        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(),
            It.IsAny<Bowler>(),
            It.IsAny<DivisionId>(),
            It.IsAny<int?>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<bool?>(),
            It.IsAny<Payment>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldReturnValidationErrors_WhenValidationFails()
    {
        // Arrange
        var divisionId = DivisionId.New();
        var existingRegistration = new Registration
        {
            Id = RegistrationId.New(),
            Bowler = new Bowler { USBCId = "12345", FirstName = "John", LastName = "Doe" },
            DivisionId = divisionId,
            Division = new Division { Id = divisionId },
            Average = 150,
            SuperSweeper = false,
            Squads = [],
            Payments = []
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 1),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        // Setup validation to fail with multiple errors
        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Bowler.Name", "Bowler name is required") { ErrorCode = "Bowler.Name" });
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Average", "Average must be between 0 and 300") { ErrorCode = "Average" });
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Squads", "At least one squad is required") { ErrorCode = "Squads" });

        _mockRegistrationValidator.Setup(validator => validator.ValidateAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345", Name = new TournamentManager.Models.PersonName { First = "", Last = "" } },
            TournamentId = TournamentId.New(),
            DivisionId = divisionId, // Same division to skip division lookup
            Squads = new List<SquadId>(), // Empty squads to skip squad processing
            Sweepers = new List<SquadId>(), // Empty sweepers to skip sweeper processing
            Average = 350,
            SuperSweeper = false
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.True);
        Assert.That(result.Errors.Count, Is.EqualTo(3));
        
        var errors = result.Errors.ToList();
        Assert.That(errors[0].Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(errors[0].Code, Is.EqualTo("Bowler.Name"));
        Assert.That(errors[0].Description, Is.EqualTo("Bowler name is required"));
        
        Assert.That(errors[1].Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(errors[1].Code, Is.EqualTo("Average"));
        Assert.That(errors[1].Description, Is.EqualTo("Average must be between 0 and 300"));
        
        Assert.That(errors[2].Type, Is.EqualTo(ErrorType.Validation));
        Assert.That(errors[2].Code, Is.EqualTo("Squads"));
        Assert.That(errors[2].Description, Is.EqualTo("At least one squad is required"));

        // Verify that division, squad, and sweeper repositories are never called when validation fails
        _mockDivisionRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockSquadRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockSweeperRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Never);
        
        // Verify that update is never called
        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Registration>(),
            It.IsAny<Bowler>(),
            It.IsAny<DivisionId>(),
            It.IsAny<int?>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<bool?>(),
            It.IsAny<Payment>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateModelWithAllChanges_WhenValidatorIsCalled()
    {
        // Arrange
        var existingDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();
        var newSquadId1 = SquadId.New();
        var newSquadId2 = SquadId.New();
        var newSweeperSquadId1 = SquadId.New();
        var newSweeperSquadId2 = SquadId.New();

        var existingRegistration = new Registration
        {
            Id = RegistrationId.New(),
            Bowler = new Bowler { USBCId = "12345", FirstName = "Old", LastName = "Name" },
            DivisionId = existingDivisionId,
            Division = new Division { Id = existingDivisionId, Name = "Old Division" },
            Average = 150,
            SuperSweeper = false,
            Squads = [],
            Payments = []
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 15),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        var newDivision = new Division { Id = newDivisionId, Name = "New Division" };
        var newSquad1 = new TournamentSquad { Id = newSquadId1, Tournament = new Tournament() };
        var newSquad2 = new TournamentSquad { Id = newSquadId2, Tournament = new Tournament() };
        var newSweeper1 = new SweeperSquad { Id = newSweeperSquadId1, CashRatio = 1.0m, Tournament = new Tournament(), Divisions = [] };
        var newSweeper2 = new SweeperSquad { Id = newSweeperSquadId2, CashRatio = 1.0m, Tournament = new Tournament(), Divisions = [] };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockDivisionRepository.Setup(repo => repo.RetrieveAsync(newDivisionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(newDivision);

        _mockSquadRepository.Setup(repo => repo.RetrieveAsync(It.Is<IEnumerable<SquadId>>(ids => ids.Contains(newSquadId1) && ids.Contains(newSquadId2)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<TournamentSquad> { newSquad1, newSquad2 });

        _mockSweeperRepository.Setup(repo => repo.RetrieveAsync(It.Is<IEnumerable<SquadId>>(ids => ids.Contains(newSweeperSquadId1) && ids.Contains(newSweeperSquadId2)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SweeperSquad> { newSweeper1, newSweeper2 });

        _mockRegistrationValidator.Setup(validator => validator.ValidateAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345", Name = new TournamentManager.Models.PersonName { First = "New", Last = "Bowler" } },
            TournamentId = TournamentId.New(),
            DivisionId = newDivisionId, // Different division
            Squads = new List<SquadId> { newSquadId1, newSquadId2 },
            Sweepers = new List<SquadId> { newSweeperSquadId1, newSweeperSquadId2 },
            Average = 180,
            SuperSweeper = true
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert - Verify the validator was called with the updated model
        _mockRegistrationValidator.Verify(validator => validator.ValidateAsync(
            It.Is<TournamentManager.Models.Registration>(reg =>
                reg.Bowler.Name.First == "New" &&
                reg.Bowler.Name.Last == "Bowler" &&
                reg.Division.Id == newDivisionId &&
                reg.Average == 180 &&
                reg.SuperSweeper == true &&
                reg.Squads.Count() == 2 &&
                reg.Sweepers.Count() == 2 &&
                reg.TournamentStartDate == new DateOnly(2024, 1, 15) &&
                reg.TournamentSweeperCount == 3),
            It.IsAny<CancellationToken>()), Times.Once);

        _mockTournamentRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockDivisionRepository.Verify(repo => repo.RetrieveAsync(newDivisionId, It.IsAny<CancellationToken>()), Times.Once);
        _mockSquadRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockSweeperRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldAddPaymentToEntity_WhenPaymentIsProvided()
    {
        // Arrange
        var divisionId = DivisionId.New();
        var existingRegistration = new Registration
        {
            Id = RegistrationId.New(),
            Bowler = new Bowler { USBCId = "12345", FirstName = "John", LastName = "Doe" },
            DivisionId = divisionId,
            Division = new Division { Id = divisionId },
            Average = 150,
            SuperSweeper = false,
            Squads = [],
            Payments = []
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 1),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockRegistrationValidator.Setup(validator => validator.ValidateAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mockBowlerEntityMapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Bowler>()))
            .Returns(new Bowler());

        var paymentEntity = new Payment { Amount = 100.50m, ConfirmationCode = "TEST123" };
        _mockPaymentEntityMapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Payment>()))
            .Returns(paymentEntity);

        var payment = new TournamentManager.Models.Payment
        {
            Amount = 100.50m,
            ConfirmationCode = "TEST123"
        };

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345", Name = new TournamentManager.Models.PersonName { First = "John", Last = "Doe" } },
            TournamentId = TournamentId.New(),
            DivisionId = divisionId, // Same division to skip division lookup
            Squads = new List<SquadId>(), // Empty squads to skip squad processing
            Sweepers = new List<SquadId>(), // Empty sweepers to skip sweeper processing
            Payment = payment
        };

        var timestampBefore = DateTime.UtcNow;

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        var timestampAfter = DateTime.UtcNow;

        // Assert
        Assert.That(result.IsError, Is.False);

        // Verify that CreatedAtUtc timestamp was set on the payment
        Assert.That(payment.CreatedAtUtc, Is.GreaterThanOrEqualTo(timestampBefore));
        Assert.That(payment.CreatedAtUtc, Is.LessThanOrEqualTo(timestampAfter));

        // Verify that the payment entity mapper was called with the payment
        _mockPaymentEntityMapper.Verify(mapper => mapper.Execute(payment), Times.Once);

        // Verify that UpdateAsync was called with the payment entity
        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<Registration>(),
            It.IsAny<Bowler>(),
            It.IsAny<DivisionId>(),
            It.IsAny<int?>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<IEnumerable<SquadId>>(),
            It.IsAny<bool?>(),
            paymentEntity, // Verify the specific payment entity is passed
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task HandleAsync_ShouldUpdateEverythingCorrectly_WhenAllChangesProvided()
    {
        // Arrange
        var existingRegistrationId = RegistrationId.New();
        var existingDivisionId = DivisionId.New();
        var newDivisionId = DivisionId.New();
        var squadId1 = SquadId.New();
        var squadId2 = SquadId.New();
        var sweeperSquadId1 = SquadId.New();
        var sweeperSquadId2 = SquadId.New();

        var existingSquadId1 = SquadId.New();
        var existingSquadId2 = SquadId.New();
        var existingSquadReg1 = new SquadRegistration 
        { 
            RegistrationId = existingRegistrationId, 
            SquadId = existingSquadId1,
            Squad = new TournamentSquad { Id = existingSquadId1 }
        };
        var existingSquadReg2 = new SquadRegistration 
        { 
            RegistrationId = existingRegistrationId, 
            SquadId = existingSquadId2,
            Squad = new SweeperSquad { Id = existingSquadId2, CashRatio = 1.0m }
        };
        var existingPayment1 = new Payment { Id = PaymentId.New(), Amount = 50.00m, ConfirmationCode = "PAY123" };
        var existingPayment2 = new Payment { Id = PaymentId.New(), Amount = 25.00m, ConfirmationCode = "PAY789" };

        var existingRegistration = new Registration
        {
            Id = existingRegistrationId,
            Bowler = new Bowler { USBCId = "12345", FirstName = "Old", LastName = "Name" },
            DivisionId = existingDivisionId,
            Division = new Division { Id = existingDivisionId },
            Average = 150,
            SuperSweeper = false,
            Squads = [existingSquadReg1, existingSquadReg2],
            Payments = [existingPayment1, existingPayment2]
        };

        var tournament = new TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Start = new DateOnly(2024, 1, 1),
            Sweepers =
            [
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] },
                new() { Id = SquadId.New(), CashRatio = 1.0m, Divisions = [] }
            ]
        };

        var newDivision = new Division { Id = newDivisionId, Name = "New Division" };
        var squad1 = new TournamentSquad { Id = squadId1, Tournament = new Tournament() };
        var squad2 = new TournamentSquad { Id = squadId2, Tournament = new Tournament() };
        var sweeper1 = new SweeperSquad { Id = sweeperSquadId1, CashRatio = 1.0m, Tournament = new Tournament(), Divisions = [] };
        var sweeper2 = new SweeperSquad { Id = sweeperSquadId2, CashRatio = 1.0m, Tournament = new Tournament(), Divisions = [] };

        _mockRegistrationRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<string>(), It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRegistration);

        _mockTournamentRepository.Setup(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournament);

        _mockDivisionRepository.Setup(repo => repo.RetrieveAsync(newDivisionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(newDivision);

        _mockSquadRepository.Setup(repo => repo.RetrieveAsync(It.Is<IEnumerable<SquadId>>(ids => ids.Contains(squadId1) && ids.Contains(squadId2)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<TournamentSquad> { squad1, squad2 });

        _mockSweeperRepository.Setup(repo => repo.RetrieveAsync(It.Is<IEnumerable<SquadId>>(ids => ids.Contains(sweeperSquadId1) && ids.Contains(sweeperSquadId2)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SweeperSquad> { sweeper1, sweeper2 });

        _mockRegistrationValidator.Setup(validator => validator.ValidateAsync(It.IsAny<TournamentManager.Models.Registration>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var bowlerEntity = new Bowler { Id = BowlerId.New(), USBCId = "12345", FirstName = "New", LastName = "Bowler" };
        _mockBowlerEntityMapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Bowler>()))
            .Returns(bowlerEntity);

        var paymentEntity = new Payment { Amount = 75.00m, ConfirmationCode = "PAY456" };
        _mockPaymentEntityMapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Payment>()))
            .Returns(paymentEntity);

        var payment = new TournamentManager.Models.Payment
        {
            Amount = 75.00m,
            ConfirmationCode = "PAY456"
        };

        var command = new AppendRegistrationCommand
        {
            Bowler = new TournamentManager.Models.Bowler { USBCId = "12345", Name = new TournamentManager.Models.PersonName { First = "New", Last = "Bowler" } },
            TournamentId = TournamentId.New(),
            DivisionId = newDivisionId,
            Squads = new List<SquadId> { squadId1, squadId2 },
            Sweepers = new List<SquadId> { sweeperSquadId1, sweeperSquadId2 },
            Average = 185,
            SuperSweeper = true,
            Payment = payment
        };

        // Act
        var result = await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsError, Is.False);
        Assert.That(result.Value, Is.EqualTo(existingRegistrationId));

        // Verify the existing registration entity is not modified - squads and payments remain unchanged
        Assert.That(existingRegistration.Squads.Count, Is.EqualTo(2));
        Assert.That(existingRegistration.Squads.First().SquadId, Is.EqualTo(existingSquadId1));
        Assert.That(existingRegistration.Squads.Last().SquadId, Is.EqualTo(existingSquadId2));
        Assert.That(existingRegistration.Payments.Count, Is.EqualTo(2));
        Assert.That(existingRegistration.Payments.First().ConfirmationCode, Is.EqualTo("PAY123"));
        Assert.That(existingRegistration.Payments.Last().ConfirmationCode, Is.EqualTo("PAY789"));

        // Verify bowler entity mapper was called with the correct bowler
        _mockBowlerEntityMapper.Verify(mapper => mapper.Execute(
            It.Is<TournamentManager.Models.Bowler>(b => 
                b.USBCId == "12345" && 
                b.Name.First == "New" && 
                b.Name.Last == "Bowler")), Times.Once);

        // Verify payment timestamp was set
        Assert.That(payment.CreatedAtUtc, Is.Not.EqualTo(default(DateTime)));

        // Verify payment entity mapper was called
        _mockPaymentEntityMapper.Verify(mapper => mapper.Execute(payment), Times.Once);

        // Verify registration repository UpdateAsync was called with all correct parameters
        _mockRegistrationRepository.Verify(repo => repo.UpdateAsync(
            existingRegistration,           // The existing registration entity
            bowlerEntity,                   // The mapped bowler entity  
            newDivisionId,                  // The new division ID
            185,                           // The new average
            It.Is<IEnumerable<SquadId>>(squads => squads.Contains(squadId1) && squads.Contains(squadId2)), // Squad IDs
            It.Is<IEnumerable<SquadId>>(sweepers => sweepers.Contains(sweeperSquadId1) && sweepers.Contains(sweeperSquadId2)), // Sweeper IDs
            true,                          // Super sweeper flag
            paymentEntity,                 // The mapped payment entity
            It.IsAny<CancellationToken>()), Times.Once);

        // Verify all repositories were called
        _mockTournamentRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockDivisionRepository.Verify(repo => repo.RetrieveAsync(newDivisionId, It.IsAny<CancellationToken>()), Times.Once);
        _mockSquadRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockSweeperRepository.Verify(repo => repo.RetrieveAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}