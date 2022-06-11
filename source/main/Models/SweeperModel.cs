
namespace NewEnglandClassic.Models;
internal class Sweeper
{
    public Guid Id { get; set; }

    public Guid TournamentId { get; set; }

    internal Tournament? Tournament { get; set; }

    public decimal EntryFee { get; set; }

    public short Games { get; set; }

    public decimal CashRatio { get; set; }

    public DateTime Date { get; set; }

    public short MaxPerPair { get; set; }

    public bool Complete { get; set; }

    public IDictionary<Guid, int?> Divisions { get; set; }

    public Sweeper(Sweepers.IViewModel viewModel)
    {
        Id = viewModel.Id;
        TournamentId = viewModel.TournamentId;
        EntryFee = viewModel.EntryFee;
        Games = viewModel.Games;
        CashRatio = viewModel.CashRatio;
        Date = viewModel.Date;
        MaxPerPair = viewModel.MaxPerPair;
        Complete = viewModel.Complete;
        Divisions = viewModel.Divisions;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Sweeper()
    {
        Divisions = new Dictionary<Guid, int?>();
    }
}
