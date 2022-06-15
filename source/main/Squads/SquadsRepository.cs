using Microsoft.EntityFrameworkCore;

namespace NewEnglandClassic.Squads;

internal class Repository : IRepository
{
    private readonly Database.IDataContext _dataContext;
    internal Repository(IConfiguration config)
    {
        _dataContext = new Database.DataContext(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataContext"></param>
    internal Repository(Database.IDataContext mockDataContext)
    {
        _dataContext = mockDataContext;
    }

    public Guid Add(Database.Entities.TournamentSquad squad)
    {
        _dataContext.Squads.Add(squad);
        _dataContext.SaveChanges();

        return squad.Id;
    }

    public IEnumerable<Database.Entities.TournamentSquad> ForTournament(Guid tournamentId)
        => _dataContext.Squads.AsNoTracking().Where(squad => squad.TournamentId == tournamentId).AsEnumerable();
}

internal interface IRepository
{
    Guid Add(Database.Entities.TournamentSquad squad);

    IEnumerable<Database.Entities.TournamentSquad> ForTournament(Guid tournamentId);
}
