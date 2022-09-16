
namespace NortheastMegabuck.Registrations.Add;
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

    public RegistrationId Execute(Models.Registration registration)
    {
        var entity = _mapper.Execute(registration);
        
        return _repository.Add(entity);
    }
    public Models.Registration Execute(BowlerId bowlerId, SquadId squadId)
        => new(_repository.AddSquad(bowlerId, squadId));
}

internal interface IDataLayer
{
    RegistrationId Execute(Models.Registration registration);

    Models.Registration Execute(BowlerId bowlerId, SquadId squadId);
}