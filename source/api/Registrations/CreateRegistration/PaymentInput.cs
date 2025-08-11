namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// PaymentInput is a record that represents the input data required for payment confirmation
/// </summary>
public sealed record PaymentInput
{
    /// <summary>
    /// The unique identifier for the payment method used in the registration.
    /// </summary>
    public required string ConfirmationCode { get; init; }

    /// <summary>
    /// The method of payment used for the registration, such as credit card, PayPal, etc.
    /// </summary>
    public required decimal Amount { get; init; }
}

internal static class PaymentInputExtensions
{ 
    public static Models.Payment ToModel(this PaymentInput input)
    {
        return new Models.Payment
        {
            ConfirmationCode = input.ConfirmationCode,
            Amount = input.Amount
        };
    }
}