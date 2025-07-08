
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Scores.Update;
internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _entityMapper;
    private readonly IRepository _repository;

    public DataLayer(IConfiguration config)
    {
        _entityMapper = new EntityMapper();
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockEntityMapper"></param>
    /// <param name="mockRepository"></param>
    internal DataLayer(IEntityMapper mockEntityMapper, IRepository mockRepository)
    {
        _entityMapper = mockEntityMapper;
        _repository = mockRepository;
    }

    public async Task ExecuteAsync(IEnumerable<Models.SquadScore> scores, CancellationToken cancellationToken)
        => await _repository.UpdateAsync([.. scores.Select(_entityMapper.Execute)], cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(IEnumerable<Models.SquadScore> scores, CancellationToken cancellationToken);
}