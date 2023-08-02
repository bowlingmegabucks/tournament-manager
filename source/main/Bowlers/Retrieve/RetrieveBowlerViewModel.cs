namespace NortheastMegabuck.Bowlers.Retrieve;
internal class ViewModel : IViewModel
{
    public BowlerId Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleInitial { get; set; }

    public string LastName { get; set; }

    public string Suffix { get; set; }

    public ViewModel(Models.Bowler bowler)
    {
        Id = bowler.Id;

        FirstName = bowler.Name.First;
        MiddleInitial = bowler.Name.MiddleInitial;
        LastName = bowler.Name.Last;
        Suffix = bowler.Name.Suffix;
    }
}

internal interface IViewModel
{
    BowlerId Id { get; }

    string FirstName { get; set; }

    string MiddleInitial { get; set; }

    string LastName { get; set; }

    string Suffix { get; set; }
}