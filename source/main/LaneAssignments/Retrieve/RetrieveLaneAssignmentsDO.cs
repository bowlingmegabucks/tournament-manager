using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.LaneAssignments.Retrieve;
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

    IEnumerable<Models.LaneAssignment> IDataLayer.Execute(SquadId squadId)
        => _repository.Retrieve(squadId).Select(squadRegistration => new Models.LaneAssignment(squadRegistration));
}

internal interface IDataLayer
{
    IEnumerable<Models.LaneAssignment> Execute(SquadId squadId);
}