namespace BowlingMegabucks.TournamentManager.App.Controls.Grids;

/// <summary>
/// Event arguments for pagination change events.
/// </summary>
internal sealed class PagingChangeEventArgs : EventArgs
{
    /// <summary>
    /// Gets the current page number (1-based).
    /// </summary>
    public int CurrentPage { get; }

    /// <summary>
    /// Gets the page size (number of records per page).
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagingChangeEventArgs"/> class.
    /// </summary>
    /// <param name="currentPage">The current page number (1-based).</param>
    /// <param name="pageSize">The page size (number of records per page).</param>
    public PagingChangeEventArgs(int currentPage, int pageSize)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}
