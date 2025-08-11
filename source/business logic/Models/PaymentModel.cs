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
    public DateTime CreatedAtUtc { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RegistrationId RegistrationId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal Amount { get; set; }

    internal Payment(Database.Entities.Payment entity)
    {
        Id = entity.Id;
        CreatedAtUtc = entity.CreatedAtUtc;
        RegistrationId = entity.RegistrationId;
        Amount = entity.Amount;
    }
}