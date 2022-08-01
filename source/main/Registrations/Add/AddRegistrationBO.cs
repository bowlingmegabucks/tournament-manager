
namespace NewEnglandClassic.Registrations.Add;
internal class BusinessLogic : IBusinessLogic
{
    private readonly Divisions.Retrieve.IBusinessLogic _getDivisionBO;

    private readonly Lazy<Bowlers.Retrieve.IBusinessLogic> _getBowlerBO;
    private Bowlers.Retrieve.IBusinessLogic GetBowlerBO => _getBowlerBO.Value;

    private readonly Lazy<FluentValidation.IValidator<Models.Registration>> _validator;
    private FluentValidation.IValidator<Models.Registration> Validator => _validator.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    public BusinessLogic(IConfiguration config)
    {
        _getDivisionBO = new Divisions.Retrieve.BusinessLogic(config);
        _getBowlerBO = new Lazy<Bowlers.Retrieve.IBusinessLogic>(() => new Bowlers.Retrieve.BusinessLogic());
        _validator = new Lazy<FluentValidation.IValidator<Models.Registration>>(() => new Validator());
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockGetDivisionBO"></param>
    /// <param name="mockGetBowlerBO"></param>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(Divisions.Retrieve.IBusinessLogic mockGetDivisionBO, Bowlers.Retrieve.IBusinessLogic mockGetBowlerBO, FluentValidation.IValidator<Models.Registration> mockValidator, IDataLayer mockDataLayer)
    {
        _getDivisionBO = mockGetDivisionBO;
        _getBowlerBO = new Lazy<Bowlers.Retrieve.IBusinessLogic>(() => mockGetBowlerBO);
        _validator = new Lazy<FluentValidation.IValidator<Models.Registration>>(()=> mockValidator);
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    public Guid? Execute(Models.Registration registration)
    {
        var division = _getDivisionBO.Execute(registration.Division.Id);

        if (_getDivisionBO.Error != null)
        {
            Errors = new[] { _getDivisionBO.Error };

            return null;
        }

        registration.Division = division!;

        //todo: get tournament start date

        if (registration.Bowler.Id != Guid.Empty)
        {
            var bowler = GetBowlerBO.Execute(registration.Bowler.Id);

            if (GetBowlerBO.Error != null)
            {
                Errors = new[] { GetBowlerBO.Error };

                return null;
            }

            registration.Bowler = bowler!;
        }

        var validatorResults = Validator.Validate(registration);

        if (!validatorResults.IsValid)
        {
            Errors = validatorResults.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));

            return null;
        }

        try
        {
            return DataLayer.Execute(registration);
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

    Guid? Execute(Models.Registration registration);
}