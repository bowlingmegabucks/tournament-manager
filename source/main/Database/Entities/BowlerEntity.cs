using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NortheastMegabuck.Database.Entities;
internal class Bowler : IEquatable<Bowler>
{
    [Key]
    public BowlerId Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(1)]
    public string MiddleInitial { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string Suffix { get; set; } = string.Empty;

    public string StreetAddress { get; set; } = string.Empty;

    public string CityAddress { get; set; } = string.Empty;

    [MaxLength(2)]
    public string StateAddress { get; set; } = string.Empty;

    [MaxLength(9)]
    public string ZipCode { get; set; } = string.Empty;

    [Required]
    public string EmailAddress { get; set; } = string.Empty;

    [MaxLength(10)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string USBCId { get; set; } = string.Empty;

    public DateOnly? DateOfBirth { get; set; }

    public Models.Gender? Gender { get; set; }

    public string SocialSecurityNumber { get; set; } = string.Empty;

    public ICollection<Registration> Registrations { get; set; } = null!;

    public ICollection<SquadScore> SquadScores { get; set; } = null!;

    public bool Equals(Bowler? other)
        => other is not null && (ReferenceEquals(this, other) || Id.Equals(other.Id));

    public override bool Equals(object? obj)
        => Equals(obj as Bowler);

    public override int GetHashCode()
        => Id.GetHashCode();

    internal class Configuration : IEntityTypeConfiguration<Bowler>
    {
        public void Configure(EntityTypeBuilder<Bowler> builder)
        {
            builder.Property(bowler => bowler.Id)
                .HasConversion<BowlerId.EfCoreValueConverter>()
                .HasValueGenerator<BowlerIdValueGenerator>();

            builder.Property(bowler => bowler.DateOfBirth).HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(bowler => bowler.MiddleInitial).IsFixedLength();

            builder.Property(bowler => bowler.StateAddress).IsFixedLength();
            builder.Property(bowler => bowler.ZipCode).IsFixedLength();

            builder.Property(bowler => bowler.PhoneNumber).IsFixedLength();
        }
    }
}
