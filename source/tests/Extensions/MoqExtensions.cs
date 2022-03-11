using Microsoft.EntityFrameworkCore;

namespace NewEnglandClassic.Tests.Extensions;
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
}
