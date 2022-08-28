namespace NewEnglandClassic.Tournaments.Retrieve;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    internal DataLayer(IConfiguration config)
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

    IEnumerable<Models.Tournament> IDataLayer.Execute()
        => _repository.RetrieveAll().Select(tournament => new Models.Tournament(tournament));

    Models.Tournament IDataLayer.Execute(TournamentId id)
        => new(_repository.Retrieve(id));

    Models.Tournament IDataLayer.Execute(NewEnglandClassic.Divisions.Id id)
        => new(_repository.Retrieve(id));
}

internal interface IDataLayer
{
    IEnumerable<Models.Tournament> Execute();

    Models.Tournament Execute(TournamentId id);

    Models.Tournament Execute(NewEnglandClassic.Divisions.Id id);
}