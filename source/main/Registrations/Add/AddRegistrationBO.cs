
namespace NortheastMegabuck.Registrations.Add;
internal class BusinessLogic : IBusinessLogic
{
    private readonly Divisions.Retrieve.IBusinessLogic _getDivisionBO;

    private readonly Lazy<Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Tournaments.Retrieve.IBusinessLogic GetTournamentBO => _getTournamentBO.Value;

    private readonly Lazy<Bowlers.Retrieve.IBusinessLogic> _getBowlerBO;
    private Bowlers.Retrieve.IBusinessLogic GetBowlerBO => _getBowlerBO.Value;

    private readonly Lazy<FluentValidation.IValidator<Models.Registration>> _validator;
    private FluentValidation.IValidator<Models.Registration> Validator => _validator.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    public BusinessLogic(IConfiguration config)
    {
        _getDivisionBO = new Divisions.Retrieve.BusinessLogic(config);
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => new Tournaments.Retrieve.BusinessLogic(config));
        _getBowlerBO = new Lazy<Bowlers.Retrieve.IBusinessLogic>(() => new Bowlers.Retrieve.BusinessLogic(config));
        _validator = new Lazy<FluentValidation.IValidator<Models.Registration>>(() => new Validator());
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockGetDivisionBO"></param>
    /// <param name="mockGetTournamentBO"></param>
    /// <param name="mockGetBowlerBO"></param>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(Divisions.Retrieve.IBusinessLogic mockGetDivisionBO, Tournaments.Retrieve.IBusinessLogic mockGetTournamentBO, Bowlers.Retrieve.IBusinessLogic mockGetBowlerBO, FluentValidation.IValidator<Models.Registration> mockValidator, IDataLayer mockDataLayer)
    {
        _getDivisionBO = mockGetDivisionBO;
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => mockGetTournamentBO);
        _getBowlerBO = new Lazy<Bowlers.Retrieve.IBusinessLogic>(() => mockGetBowlerBO);
        _validator = new Lazy<FluentValidation.IValidator<Models.Registration>>(()=> mockValidator);
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    public async Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken)
    {
        var division = _getDivisionBO.Execute(registration.Division.Id);

        if (_getDivisionBO.Error != null)
        {
            Errors = new[] { _getDivisionBO.Error };

            return null;
        }

        registration.Division = division!;

        var tournament = await GetTournamentBO.ExecuteAsync(division!.Id, cancellationToken).ConfigureAwait(false);

        if (GetTournamentBO.Error is not null)
        {
            Errors = new[] { GetTournamentBO.Error };

            return null;
        }

        registration.TournamentStartDate = tournament!.Start;
        registration.SweeperCount = tournament!.Sweepers.Count();

        if (registration.Bowler.Id.Value != Guid.Empty)
        {
            var bowler = GetBowlerBO.Execute(registration.Bowler.Id);

            if (GetBowlerBO.Error != null)
            {
                Errors = new[] { GetBowlerBO.Error };

                return null;
            }

            registration.Bowler = bowler!;
        }

        var validatorResults = await Validator.ValidateAsync(registration, cancellationToken).ConfigureAwait(false);

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

    public Models.Registration? Execute(BowlerId bowlerId, SquadId squadId)
    {
        try
        {
            //UI search is doing a lot of the heavy lifting here.
            //when revisiting, add some validation methods to make sure the bowler is in the tournament
            //already and not already on the squad
            return DataLayer.Execute(bowlerId, squadId);
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

    Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken);

    Models.Registration? Execute(BowlerId bowlerId, SquadId squadId);
}