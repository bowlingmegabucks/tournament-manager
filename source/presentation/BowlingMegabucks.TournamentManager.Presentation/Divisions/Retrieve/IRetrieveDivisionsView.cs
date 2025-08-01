namespace BowlingMegabucks.TournamentManager.Divisions.Retrieve;

/// <summary>
/// 
/// </summary>
public interface IView
{
    /// <summary>
    /// 
    /// </summary>
    TournamentId TournamentId { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    void DisplayError(string message);

    /// <summary>
    /// 
    /// </summary>
    void Disable();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="divisions"></param>
    void BindDivisions(IEnumerable<IViewModel> divisions);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tournamentId"></param>
    /// <returns></returns>
    DivisionId? AddDivision(TournamentId tournamentId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RefreshDivisionsAsync(CancellationToken cancellationToken);
}
