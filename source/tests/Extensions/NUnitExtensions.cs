namespace NewEnglandClassic.Tests.Extensions;
internal static class NUnit
{
    internal static void HasErrorMessage(this IEnumerable<NewEnglandClassic.Models.ErrorDetail> errors, string errorMessage)
        => Assert.That(errors.Count(error => error.Message == errorMessage), Is.EqualTo(1));
}
