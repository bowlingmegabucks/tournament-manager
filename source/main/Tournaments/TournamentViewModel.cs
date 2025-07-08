
namespace NortheastMegabuck.Tournaments;
internal class ViewModel : IViewModel
{
    public TournamentId Id { get; set; }

    public string TournamentName { get; set; } = string.Empty;

    public DateOnly Start { get; set; }

    public DateOnly End { get; set; }

    public decimal EntryFee { get; set; }

    public short Games { get; set; }

    public decimal FinalsRatio { get; set; }

    public decimal CashRatio { get; set; }

    public decimal SuperSweeperCashRatio { get; set; }

    public string BowlingCenter { get; set; } = string.Empty;

    public bool Completed { get; set; }

    public ViewModel(Models.Tournament model)
    {
        Id = model.Id;
        TournamentName = model.Name;
        Start = model.Start;
        End = model.End;
        EntryFee = model.EntryFee;
        Games = model.Games;
        FinalsRatio = model.FinalsRatio;
        CashRatio = model.CashRatio;
        SuperSweeperCashRatio = model.SuperSweeperCashRatio;
        BowlingCenter = model.BowlingCenter;
        Completed = model.Completed;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {

    }
}

internal interface IViewModel
{
    TournamentId Id { get; set; }

    string TournamentName { get; set; }

    DateOnly Start { get; set; }

    DateOnly End { get; set; }

    decimal EntryFee { get; set; }

    short Games { get; set; }

    decimal FinalsRatio { get; set; }

    decimal CashRatio { get; set; }

    decimal SuperSweeperCashRatio { get; set; }

    string BowlingCenter { get; set; }

    bool Completed { get; set; }
}

internal static class  ViewModelExtensions
{
    public static Models.Tournament ToModel(this IViewModel viewModel)
        => new()
        {
            Id = viewModel.Id,
            Name = viewModel.TournamentName,
            Start = viewModel.Start,
            End = viewModel.End,
            EntryFee = viewModel.EntryFee,
            Games = viewModel.Games,
            FinalsRatio = viewModel.FinalsRatio,
            CashRatio = viewModel.CashRatio,
            SuperSweeperCashRatio = viewModel.SuperSweeperCashRatio,
            BowlingCenter = viewModel.BowlingCenter,
            Completed = viewModel.Completed,
        };
}