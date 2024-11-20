
using FluentValidation;
using NortheastMegabuck.Models;

namespace NortheastMegabuck.Registrations.Update;

internal sealed class BusinessLogic : IBusinessLogic
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly IDataLayer _dataLayer;

    private readonly Retrieve.IBusinessLogic _retrieveBusinessLogic;
    private readonly Tournaments.Retrieve.IBusinessLogic _retrieveTournamentBusinessLogic;

    private readonly Lazy<Divisions.Retrieve.IBusinessLogic> _getDivisionBO;
    private Divisions.Retrieve.IBusinessLogic GetDivisionBO => _getDivisionBO.Value;

    private readonly Lazy<Tournaments.Retrieve.IBusinessLogic> _getTournamentBO;
    private Tournaments.Retrieve.IBusinessLogic GetTournamentBO => _getTournamentBO.Value;

    private readonly Lazy<IValidator<UpdateRegistrationModel>> _validator;
    private IValidator<UpdateRegistrationModel> Validator => _validator.Value;

    public BusinessLogic(IConfiguration config)
    {
        _dataLayer = new DataLayer(config);
        _retrieveBusinessLogic = new Retrieve.BusinessLogic(config);
        _retrieveTournamentBusinessLogic = new Tournaments.Retrieve.BusinessLogic(config);

        _getDivisionBO = new Lazy<Divisions.Retrieve.IBusinessLogic>(() => new Divisions.Retrieve.BusinessLogic(config));
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => new Tournaments.Retrieve.BusinessLogic(config));
        _validator = new Lazy<IValidator<UpdateRegistrationModel>>(() => new Validator());
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataLayer"></param>
    /// <param name="mockRetrieveBusinessLogic"></param>
    /// <param name="mockTournamentBusinessLogic"></param>
    /// <param name="mockGetDivisionBO"></param>
    /// <param name="mockGetTournamentBO"></param>
    /// <param name="mockValidator"></param>
    internal BusinessLogic(IDataLayer mockDataLayer, Retrieve.IBusinessLogic mockRetrieveBusinessLogic, Tournaments.Retrieve.IBusinessLogic mockTournamentBusinessLogic, Divisions.Retrieve.IBusinessLogic mockGetDivisionBO, Tournaments.Retrieve.IBusinessLogic mockGetTournamentBO, IValidator<UpdateRegistrationModel> mockValidator)
    {
        _dataLayer = mockDataLayer;
        _retrieveBusinessLogic = mockRetrieveBusinessLogic;
        _retrieveTournamentBusinessLogic = mockTournamentBusinessLogic;

        _getDivisionBO = new Lazy<Divisions.Retrieve.IBusinessLogic>(() => mockGetDivisionBO);
        _getTournamentBO = new Lazy<Tournaments.Retrieve.IBusinessLogic>(() => mockGetTournamentBO);
        _validator = new Lazy<IValidator<UpdateRegistrationModel>>(() => mockValidator);
    }

    public async Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        var registration = await _retrieveBusinessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        if (_retrieveBusinessLogic.Error is not null)
        {
            Errors = [_retrieveBusinessLogic.Error];

            return;
        }

        if (registration!.Sweepers.Any(sweeper => sweeper.Complete))
        {
            Errors = [new Models.ErrorDetail("Cannot add super sweeper to registration with completed sweepers.")];

            return;
        }

        var tournament = await _retrieveTournamentBusinessLogic.ExecuteAsync(registration.Id, cancellationToken).ConfigureAwait(false);

        if (_retrieveTournamentBusinessLogic.Error is not null)
        {
            Errors = [_retrieveTournamentBusinessLogic.Error];

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

    public async Task ExecuteAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken)
    {
        var division = await GetDivisionBO.ExecuteAsync(divisionId, cancellationToken).ConfigureAwait(false);

        if (GetDivisionBO.Error is not null)
        {
            Errors = [GetDivisionBO.Error];

            return;
        }

        var tournament = await GetTournamentBO.ExecuteAsync(division!.Id, cancellationToken).ConfigureAwait(false);

        if (GetTournamentBO.Error is not null)
        {
            Errors = [GetTournamentBO.Error];

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
}

internal interface IBusinessLogic
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, DivisionId divisionId, Gender? gender, int? average, string usbcId, DateOnly? dateOfBirth, CancellationToken cancellationToken);
}
