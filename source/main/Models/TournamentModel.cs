namespace NewEnglandClassic.Models;

internal class Tournament
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateOnly Start { get; set; }

    public DateOnly End { get; set; }

    public decimal EntryFee { get; set; }

    public short Games { get; set; }

    public decimal FinalsRatio { get; set; }

    public decimal CashRatio { get; set; }

    public string BowlingCenter { get; set; } = string.Empty;

    public bool Completed { get; set; }

    public IEnumerable<Squad> Squads { get; }

    internal Tournament(Database.Entities.Tournament entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Start = entity.Start;
        End = entity.End;
        EntryFee = entity.EntryFee;
        Games = entity.Games;
        FinalsRatio = entity.FinalsRatio;
        CashRatio = entity.CashRatio;
        BowlingCenter = entity.BowlingCenter;
        Completed = entity.Completed;
        
        Squads = entity.Squads?.Select(squad => new Squad(squad)).ToList() ?? Enumerable.Empty<Squad>();
    }

    internal Tournament(Tournaments.IViewModel viewModel)
    {
        Id = viewModel.Id;
        Name = viewModel.TournamentName;
        Start = viewModel.Start;
        End = viewModel.End;
        EntryFee = viewModel.EntryFee;
        Games = viewModel.Games;
        FinalsRatio = viewModel.FinalsRatio;
        CashRatio = viewModel.CashRatio;
        BowlingCenter = viewModel.BowlingCenter;
        Completed = viewModel.Completed;

        Squads = Enumerable.Empty<Squad>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Tournament()
    {
        Squads = Enumerable.Empty<Squad>();
    }
}
