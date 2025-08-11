using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;
using BowlingMegabucks.TournamentManager.Registrations;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

internal static class CreateRegistrationRequestFactory
{
    public static CreateRegistrationRequest Create(
        TournamentId? tournamentId = null,
        BowlerInput? bowler = null,
        DivisionId? divisionId = null,
        IEnumerable<SquadId>? squads = null,
        IEnumerable<SquadId>? sweepers = null,
        bool? superSweeper = false,
        PaymentInput? payment = null,
        int? average = null
    )
        => new()
        {
            Registration = new()
            {
                TournamentId = tournamentId ?? TournamentId.New(),
                Bowler = bowler ?? BowlerInputFactory.Bogus(),
                DivisionId = divisionId ?? DivisionId.New(),
                Squads = squads ?? new[] { SquadId.New() },
                Sweepers = sweepers ?? new[] { SquadId.New() },
                SuperSweeper = superSweeper ?? true,
                Payment = payment ?? PaymentInputFactory.Create(),
                Average = average
            }
        };
}