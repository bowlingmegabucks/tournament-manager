using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusPaymentInput
    : Faker<PaymentInput>
{
    private static readonly string[] _processingSystems =
    [
        "Pinwheel",
        "PayPal",
        "Stripe",
        "Square"
    ];

    public BogusPaymentInput()
    {
        RuleFor(input => input.ProcessingSystem, f => f.PickRandom(_processingSystems));
        RuleFor(input => input.ConfirmationCode, f => f.Finance.BitcoinAddress());
        RuleFor(input => input.Amount, f => f.Finance.Amount(0, 1000, 2));
    }
}