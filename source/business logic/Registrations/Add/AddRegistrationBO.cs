
namespace NortheastMegabuck.Registrations.Add;

/// <summary>
/// 
/// </summary>
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

    public BusinessLogic(Divisions.Retrieve.IBusinessLogic getDivisionBO, Tournaments.Retrieve.IBusinessLogic getTournamentBO, Bowlers.Search.IBusinessLogic searchBowlerBO, Bowlers.Update.IBusinessLogic updateBowlerBO, FluentValidation.IValidator<Models.Registration> validator, IDataLayer dataLayer)
    {
        _getDivisionBO = getDivisionBO;
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => getTournamentBO);
        _searchBowlerBO = new Lazy<Bowlers.Search.IBusinessLogic>(() => searchBowlerBO);
        _updateBowlerBO = new Lazy<Bowlers.Update.IBusinessLogic>(() => updateBowlerBO);
        _validator = new Lazy<FluentValidation.IValidator<Models.Registration>>(() => validator);
        _dataLayer = new Lazy<IDataLayer>(() => dataLayer);
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="registration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(registration);

        var division = await _getDivisionBO.ExecuteAsync(registration.Division.Id, cancellationToken).ConfigureAwait(false);

        if (_getDivisionBO.ErrorDetail != null)
        {
            Errors = [_getDivisionBO.ErrorDetail];

            return null;
        }

        registration.Division = division!;

        var tournament = await GetTournamentBO.ExecuteAsync(division!.Id, cancellationToken).ConfigureAwait(false);

        if (GetTournamentBO.ErrorDetail is not null)
        {
            Errors = [GetTournamentBO.ErrorDetail];

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

            if (SearchBowlerBO.ErrorDetail is not null)
            {
                Errors = [SearchBowlerBO.ErrorDetail];

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowlerId"></param>
    /// <param name="squadId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

/// <summary>
/// 
/// </summary>
public interface IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    IEnumerable<Models.ErrorDetail> Errors { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="registration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<RegistrationId?> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowlerId"></param>
    /// <param name="squadId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.Registration?> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);
}