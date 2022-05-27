namespace NewEnglandClassic.Squads;

internal class ViewModel : IViewModel
{
    public Guid Id { get; set; }

    public Guid TournamentId { get; set; }

    public decimal? CashRatio { get; set; }

    public decimal? FinalsRatio { get; set; }

    public DateTime Date { get; set; }

    public int MaxPerPair { get; set; }

    public bool Complete { get; set; }
}

internal interface IViewModel
{
    Guid Id { get; set; }

    Guid TournamentId { get; set; }

    decimal? CashRatio { get; set; }

    decimal? FinalsRatio { get; set; }

    DateTime Date { get; set; }

    int MaxPerPair { get; set; }

    bool Complete { get; set; }
}
