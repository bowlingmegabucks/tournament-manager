
using FluentValidation;

namespace NortheastMegabuck.Registrations.Update;

/// <summary>
/// 
/// </summary>
internal sealed class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly IDataLayer _dataLayer;

    private readonly Retrieve.IBusinessLogic _retrieveBusinessLogic;
    private readonly Tournaments.Retrieve.IBusinessLogic _retrieveTournamentBusinessLogic;

    private readonly Lazy<Divisions.Retrieve.IBusinessLogic> _getDivisionBO;
    private Divisions.Retrieve.IBusinessLogic GetDivisionBO => _getDivisionBO.Value;

    private readonly Lazy<Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Tournaments.Retrieve.IBusinessLogic GetTournamentBO => _getTournamentBO.Value;

    private readonly Lazy<Scores.IRepository> _scoresRepository;
    private Scores.IRepository ScoresRepository => _scoresRepository.Value;

    private readonly Lazy<IValidator<UpdateRegistrationModel>> _validator;
    private IValidator<UpdateRegistrationModel> Validator => _validator.Value;

    public BusinessLogic(IDataLayer dataLayer, Retrieve.IBusinessLogic retrieveBusinessLogic,
        Tournaments.Retrieve.IBusinessLogic tournamentBusinessLogic, Divisions.Retrieve.IBusinessLogic getDivisionBO,
        Tournaments.Retrieve.IBusinessLogic getTournamentBO, IValidator<UpdateRegistrationModel> validator,
        Scores.IRepository scoresRepository)
    {
        _dataLayer = dataLayer;
        _retrieveBusinessLogic = retrieveBusinessLogic;
        _retrieveTournamentBusinessLogic = tournamentBusinessLogic;

        _getDivisionBO = new Lazy<Divisions.Retrieve.IBusinessLogic>(() => getDivisionBO);
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => getTournamentBO);
        _validator = new Lazy<IValidator<UpdateRegistrationModel>>(() => validator);
        _scoresRepository = new Lazy<Scores.IRepository>(() => scoresRepository);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        var registration = await _retrieveBusinessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        if (_retrieveBusinessLogic.ErrorDetail is not null)
        {
            Errors = [_retrieveBusinessLogic.ErrorDetail];

            return;
        }

        if (registration!.Sweepers.Any(sweeper => sweeper.Complete))
        {
            Errors = [new Models.ErrorDetail("Cannot add super sweeper to registration with completed sweepers.")];

            return;
        }

        var tournament = await _retrieveTournamentBusinessLogic.ExecuteAsync(registration.Id, cancellationToken).ConfigureAwait(false);

        if (_retrieveTournamentBusinessLogic.ErrorDetail is not null)
        {
            Errors = [_retrieveTournamentBusinessLogic.ErrorDetail];

            return;
        }

        if (!tournament!.Sweepers.Any())
        {
            Errors = [new Models.ErrorDetail("Cannot add super sweeper to tournament without sweepers.")];

            return;
        }

        if (tournament!.Sweepers.Count() != tournament.Sweepers.Count())
        {
            Errors = [new Models.ErrorDetail("Bowler is not registered for all sweepers.")];

            return;
        }

        if (registration.SuperSweeper)
        {
            Errors = [new Models.ErrorDetail("Bowler is already registered for the super sweeper.")];

            return;
        }

        await _dataLayer.ExecuteAsync(id, true, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="divisionId"></param>
    /// <param name="gender"></param>
    /// <param name="average"></param>
    /// <param name="usbcId"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(RegistrationId id, DivisionId divisionId, Models.Gender? gender, int? average, string usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken)
    {
        var division = await GetDivisionBO.ExecuteAsync(divisionId, cancellationToken).ConfigureAwait(false);

        if (GetDivisionBO.ErrorDetail is not null)
        {
            Errors = [GetDivisionBO.ErrorDetail];

            return;
        }

        var tournament = await GetTournamentBO.ExecuteAsync(division!.Id, cancellationToken).ConfigureAwait(false);

        if (GetTournamentBO.ErrorDetail is not null)
        {
            Errors = [GetTournamentBO.ErrorDetail];

            return;
        }

        var hasBowlerAlreadyBowledTournament = await ScoresRepository.DoesBowlerHaveAnyScoresForTournamentAsync(id, tournament!.Id, cancellationToken).ConfigureAwait(false);

        if (hasBowlerAlreadyBowledTournament)
        {
            Errors = [new Models.ErrorDetail("Cannot change bowler division after scores have been recorded.")];
            return;
        }

        var model = new UpdateRegistrationModel
        {
            Division = division,
            TournamentStartDate = tournament!.Start,
            Average = average,
            USBCId = usbcId,
            Gender = gender,
            DateOfBirth = dateOfBirth
        };

        var validatorResults = await Validator.ValidateAsync(model, cancellationToken).ConfigureAwait(false);

        if (!validatorResults.IsValid)
        {
            Errors = validatorResults.Errors.Select(error => new Models.ErrorDetail(error.ErrorMessage)).ToList();

            return;
        }

        try
        {
            await _dataLayer.ExecuteAsync(id, divisionId, gender, average, usbcId, dateOfBirth, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Errors = [new Models.ErrorDetail(ex.Message)];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="average"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(RegistrationId id, int? average, CancellationToken cancellationToken)
    {
        if (average.HasValue)
        {
            if (average.Value <= 0)
            {
                Errors = [new Models.ErrorDetail("Average must be greater than 0.")];

                return;
            }

            if (average.Value > 300)
            {
                Errors = [new Models.ErrorDetail("Average must be less than or equal to 300.")];

                return;
            }
        }

        try
        {
            await _dataLayer.ExecuteAsync(id, average, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Errors = [new Models.ErrorDetail(ex.Message)];
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
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="divisionId"></param>
    /// <param name="gender"></param>
    /// <param name="average"></param>
    /// <param name="usbcId"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(RegistrationId id, DivisionId divisionId, Models.Gender? gender, int? average, string usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="average"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(RegistrationId id, int? average, CancellationToken cancellationToken);
}
