
namespace BowlingMegabucks.TournamentManager.Registrations;

internal class EntityMapper : IEntityMapper
{
    private readonly Bowlers.IEntityMapper _bowlerMapper;
    private readonly IPaymentEntityMapper _paymentMapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowlerMapper"></param>
    public EntityMapper(Bowlers.IEntityMapper bowlerMapper, IPaymentEntityMapper paymentMapper)
    {
        _bowlerMapper = bowlerMapper;
        _paymentMapper = paymentMapper;
    }

    public Database.Entities.Registration Execute(Models.Registration registration)
        => new()
        {
            Id = registration.Id,
            BowlerId = registration.Bowler.Id,
            Bowler = _bowlerMapper.Execute(registration.Bowler),
            DivisionId = registration.Division.Id,
            Average = registration.Average,
            Squads = [.. registration.Squads.Select(squad => squad.Id).Union(registration.Sweepers.Select(sweeper => sweeper.Id)).Select(
                id => new Database.Entities.SquadRegistration
                {
                    RegistrationId = registration.Id,
                    SquadId = id
                })],
            Payments = [.. registration.Payments.Select(_paymentMapper.Execute)],
            SuperSweeper = registration.SuperSweeper
        };
}

internal interface IEntityMapper
{
    Database.Entities.Registration Execute(Models.Registration registration);
}