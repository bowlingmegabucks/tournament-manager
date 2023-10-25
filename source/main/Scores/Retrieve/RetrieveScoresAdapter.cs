
namespace NortheastMegabuck.Scores.Retrieve;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IConfiguration config)
    {
        _businessLogic = new BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = mockBusinessLogic;
    }

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
        => (await _businessLogic.ExecuteAsync(new[] { squadId }, cancellationToken).ConfigureAwait(false)).Select(squadScore => new ViewModel(squadScore));
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}