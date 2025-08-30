namespace BowlingMegabucks.TournamentManager.Presentation;

internal sealed record OffsetPagingResult<T>
{
    public IReadOnlyCollection<T> Items { get; init; } = [];
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int TotalItems { get; init; }
}
