using Bogus;
using BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournament;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusGetTournamentResponse
    : Faker<GetTournamentResponse>
{
    public BogusGetTournamentResponse(TournamentId id)
    {
        RuleFor(dto => dto.Tournament, _ => new BogusTournamentDetailDto(id).Generate());
    }
}