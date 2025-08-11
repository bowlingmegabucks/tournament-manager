using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusPaymentInput
    : Faker<PaymentInput>
{
    public BogusPaymentInput()
    {
        RuleFor(input => input.ConfirmationCode, f => f.Finance.BitcoinAddress());
        RuleFor(input => input.Amount, f => f.Finance.Amount(0, 1000, 2));
    }
}