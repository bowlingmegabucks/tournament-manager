using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NortheastMegabuck.Database;
internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), dateTime => DateOnly.FromDateTime(dateTime))
    {

    }
}
