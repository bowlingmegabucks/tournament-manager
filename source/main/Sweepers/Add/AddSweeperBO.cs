namespace NewEnglandClassic.Sweepers.Add;

internal class BusinessLogic : IBusinessLogic
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    private readonly Tournaments.Retrieve.IBusinessLogic _getTournamentBO;

    private readonly Lazy<FluentValidation.IValidator<Models.Sweeper>> _validator;
    private FluentValidation.IValidator<Models.Sweeper> Validator => _validator.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    public BusinessLogic(IConfiguration config)
    {
        _getTournamentBO = new Tournaments.Retrieve.BusinessLogic(config);
        _validator = new Lazy<FluentValidation.IValidator<Models.Sweeper>>(() => new Validator());
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockGetTournamentBO"></param>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(Tournaments.Retrieve.IBusinessLogic mockGetTournamentBO, FluentValidation.IValidator<Models.Sweeper> mockValidator, IDataLayer mockDataLayer)
    {
        _getTournamentBO = mockGetTournamentBO;
        _validator = new Lazy<FluentValidation.IValidator<Models.Sweeper>>(() => mockValidator);
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    public Guid? Execute(Models.Sweeper sweeper)
    {
        var tournament = _getTournamentBO.Execute(sweeper.TournamentId);

        if (_getTournamentBO.Error != null)
        {
            Errors = new[] { _getTournamentBO.Error };

            return null;
        }

        sweeper.Tournament = tournament!;

        var validationResults = Validator.Validate(sweeper);

        if (!validationResults.IsValid)
        {
            Errors = validationResults.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));

            return null;
        }
        
        try
        {
            return DataLayer.Execute(sweeper);
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

    Guid? Execute(Models.Sweeper sweeper);
}
