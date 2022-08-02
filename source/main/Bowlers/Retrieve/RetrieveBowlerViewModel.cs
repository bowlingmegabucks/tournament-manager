namespace NewEnglandClassic.Bowlers.Retrieve;
internal class ViewModel : IViewModel
{
    public Guid Id { get; set; }

    public ViewModel(Models.Bowler bowler)
    {
        Id = bowler.Id.Value;
    }
}

internal interface IViewModel
{
    Guid Id { get; }
}