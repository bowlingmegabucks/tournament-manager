using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace NortheastMegabuck.Tests.Extensions;
internal static class Moq
{
    internal static DbSet<TEntity> SetUpDbContext<TEntity>(this IEnumerable<TEntity> items) where TEntity : class
    {
        var mockSet = new Mock<DbSet<TEntity>>();
        var queryable = items.AsQueryable();

        mockSet.As<IQueryable<TEntity>>().Setup(mock => mock.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<TEntity>>().Setup(mock => mock.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<TEntity>>().Setup(mock => mock.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<TEntity>>().Setup(mock => mock.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        return mockSet.Object;
    }

    internal static void Validate_IsValid<T>(this Mock<IValidator<T>> mockValidator) where T : class
    {
        var result = new FluentValidation.Results.ValidationResult();

        mockValidator.Setup(validator => validator.Validate(It.IsAny<T>())).Returns(result);
        mockValidator.Setup(validator => validator.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);
    }

    internal static void Validate_IsNotValid<T>(this Mock<IValidator<T>> mockValidator, string propertyName, string errorMessage) where T : class
    {
        var result = new FluentValidation.Results.ValidationResult();
        result.Errors.Add(new FluentValidation.Results.ValidationFailure(propertyName,errorMessage));

        mockValidator.Setup(validator => validator.Validate(It.IsAny<T>())).Returns(result);
        mockValidator.Setup(validator => validator.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);
    }
}
