namespace BowlingMegabucks.TournamentManager.App.Controls.Grids;

internal sealed class PageChangeCommittedEventArgs
    : EventArgs
{
    public int NewPage { get; }

    public int NewPageSize { get; }

    public CancellationTokenSource CancellationTokenSource { get; }

    public PageChangeCommittedEventArgs(int newPage, int newPageSize, CancellationTokenSource cts)
    {
        NewPage = newPage;
        NewPageSize = newPageSize;
        CancellationTokenSource = cts;
    }
}
