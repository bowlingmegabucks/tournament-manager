using System.Collections.ObjectModel;

namespace NortheastMegabuck.Api.Tournaments.GetTournaments;

/// <summary>
/// Response for retrieving a list of tournaments.
/// </summary>
public sealed record GetTournamentsResponse
{
    /// <summary>
    /// Array of tournament summaries.
    /// </summary>
    public required ReadOnlyCollection<GetTournamentsDto> Tournaments { get; init; }

    /// <summary>
    /// Total count of tournaments in the response.
    /// </summary>
    public int TotalCount
        => Tournaments.Count;
}