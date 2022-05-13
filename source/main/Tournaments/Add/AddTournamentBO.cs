using FluentValidation;

namespace NewEnglandClassic.Tournaments.Add;
internal class BusinessLogic : IBusinessLogic
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    private readonly IValidator<Models.Tournament> _validator;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    internal BusinessLogic(IConfiguration config)
    {
        _validator = new Validator();
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(IValidator<Models.Tournament> mockValidator, IDataLayer mockDataLayer)
    {
        _validator = mockValidator;
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    public Guid? Execute(Models.Tournament tournament)
    {
        var validation = _validator.Validate(tournament);

        if (!validation.IsValid)
        {
            Errors = validation.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));
            return null;
        }

        try
        {
            return DataLayer.Execute(tournament);
        }
        catch (Exception ex)
        {
            Errors = new[] { new Models.ErrorDetail(ex) };
            return null;
        }
    }
}

internal interface IBusinessLogic
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Guid? Execute(Models.Tournament tournament);
}