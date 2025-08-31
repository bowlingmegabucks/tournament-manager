namespace BowlingMegabucks.TournamentManager.App.Controls.Grids;

/// <summary>
/// Event arguments for pagination change events.
/// </summary>
internal sealed class PagingChangeEventArgs : EventArgs
{
    /// <summary>
    /// Gets the previous page number (1-based).
    /// </summary>
    public int PreviousPage { get; }

    /// <summary>
    /// Gets the previous page size (number of records per page).
    /// </summary>
    public int PreviousPageSize { get; }

    /// <summary>
    /// Gets the new page number (1-based).
    /// </summary>
    public int NewPage { get; }

    /// <summary>
    /// Gets the new page size (number of records per page).
    /// </summary>
    public int NewPageSize { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagingChangeEventArgs"/> class.
    /// </summary>
    /// <param name="previousPage">The previous page number (1-based).</param>
    /// <param name="previousPageSize">The previous page size.</param>
    /// <param name="newPage">The new page number (1-based).</param>
    /// <param name="newPageSize">The new page size.</param>
    public PagingChangeEventArgs(int previousPage, int previousPageSize, int newPage, int newPageSize)
    {
        PreviousPage = previousPage;
        PreviousPageSize = previousPageSize;
        NewPage = newPage;
        NewPageSize = newPageSize;
    }
}
