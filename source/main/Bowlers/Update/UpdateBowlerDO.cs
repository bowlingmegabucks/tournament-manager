
namespace NortheastMegabuck.Bowlers.Update;

internal sealed class DataLayer : IDataLayer
{
    private readonly IRepository _repository;
    private readonly IEntityMapper _entityMapper;

    public DataLayer(IConfiguration config)
    {
        _repository = new Repository(config);
        _entityMapper = new EntityMapper();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRepository"></param>
    internal DataLayer(IRepository mockRepository, IEntityMapper mockEntityMapper)
    {
        _repository = mockRepository;
        _entityMapper = mockEntityMapper;
    }

    async Task IDataLayer.ExecuteAsync(BowlerId id, Models.PersonName name, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(id, name.First, name.MiddleInitial, name.Last, name.Suffix, cancellationToken).ConfigureAwait(false);

    async Task IDataLayer.ExecuteAsync(Models.Bowler bowler, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(_entityMapper.Execute(bowler), cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(BowlerId id, Models.PersonName name, CancellationToken cancellationToken);

    Task ExecuteAsync(Models.Bowler bowler, CancellationToken cancellationToken);
}