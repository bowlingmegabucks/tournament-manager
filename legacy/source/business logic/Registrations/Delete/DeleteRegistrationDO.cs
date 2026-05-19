
namespace BowlingMegabucks.TournamentManager.Registrations.Delete;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
        => await _repository.DeleteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);
}