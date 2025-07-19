using Bogus;
using BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusGetTournamentsResponse
    : Faker<GetTournamentsResponse>
{
    public BogusGetTournamentsResponse()
    {
        RuleFor(response => response.Tournaments, f => new BogusGetTournamentsDto().Generate(f.Random.Int(min: 1, max: 10)).AsReadOnly());
    }
}