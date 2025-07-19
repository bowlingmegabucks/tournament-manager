
namespace BowlingMegabucks.TournamentManager.Squads.Complete;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => await _repository.CompleteAsync(id, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}