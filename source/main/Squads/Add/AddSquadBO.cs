namespace NortheastMegabuck.Squads.Add;

internal class BusinessLogic : IBusinessLogic
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    private readonly Tournaments.Retrieve.IBusinessLogic _getTournamentBO;

    private readonly Lazy<FluentValidation.IValidator<Models.Squad>> _validator;
    private FluentValidation.IValidator<Models.Squad> Validator => _validator.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    public BusinessLogic(IConfiguration config)
    {
        _getTournamentBO = new Tournaments.Retrieve.BusinessLogic(config);
        _validator = new Lazy<FluentValidation.IValidator<Models.Squad>>(() => new Validator());
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockGetTournamentBO"></param>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(Tournaments.Retrieve.IBusinessLogic mockGetTournamentBO, FluentValidation.IValidator<Models.Squad> mockValidator, IDataLayer mockDataLayer)
    {
        _getTournamentBO = mockGetTournamentBO;
        _validator = new Lazy<FluentValidation.IValidator<Models.Squad>>(() => mockValidator);
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    public async Task<SquadId?> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken)
    {
        var tournament = await _getTournamentBO.ExecuteAsync(squad.TournamentId, cancellationToken).ConfigureAwait(false);

        if (_getTournamentBO.Error != null)
        {
            Errors = new[] { _getTournamentBO.Error };

            return null;
        }

        squad.Tournament = tournament!;

        var validationResults = await Validator.ValidateAsync(squad, cancellationToken).ConfigureAwait(false);

        if (!validationResults.IsValid)
        {
            Errors = validationResults.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));

            return null;
        }
        
        try
        { 
            return await DataLayer.ExecuteAsync(squad, cancellationToken).ConfigureAwait(false);
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

    Task<SquadId?> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken);
}
