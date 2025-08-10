using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BowlingMegabucks.TournamentManager.Database.Entities;

internal sealed class Payment
{
    [Key]
    public PaymentId Id { get; set; }

    [Required]
    public RegistrationId RegistrationId { get; set; }

    public Registration Registration { get; set; } = null!;

    [Required]
    public string ConfirmationCode { get; set; } = null!;

    [Required]
    [Precision(5, 2)]
    public decimal Amount { get; set; }

    internal sealed class Configuration
        : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(payment => payment.Id)
                .HasConversion<PaymentId.EfCoreValueConverter>()
                .HasValueGenerator<PaymentIdValueGenerator>();

            builder.Property(payment => payment.RegistrationId)
                .HasConversion<RegistrationId.EfCoreValueConverter>();

            builder.HasOne(payment => payment.Registration)
                .WithMany(registration => registration.Payments)
                .HasForeignKey(payment => payment.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}