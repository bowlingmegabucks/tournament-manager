namespace NewEnglandClassic.Sweepers.Add;
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
    
    public SquadId Execute(Models.Sweeper sweeper)
    {
        var entity = _mapper.Execute(sweeper);
        
        return _repository.Add(entity);
    }
}

internal interface IDataLayer
{
    SquadId Execute(Models.Sweeper sweeper);
}