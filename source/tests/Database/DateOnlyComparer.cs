namespace BowlingMegabucks.TournamentManager.Tests.Database;

[TestFixture]
internal sealed class DateOnlyComparer
{
    [Test]
    public void Comparer_SameValues_ReturnsTrue()
    {
        var comparer = new BowlingMegabucks.TournamentManager.Database.DateOnlyComparer();

        var equals = comparer.EqualsExpression.Compile();

        var dateOnly1 = new DateOnly(2000, 1, 1);
        var dateOnly2 = new DateOnly(2000, 1, 1);

        Assert.That(equals(dateOnly1, dateOnly2), Is.True);
    }

    [Test]
    public void Comparer_DifferentValues_ReturnsFalse()
    {
        var comparer = new BowlingMegabucks.TournamentManager.Database.DateOnlyComparer();

        var equals = comparer.EqualsExpression.Compile();

        var dateOnly1 = new DateOnly(2000, 1, 1);
        var dateOnly2 = new DateOnly(2000, 1, 2);

        Assert.That(equals(dateOnly1, dateOnly2), Is.False);
    }

    [Test]
    public void Comparer_GetHashCode_ReturnsDateOnlyHashCode()
    {
        var comparer = new BowlingMegabucks.TournamentManager.Database.DateOnlyComparer();

        var hashCode = comparer.HashCodeExpression.Compile();

        var dateOnly = new DateOnly(2000, 1, 1);

        Assert.That(hashCode(dateOnly), Is.EqualTo(dateOnly.GetHashCode()));
    }
}
