namespace BowlingMegabucks.TournamentManager.Registrations;

internal sealed class PaymentEntityMapper
    : IPaymentEntityMapper
{
    public Database.Entities.Payment Execute(Models.Payment payment)
        => new()
        {
            Id = payment.Id,
            ConfirmationCode = payment.ConfirmationCode,
            CreatedAtUtc = payment.CreatedAtUtc,
            RegistrationId = payment.RegistrationId,
            Amount = payment.Amount
        };
}

internal interface IPaymentEntityMapper
{
    Database.Entities.Payment Execute(Models.Payment payment);
}