using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic.Divisions;

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

    Guid IRepository.Add(Database.Entities.Division division)
    {
        _dataContext.Divisions.Add(division);
        _dataContext.SaveChanges();

        return division.Id; ;
    }

    IEnumerable<Database.Entities.Division> IRepository.ForTournament(Guid tournamentId)
        => _dataContext.Divisions.Where(division => division.TournamentId == tournamentId).AsEnumerable();
}

internal interface IRepository
{
    Guid Add(Database.Entities.Division division);

    IEnumerable<Database.Entities.Division> ForTournament(Guid tournamentId);
}