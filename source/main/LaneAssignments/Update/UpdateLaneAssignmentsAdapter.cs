

namespace NortheastMegabuck.LaneAssignments.Update;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    public IBusinessLogic _businessLogic;

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

    void IAdapter.Execute(SquadId squadId, BowlerId bowlerId, string position)
        => _businessLogic.Execute(squadId, bowlerId, position);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    void Execute(SquadId squadId, BowlerId bowlerId, string position);
}