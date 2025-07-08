using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Sweepers.Add;
internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _mapper;
    private readonly IRepository _repository;

    internal DataLayer(IConfiguration config)
    {
        _mapper = new EntityMapper();
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockMapper"></param>
    /// <param name="mockRepository"></param>
    internal DataLayer(IEntityMapper mockMapper, IRepository mockRepository)
    {
        _mapper = mockMapper;
        _repository = mockRepository;
    }

    public async Task<SquadId> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken)
    {
        var entity = _mapper.Execute(sweeper);

        return await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IDataLayer
{
    Task<SquadId> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken);
}