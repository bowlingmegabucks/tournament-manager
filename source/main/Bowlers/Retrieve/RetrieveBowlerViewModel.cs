namespace NewEnglandClassic.Bowlers.Retrieve;
internal class ViewModel : IViewModel
{
    public Guid Id { get; set; }

    public ViewModel(Models.Bowler bowler)
    {
        Id = bowler.Id;
    }
}

internal interface IViewModel
{
    Guid Id { get; }
}