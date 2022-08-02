namespace NewEnglandClassic.Registrations.Add;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    internal Adapter(IConfiguration config)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => new BusinessLogic(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => mockBusinessLogic);
    }

    public Guid? Execute(Bowlers.Add.IViewModel bowler, Guid divisionId, IEnumerable<Guid> squads, IEnumerable<Guid> sweepers, int? average)
        => Execute(new Models.Registration(new Models.Bowler(bowler), divisionId, squads, sweepers, average));

    private Guid? Execute(Models.Registration registration)
    {
        var id = BusinessLogic.Execute(registration);

        Errors = BusinessLogic.Errors;

        return id;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Guid? Execute(Bowlers.Add.IViewModel bowler, Guid divisionId, IEnumerable<Guid> squads, IEnumerable<Guid> sweepers, int? average);
}