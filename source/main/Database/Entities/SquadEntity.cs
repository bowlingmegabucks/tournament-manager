using System.ComponentModel.DataAnnotations;

namespace NewEnglandClassic.Database.Entities;
internal abstract class Squad
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid TournamentId { get; set; }

    public Tournament Tournament { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int MaxPerPair { get; set; }

    [Required]
    public bool Complete { get; set; }
}
