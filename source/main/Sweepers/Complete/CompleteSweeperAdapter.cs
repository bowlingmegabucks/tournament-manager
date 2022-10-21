
namespace NortheastMegabuck.Sweepers.Complete;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IConfiguration config)
    {
        _businessLogic = new BusinessLogic(config);
    }

    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = mockBusinessLogic;
    }

    public void Execute(SquadId id)
        => _businessLogic.Execute(id);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    void Execute(SquadId id);
}