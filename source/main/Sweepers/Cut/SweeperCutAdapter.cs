
namespace NortheastMegabuck.Sweepers.Cut;
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
    { 
        var result = _businessLogic.Execute(squadId);

        if (result == null)
        {
            return Enumerable.Empty<IViewModel>();
        }

        var scores = result.Scores.ToList();

        var placings = new List<ViewModel>();

        for (short i = 1; i <= scores.Count; i++)
        {
            placings.Add(new ViewModel(scores[i - 1], i, result.CasherCount));
        }

        return placings;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute(SquadId squadId);
}