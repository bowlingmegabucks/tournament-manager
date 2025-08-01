
namespace BowlingMegabucks.TournamentManager.Bowlers.Search;
internal class ViewModel(Models.Bowler bowler) : IViewModel
{
    public BowlerId Id { get; set; } = bowler.Id;

    public string FirstName { get; set; } = bowler.Name.First;

    public string LastName { get; set; } = bowler.Name.Last;

    public string EmailAddress { get; set; } = bowler.EmailAddress;

    public string City { get; set; } = bowler.CityAddress;

    public string State { get; set; } = bowler.StateAddress;
}

/// <summary>
/// 
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// 
    /// </summary>
    BowlerId Id { get; }

    /// <summary>
    /// 
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// 
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// 
    /// </summary>
    string EmailAddress { get; }

    /// <summary>
    /// 
    /// </summary>
    string City { get; }

    /// <summary>
    /// 
    /// </summary>
    string State { get; }
}