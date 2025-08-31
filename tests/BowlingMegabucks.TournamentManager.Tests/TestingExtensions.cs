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

    /// <summary>
    /// Creates a <see cref="CancellationTokenSource"/> that is linked to the provided <see cref="CancellationToken"/>.
    /// When the provided token is cancelled, the returned <see cref="CancellationTokenSource"/> will also be cancelled.
    /// </summary>
    /// <param name="cancellationToken">The existing <see cref="CancellationToken"/> to link to the new source.</param>
    /// <returns>A <see cref="CancellationTokenSource"/> linked to the specified token.</returns>
    public static CancellationTokenSource CreateLinkedCancellationTokenSource(this CancellationToken cancellationToken)
        => CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
}
