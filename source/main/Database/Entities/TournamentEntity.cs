using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NewEnglandClassic.Database.Entities;

internal class Tournament
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateOnly Start { get; set; }

    [Required]
    public DateOnly End { get; set; }

    [Required]
    [Precision(5,2)]
    public decimal EntryFee { get; set; }
    
    [Required]
    public short Games { get; set; }

    [Required]
    [Precision(3,1)]
    public decimal FinalsRatio { get; set; }

    [Required]
    [Precision(3,1)]
    public decimal CashRatio { get; set; }

    [Required]
    public string BowlingCenter { get; set; } = string.Empty;

    [Required]
    public bool Completed { get; set; }

    public ICollection<Division> Divisions { get; set; } = null!;

    public ICollection<TournamentSquad> Squads { get; set; } = null!;

    public ICollection<SweeperSquad> Sweepers { get; set; } = null!;
}
