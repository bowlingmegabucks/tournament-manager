
namespace NortheastMegabuck.Bowlers.Search;
internal class ViewModel : IViewModel
{
    public BowlerId Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public ViewModel(Models.Bowler bowler)
    {
        Id = bowler.Id;
        FirstName = bowler.Name.First;
        LastName = bowler.Name.Last;
        EmailAddress = bowler.EmailAddress;
        City = bowler.CityAddress;
        State = bowler.StateAddress;  
    }
}

public interface IViewModel
{
    BowlerId Id { get; }
    
    string FirstName { get; }

    string LastName { get; }

    string EmailAddress { get; }

    string City { get; }

    string State { get; }
}