namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// PaymentInput is a record that represents the input data required for payment confirmation
/// </summary>
public sealed record PaymentInput
{
    /// <summary>
    /// The unique identifier for the payment processing system used in the registration.
    /// </summary>
    /// <example>Pinwheel</example>
    /// <example>PayPal</example>
    public required string ProcessingSystem { get; init; }

    /// <summary>
    /// The unique identifier for the payment method used in the registration.
    /// This should be a unique confirmation code provided by the payment processing system.
    /// <example>CONF12345</example>
    /// </summary>
    public required string ConfirmationCode { get; init; }

    /// <summary>
    /// The amount of the payment made for the registration.
    /// This should be a positive decimal value representing the payment amount.
    /// </summary>
    public required decimal Amount { get; init; }
}

internal static class PaymentInputExtensions
{ 
    public static Models.Payment ToModel(this PaymentInput input)
    {
        return new Models.Payment
        {
            ConfirmationCode = $"{input.ProcessingSystem}_{input.ConfirmationCode}",
            Amount = input.Amount
        };
    }
}