
namespace NortheastMegabuck.Models;
internal class BowlerSearchCriteria
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public IEnumerable<SquadId> WithoutRegistrationOnSquads { get; set; } = Enumerable.Empty<SquadId>();

    public TournamentId? RegisteredInTournament { get; set; }
}
