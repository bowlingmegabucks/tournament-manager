namespace NortheastMegabuck.Tests.Database;

[TestFixture]
internal sealed class DateOnlyConverterTests
{
    [Test]
    public void DateOnlyToDateTime_MappedCorrectly()
    {
        var dateOnlyConverter = new NortheastMegabuck.Database.DateOnlyConverter();

        var dateOnly = new DateOnly(2000, 1, 2);

        var toConverter = dateOnlyConverter.ConvertToProviderExpression.Compile();

        var actual = toConverter(dateOnly);

        Assert.That(actual, Is.EqualTo(dateOnly.ToDateTime(TimeOnly.MinValue)));
    }

    [Test]
    public void DateTimeToDateOnly_MappedCorrectly()
    {
        var dateOnlyConverter = new NortheastMegabuck.Database.DateOnlyConverter();

        var dateTime = new DateTime(2000, 1, 2, 3, 4, 5, DateTimeKind.Unspecified);

        var fromConverter = dateOnlyConverter.ConvertFromProviderExpression.Compile();

        var actual = fromConverter(dateTime);

        Assert.That(actual, Is.EqualTo(DateOnly.FromDateTime(dateTime)));
    }
}
