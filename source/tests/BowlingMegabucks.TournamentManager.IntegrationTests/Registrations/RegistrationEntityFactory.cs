using Bogus;
using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.IntegrationTests.Bowlers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

internal static class RegistrationEntityFactory
{
    public static Registration Create(
        RegistrationId? id = null,
        Division? division = null,
        Bowler? bowler = null,
        IEnumerable<SquadId>? squadIds = null,
        IEnumerable<SquadId>? sweeperIds = null,
        bool? superSweeper = null,
        int? average = null,
        Payment? payment = null
    ) => new()
    {
        Id = id ?? RegistrationId.New(),
        DivisionId = division?.Id ?? DivisionId.New(),
        BowlerId = bowler?.Id ?? BowlerId.New(),
        Squads = squadIds?.Union(sweeperIds ?? []).Select(squadId => new SquadRegistration
        {
            SquadId = squadId,
            RegistrationId = id ?? RegistrationId.New()
        }).ToList() ?? [],
        SuperSweeper = superSweeper ?? false,
        Average = average,
        Payments = [payment ?? PaymentEntityFactory.Bogus()]
    };
    
    public static Registration Bogus(Bowler bowler, Division division, IEnumerable<Squad>? squads = null)
        => new RegistrationEntityFaker(bowler, division, squads).Generate();
}

internal sealed class RegistrationEntityFaker
    : Faker<Registration>
{
    public RegistrationEntityFaker(Bowler? bowler = null, Division? division = null, IEnumerable<Squad>? squads = null)
        : base()
    {
        RuleFor(registration => registration.Id, _ => RegistrationId.New());

        RuleFor(registration => registration.Division, _ => division ?? DivisionEntityFactory.Bogus(1, TournamentId.New()).Single());
        RuleFor(registration => registration.DivisionId, (_, registration) => registration.Division.Id);

        RuleFor(registration => registration.Bowler, _ => bowler ?? BowlerEntityFactory.Bogus());
        RuleFor(registration => registration.BowlerId, (_, registration) => registration.Bowler.Id);

        RuleFor(registration => registration.Average, f => f.Random.Short(150, 230).OrNull(f));
        RuleFor(registration => registration.Payments, (_, registration) => PaymentEntityFactory.Bogus(3, registration.Id));

        RuleFor(registration => registration.Squads, (_, registration) => [.. (squads ?? SquadEntityFactory.Bogus(2, TournamentId.New())).Select(squad => new SquadRegistration
        {
            Squad = squad,
            SquadId = squad.Id,
            RegistrationId = registration.Id
        })]);
    }
}