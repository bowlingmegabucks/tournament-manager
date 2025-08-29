using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Tests;

/// <summary>
/// Provides extension methods for testing scenarios.
/// </summary>
public static class TestingExtensions
{
    /// <summary>
    /// Sets up pagination properties on a mock with proper default implementation behavior.
    /// </summary>
    /// <param name="mock">The mock to configure.</param>
    /// <param name="page">The page number (1-indexed).</param>
    /// <param name="pageSize">The number of items per page.</param>
    public static void SetupPagination(this Mock<IOffsetPaginationQuery> mock, int page = 1, int pageSize = 10)
    {
        ArgumentNullException.ThrowIfNull(mock);

        mock.SetupGet(p => p.Page).Returns(page);
        mock.SetupGet(p => p.PageSize).Returns(pageSize);

        mock.Setup(p => p.Offset).CallBase();
    }
}
