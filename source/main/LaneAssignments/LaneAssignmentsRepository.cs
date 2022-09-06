using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.LaneAssignments;
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

    IEnumerable<Database.Entities.SquadRegistration> IRepository.Retrieve(SquadId squadId)
    { 
        if (_dataContext.Squads.Any(squad => squad.Id == squadId))
        {
            return _dataContext.Squads
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
                .Include(squad => squad.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Division)
                .AsNoTracking()
                .Where(squad => squad.Id == squadId)
                .SelectMany(squad => squad.Registrations);
        }

        //todo: verify this works when bringing in sweepers
        return _dataContext.Sweepers
            .Include(sweeper => sweeper.Registrations).ThenInclude(squadRegistration => squadRegistration.Registration).ThenInclude(registration => registration.Bowler)
            .Include(sweeper => sweeper.Registrations).ThenInclude(squadRegistration => squadRegistration.Squad as Database.Entities.SweeperSquad).ThenInclude(sweeper => sweeper!.Divisions)
            .AsNoTracking()
            .Where(sweeper => sweeper.Id == squadId)
            .SelectMany(sweeper => sweeper.Registrations);
    }
}

internal interface IRepository
{
    IEnumerable<Database.Entities.SquadRegistration> Retrieve(SquadId squadId);
}