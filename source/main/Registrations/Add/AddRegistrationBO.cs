
namespace NortheastMegabuck.Registrations.Add;
internal class BusinessLogic : IBusinessLogic
{
    private readonly Divisions.Retrieve.IBusinessLogic _getDivisionBO;

    private readonly Lazy<Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Tournaments.Retrieve.IBusinessLogic GetTournamentBO => _getTournamentBO.Value;

    private readonly Lazy<Bowlers.Search.IBusinessLogic> _searchBowlerBO;
    private Bowlers.Search.IBusinessLogic SearchBowlerBO => _searchBowlerBO.Value;

    private readonly Lazy<FluentValidation.IValidator<Models.Registration>> _validator;
    private FluentValidation.IValidator<Models.Registration> Validator => _validator.Value;

    private readonly Lazy<Bowlers.Update.IBusinessLogic> _updateBowlerBO;
    private Bowlers.Update.IBusinessLogic UpdateBowlerBO => _updateBowlerBO.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    public BusinessLogic(IConfiguration config)
    {
        _getDivisionBO = new Divisions.Retrieve.BusinessLogic(config);
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => new Tournaments.Retrieve.BusinessLogic(config));
        _searchBowlerBO = new Lazy<Bowlers.Search.IBusinessLogic>(() => new Bowlers.Search.BusinessLogic(config));
        _updateBowlerBO = new Lazy<Bowlers.Update.IBusinessLogic>(() => new Bowlers.Update.BusinessLogic(config));
        _validator = new Lazy<FluentValidation.IValidator<Models.Registration>>(() => new Validator());
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockGetDivisionBO"></param>
    /// <param name="mockGetTournamentBO"></param>
    /// <param name="mockSearchBowlerBO"></param>
    /// <param name="mockUpdateBowlerBO"></param>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(Divisions.Retrieve.IBusinessLogic mockGetDivisionBO, Tournaments.Retrieve.IBusinessLogic mockGetTournamentBO, Bowlers.Search.IBusinessLogic mockSearchBowlerBO, Bowlers.Update.IBusinessLogic mockUpdateBowlerBO, FluentValidation.IValidator<Models.Registration> mockValidator, IDataLayer mockDataLayer)
    {
        _getDivisionBO = mockGetDivisionBO;
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => mockGetTournamentBO);
        _searchBowlerBO = new Lazy<Bowlers.Search.IBusinessLogic>(() => mockSearchBowlerBO);
        _updateBowlerBO = new Lazy<Bowlers.Update.IBusinessLogic>(() => mockUpdateBowlerBO);
        _validator = new Lazy<FluentValidation.IValidator<Models.Registration>>(() => mockValidator);
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    public async Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken)
    {
        var division = await _getDivisionBO.ExecuteAsync(registration.Division.Id, cancellationToken).ConfigureAwait(false);

        if (_getDivisionBO.Error != null)
        {
            Errors = [_getDivisionBO.Error];

            return null;
        }

        registration.Division = division!;

        var tournament = await GetTournamentBO.ExecuteAsync(division!.Id, cancellationToken).ConfigureAwait(false);

        if (GetTournamentBO.Error is not null)
        {
            Errors = [GetTournamentBO.Error];

            return null;
        }

        registration.TournamentStartDate = tournament!.Start;
        registration.TournamentSweeperCount = tournament!.Sweepers.Count();

        var validatorResults = await Validator.ValidateAsync(registration, cancellationToken).ConfigureAwait(false);

        if (!validatorResults.IsValid)
        {
            Errors = validatorResults.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));

            return null;
        }

        if (registration.Bowler.Id.Value != Guid.Empty)
        {
            var searchCriteria = new Models.BowlerSearchCriteria
            {
                BowlerId = registration.Bowler.Id,
                RegisteredInTournament = tournament.Id
            };

            var registeredInTournament = (await SearchBowlerBO.ExecuteAsync(searchCriteria, cancellationToken).ConfigureAwait(false)).Any();

            if (SearchBowlerBO.Error is not null)
            {
                Errors = [SearchBowlerBO.Error];

                return null;
            }

            if (registeredInTournament)
            {
                Errors = [new Models.ErrorDetail("Bowler already registered for this tournament")];

                return null;
            }

            await UpdateBowlerBO.ExecuteAsync(registration.Bowler, cancellationToken).ConfigureAwait(false);

            if (UpdateBowlerBO.Errors.Any())
            {
                Errors = UpdateBowlerBO.Errors;

                return null;
            }
        }

        try
        {
            return await DataLayer.ExecuteAsync(registration, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Errors = [new Models.ErrorDetail(ex)];

            return null;
        }
    }

    public async Task<Models.Registration?> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
    {
        try
        {
            //UI search is doing a lot of the heavy lifting here.
            //when revisiting, add some validation methods to make sure the bowler is in the tournament
            //already and not already on the squad
            return await DataLayer.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Errors = [new Models.ErrorDetail(ex)];

            return null;
        }
    }
}

internal interface IBusinessLogic
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken);

    Task<Models.Registration?> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);
}