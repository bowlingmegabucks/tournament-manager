using FluentValidation;

namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.UpdateRegistration;

[TestFixture]
public sealed class UpdateRegistrationCommandHandlerTests
{
    private Mock<TournamentManager.Registrations.IRepository> _mockRegistrationRepository;
    private Mock<TournamentManager.Scores.IRepository> _mockScoresRepository;
    private IValidator<TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationRecord> _registrationValidator;
    private Mock<TournamentManager.Tournaments.IRepository> _mockTournamentRepository;
    private TournamentManager.Registrations.IPaymentEntityMapper _paymentEntityMapper;

    private TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockRegistrationRepository = new Mock<TournamentManager.Registrations.IRepository>();
        _mockScoresRepository = new Mock<TournamentManager.Scores.IRepository>();
        _registrationValidator = new InlineValidator<TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationRecord>();
        _mockTournamentRepository = new Mock<TournamentManager.Tournaments.IRepository>();
        _paymentEntityMapper = new TournamentManager.Registrations.PaymentEntityMapper();

        _handler = new TournamentManager.Registrations.UpdateRegistration.UpdateRegistrationCommandHandler(
            _mockRegistrationRepository.Object,
            _mockScoresRepository.Object,
            _mockTournamentRepository.Object,
            _paymentEntityMapper,
            _registrationValidator);
    }
}