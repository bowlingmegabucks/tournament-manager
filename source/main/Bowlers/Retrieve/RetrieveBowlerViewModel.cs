namespace NortheastMegabuck.Bowlers.Retrieve;
internal class ViewModel : IViewModel
{
    public BowlerId Id { get; set; }

    public ViewModel(Models.Bowler bowler)
    {
        Id = bowler.Id;
    }
}

internal interface IViewModel
{
    BowlerId Id { get; }
}