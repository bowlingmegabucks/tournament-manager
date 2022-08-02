
namespace NewEnglandClassic.Bowlers.Retrieve;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Adapter()
    {
        _businessLogic = new BusinessLogic();
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

    public IViewModel? Execute(Guid bowlerId)
    {
        var bowler = _businessLogic.Execute(new BowlerId(bowlerId));

        return bowler != null ? new ViewModel(bowler) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    IViewModel? Execute(Guid bowlerId);
}