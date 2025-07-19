using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;

namespace BowlingMegabucks.TournamentManager.Api.BogusData;

internal sealed class BogusGetRegistrationResponse
    : Faker<GetRegistrationResponse>
{
    public BogusGetRegistrationResponse()
    {
        RuleFor(response => response.Registration, _ => new BogusRegistrationDetailDto());
    }
}