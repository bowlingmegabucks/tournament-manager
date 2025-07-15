
namespace NortheastMegabuck.Scores.Update;

internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _entityMapper;
    private readonly IRepository _repository;

    internal DataLayer(IEntityMapper entityMapper, IRepository repository)
    {
        _entityMapper = entityMapper;
        _repository = repository;
    }

    public async Task ExecuteAsync(IEnumerable<Models.SquadScore> scores, CancellationToken cancellationToken)
        => await _repository.UpdateAsync([.. scores.Select(_entityMapper.Execute)], cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(IEnumerable<Models.SquadScore> scores, CancellationToken cancellationToken);
}