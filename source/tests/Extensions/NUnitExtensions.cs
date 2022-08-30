namespace NortheastMegabuck.Tests.Extensions;
internal static class NUnit
{
    internal static void Assert_HasErrorMessage(this IEnumerable<NortheastMegabuck.Models.ErrorDetail> errors, string errorMessage)
        => Assert.That(errors.Count(error => error.Message == errorMessage), Is.EqualTo(1));
}
