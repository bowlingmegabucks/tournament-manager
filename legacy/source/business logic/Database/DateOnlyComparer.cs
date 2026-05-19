using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BowlingMegabucks.TournamentManager.Database;

internal class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base((x, y) => x.DayNumber == y.DayNumber, d => d.GetHashCode())
    {

    }
}
