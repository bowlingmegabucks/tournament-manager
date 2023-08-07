using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Bowlers.Retrieve;
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

    async Task<Models.Bowler> IDataLayer.ExecuteAsync(BowlerId id, CancellationToken cancellationToken)
        => new (await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<Models.Bowler> ExecuteAsync(BowlerId id, CancellationToken cancellationToken);
}