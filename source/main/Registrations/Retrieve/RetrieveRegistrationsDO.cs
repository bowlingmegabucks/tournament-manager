
namespace NewEnglandClassic.Registrations.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IConfiguration config)
    {
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRepository"></param>
    internal DataLayer(IRepository mockRepository)
    {
        _repository = mockRepository;
    }

    IEnumerable<Models.Registration> IDataLayer.Execute(TournamentId tournamentId)
        => _repository.Retrieve(tournamentId).Select(registration => new Models.Registration(registration));
}

internal interface IDataLayer
{
    IEnumerable<Models.Registration> Execute(TournamentId tournamentId);
}