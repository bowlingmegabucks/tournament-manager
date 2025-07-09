using Bogus;
using NortheastMegabuck.Api.Tournaments.GetTournament;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusGetTournamentResponse
    : Faker<GetTournamentResponse>
{
    public BogusGetTournamentResponse(TournamentId id)
    {
        RuleFor(dto => dto.Tournament, _ => new BogusTournamentDetailDto(id).Generate());
    }
}