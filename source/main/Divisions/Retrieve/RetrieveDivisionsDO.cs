using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Divisions.Retrieve;
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
    /// <param name="repository"></param>
    internal DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Models.Division> ForTournament(Guid tournamentId)
        => _repository.ForTournament(tournamentId).Select(division=> new Models.Division(division));
}

internal interface IDataLayer
{
    IEnumerable<Models.Division> ForTournament(Guid tournamentId);
}