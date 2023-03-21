
namespace NortheastMegabuck.Bowlers.Update;
internal class Adapter : IAdapter
{
    public IEnumerable<Models.ErrorDetail> Errors
        => _businessLogic.Errors;

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

    void IAdapter.Execute(BowlerId id, INameViewModel viewModel)
        => _businessLogic.Execute(id, new Models.PersonName(viewModel));
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    void Execute(BowlerId id, INameViewModel viewModel);
}