
namespace NewEnglandClassic.Sweepers;
internal class ViewModel
{
    public Guid Id { get; set; }

    public Guid TournamentId { get; set; }

    public decimal EntryFee { get; set; }

    public short Games { get; set; }

    public decimal CashRatio { get; set; }

    public DateTime Date { get; set; }

    public short MaxPerPair { get; set; }

    public bool Complete { get; set; }

    public IDictionary<Guid, int?> Divisions { get; set; } = new Dictionary<Guid, int?>();
}

internal interface IViewModel
{
    Guid Id { get; set; }

    Guid TournamentId { get; set; }

    decimal EntryFee { get; set; }

    short Games { get; set; }

    decimal CashRatio { get; set; }

    DateTime Date { get; set; }

    short MaxPerPair { get; set; }

    bool Complete { get; set; }

    IDictionary<Guid, int?> Divisions { get; }
}