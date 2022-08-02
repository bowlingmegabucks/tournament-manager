
namespace NewEnglandClassic.Bowlers.Retrieve;
internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    internal BusinessLogic()
    {

    }

    public Models.Bowler? Execute(Guid id)
        => null;
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Models.Bowler? Execute(Guid id);
}