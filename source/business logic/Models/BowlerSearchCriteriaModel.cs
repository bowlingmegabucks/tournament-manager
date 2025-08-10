
namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public class BowlerSearchCriteria
{
    /// <summary>
    /// 
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? EmailAddress { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<SquadId> WithoutRegistrationOnSquads { get; set; } = [];

    /// <summary>
    /// 
    /// </summary>
    public TournamentId? RegisteredInTournament { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TournamentId? NotRegisteredInTournament { get; set; }

    internal BowlerId? BowlerId { get; set; }

    internal string? UsbcId { get; set; }
}
