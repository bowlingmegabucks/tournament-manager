using Bogus;
using BowlingMegabucks.TournamentManager.Database.Entities;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

internal static class PaymentEntityFactory
{
    public static Payment Bogus()
        => new PaymentEntityFaker().Generate();

    public static IEnumerable<Payment> Bogus(int count, RegistrationId registrationId)
        => new PaymentEntityFaker(registrationId).Generate(count);
}

internal sealed class PaymentEntityFaker
    : Faker<Payment>
{
    public PaymentEntityFaker()
        : this(RegistrationId.New())
    { }
    
    public PaymentEntityFaker(RegistrationId registrationId)
    {
        RuleFor(payment => payment.Id, _ => PaymentId.New());
        RuleFor(payment => payment.RegistrationId, _ => registrationId);
        RuleFor(payment => payment.ConfirmationCode, f => f.Random.Guid().ToString());
        RuleFor(payment => payment.Amount, f => f.Finance.Amount(10, 1000));
        RuleFor(payment => payment.CreatedAtUtc, f => f.Date.Past());
    }
}