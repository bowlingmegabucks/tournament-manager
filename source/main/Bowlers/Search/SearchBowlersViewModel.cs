
namespace NewEnglandClassic.Bowlers.Search;
internal class ViewModel : IViewModel
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public ViewModel(Models.Bowler bowler)
    {
        Id = bowler.Id;
        FirstName = bowler.FirstName;
        LastName = bowler.LastName;
        EmailAddress = bowler.EmailAddress;
        City = bowler.CityAddress;
        State = bowler.StateAddress;  
    }
}

internal interface IViewModel
{
    Guid Id { get; }
    
    string FirstName { get; }

    string LastName { get; }

    string EmailAddress { get; }

    string City { get; }

    string State { get; }
}