namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public sealed class Payment
{
    /// <summary>
    /// 
    /// </summary>
    public PaymentId Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ConfirmationCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RegistrationId RegistrationId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Default constructor for serialization purposes.
    /// </summary>
    public Payment()
    { 
        ConfirmationCode = string.Empty;
    }

    internal Payment(Database.Entities.Payment entity)
    {
        Id = entity.Id;
        ConfirmationCode = entity.ConfirmationCode;
        CreatedAtUtc = entity.CreatedAtUtc;
        RegistrationId = entity.RegistrationId;
        Amount = entity.Amount;
    }
}