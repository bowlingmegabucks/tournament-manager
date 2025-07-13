using Bogus;
using NortheastMegabuck.Api.Tournaments.GetTournaments;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusGetTournamentsResponse
    : Faker<GetTournamentsResponse>
{
    public BogusGetTournamentsResponse()
    {
        RuleFor(response => response.Tournaments, f => new BogusGetTournamentsDto().Generate(f.Random.Int(min: 1, max: 10)).AsReadOnly());
    }
}