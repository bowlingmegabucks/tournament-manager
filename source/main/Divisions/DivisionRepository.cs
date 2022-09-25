using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Divisions;

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

    DivisionId IRepository.Add(Database.Entities.Division division)
    {
        _dataContext.Divisions.Add(division);
        _dataContext.SaveChanges();

        return division.Id;
    }

    IEnumerable<Database.Entities.Division> IRepository.Retrieve(TournamentId tournamentId)
        => _dataContext.Divisions.AsNoTracking().Where(division => division.TournamentId == tournamentId).AsEnumerable();

    Database.Entities.Division IRepository.Retrieve(NortheastMegabuck.DivisionId id)
        => _dataContext.Divisions.Single(division => division.Id == id);
}

internal interface IRepository
{
    DivisionId Add(Database.Entities.Division division);

    IEnumerable<Database.Entities.Division> Retrieve(TournamentId tournamentId);

    Database.Entities.Division Retrieve(DivisionId id);
}