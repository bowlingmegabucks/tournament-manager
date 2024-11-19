
namespace NortheastMegabuck.Bowlers.Search;
internal class ViewModel(Models.Bowler bowler) : IViewModel
{
    public BowlerId Id { get; set; } = bowler.Id;

    public string FirstName { get; set; } = bowler.Name.First;

    public string LastName { get; set; } = bowler.Name.Last;

    public string EmailAddress { get; set; } = bowler.EmailAddress;

    public string City { get; set; } = bowler.CityAddress;

    public string State { get; set; } = bowler.StateAddress;
}

internal interface IViewModel
{
    BowlerId Id { get; }

    string FirstName { get; }

    string LastName { get; }

    string EmailAddress { get; }

    string City { get; }

    string State { get; }
}