
namespace NortheastMegabuck.Bowlers.Retrieve;
internal class Adapter : IAdapter
{
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

    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    public async Task<IViewModel?> ExecuteAsync(BowlerId bowlerId, CancellationToken cancellationToken)
    {
        var bowler = await _businessLogic.ExecuteAsync(bowlerId, cancellationToken).ConfigureAwait(false);

        return bowler is not null ? new ViewModel(bowler) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IViewModel?> ExecuteAsync(BowlerId bowlerId, CancellationToken cancellationToken);
}