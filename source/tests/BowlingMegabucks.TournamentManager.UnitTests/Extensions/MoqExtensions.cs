using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Tests.Extensions;
internal static class Moq
{
    internal static DbSet<TEntity> SetUpDbContext<TEntity>(this IEnumerable<TEntity> items) where TEntity : class
        => items.AsQueryable().BuildMockDbSet().Object;

    internal static void Validate_IsValid<T>(this Mock<IValidator<T>> mockValidator) where T : class
    {
        var result = new FluentValidation.Results.ValidationResult();

        mockValidator.Setup(validator => validator.Validate(It.IsAny<T>())).Returns(result);
        mockValidator.Setup(validator => validator.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);
    }

    internal static void Validate_IsNotValid<T>(this Mock<IValidator<T>> mockValidator, string errorMessage) where T : class
    {
        var result = new FluentValidation.Results.ValidationResult();
        result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, errorMessage));

        mockValidator.Setup(validator => validator.Validate(It.IsAny<T>())).Returns(result);
        mockValidator.Setup(validator => validator.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);
    }

    internal static void IsValid_True<T>(this Mock<T> view) where T : class, IView
        => view.Setup(v => v.IsValid()).Returns(true);

    internal static void IsValid_False<T>(this Mock<T> view) where T : class, IView
        => view.Setup(v => v.IsValid()).Returns(false);
}
