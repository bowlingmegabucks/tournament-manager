namespace NortheastMegabuck.Squads.Add;
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

    public async Task<SquadId> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken)
    {
        var entity = _mapper.Execute(squad);
        
        return await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IDataLayer
{
    Task<SquadId> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken);
}