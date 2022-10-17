
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

    public IEnumerable<IViewModel> Execute(SquadId squadId)
        => _businessLogic.Execute(squadId).Select(squadScore => new ViewModel(squadScore));
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute(SquadId squadId);
}