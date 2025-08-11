
namespace BowlingMegabucks.TournamentManager.Registrations.Add;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Models.Registration> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
        => new(await _repository.AddSquadAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<Models.Registration> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);
}