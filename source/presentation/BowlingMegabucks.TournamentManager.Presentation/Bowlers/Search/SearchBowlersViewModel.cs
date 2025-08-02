namespace BowlingMegabucks.TournamentManager.Bowlers.Search;
internal class ViewModel(Models.Bowler bowler) 
    : IViewModel
{
    public BowlerId Id { get; set; } = bowler.Id;

    public string FirstName { get; set; } = bowler.Name.First;

    public string LastName { get; set; } = bowler.Name.Last;

    public string EmailAddress { get; set; } = bowler.EmailAddress;

    public string City { get; set; } = bowler.CityAddress;

    public string State { get; set; } = bowler.StateAddress;
}

/// <summary>
/// Represents a lightweight view model for displaying bowler search results.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets the unique identifier for the bowler.
    /// </summary>
    BowlerId Id { get; }

    /// <summary>
    /// Gets the first name of the bowler.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Gets the last name of the bowler.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Gets the email address of the bowler.
    /// </summary>
    string EmailAddress { get; }

    /// <summary>
    /// Gets the city of the bowler's address.
    /// </summary>
    string City { get; }

    /// <summary>
    /// Gets the state of the bowler's address.
    /// </summary>
    string State { get; }
}