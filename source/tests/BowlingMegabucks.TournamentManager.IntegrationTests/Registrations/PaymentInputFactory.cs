using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

internal static class PaymentInputFactory
{ 
    public static PaymentInput Create(
        string? confirmationCode = null,
        decimal? amount = null
    )
    {
        return new PaymentInput
        {
            ProcessingSystem = "Test",
            ConfirmationCode = confirmationCode ?? Guid.CreateVersion7().ToString(),
            Amount = amount ?? 100.00m
        };
    }
}