using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BowlingMegabucks.TournamentManager.Database;
internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), dateTime => DateOnly.FromDateTime(dateTime))
    {

    }
}
