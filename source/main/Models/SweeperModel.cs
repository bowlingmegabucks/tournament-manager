
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

    public IDictionary<Guid, int?> Divisions { get; set; } = new Dictionary<Guid, int?>();
}
