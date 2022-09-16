using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Tournaments;
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
    
    IEnumerable<Database.Entities.Tournament> IRepository.RetrieveAll()
        => _dataContext.Tournaments.AsNoTracking().AsEnumerable();

    Database.Entities.Tournament IRepository.Retrieve(TournamentId id)
        => _dataContext.Tournaments.AsNoTracking().Single(tournament=> tournament.Id == id);

    Database.Entities.Tournament IRepository.Retrieve(DivisionId divisionId)
        => _dataContext.Tournaments.Include(tournament => tournament.Divisions).Include(tournament=> tournament.Sweepers).ThenInclude(sweeper=> sweeper.Divisions).AsNoTracking().Single(tournament => tournament.Divisions.Any(division => division.Id == divisionId));

    Database.Entities.Tournament IRepository.Retrieve(SquadId squadId)
        => _dataContext.Tournaments.Include(tournament => tournament.Squads).Include(tournament => tournament.Sweepers).AsNoTracking().Single(tournament => tournament.Squads.Any(squad => squad.Id == squadId) || tournament.Sweepers.Any(sweeper=> sweeper.Id == squadId));

    TournamentId IRepository.Add(Database.Entities.Tournament tournament)
    {
        _dataContext.Tournaments.Add(tournament);
        _dataContext.SaveChanges();
        
        return tournament.Id;
    }
}

internal interface IRepository
{
    IEnumerable<Database.Entities.Tournament> RetrieveAll();

    Database.Entities.Tournament Retrieve(TournamentId id);

    Database.Entities.Tournament Retrieve(DivisionId divisionId);

    Database.Entities.Tournament Retrieve(SquadId squadId);

    TournamentId Add(Database.Entities.Tournament tournament);
}