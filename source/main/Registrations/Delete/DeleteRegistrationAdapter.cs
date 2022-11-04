
namespace NortheastMegabuck.Registrations.Delete;
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

    public void Execute(BowlerId bowlerId, SquadId squadId)
        => _businessLogic.Execute(bowlerId, squadId);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    void Execute(BowlerId bowlerId, SquadId squadId);
}