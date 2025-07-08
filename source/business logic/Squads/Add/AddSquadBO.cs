using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Squads.Add;

/// <summary>
/// 
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly Tournaments.Retrieve.IBusinessLogic _getTournamentBO;

    private readonly Lazy<FluentValidation.IValidator<Models.Squad>> _validator;
    private FluentValidation.IValidator<Models.Squad> Validator => _validator.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squad"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SquadId?> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(squad);

        var tournament = await _getTournamentBO.ExecuteAsync(squad.TournamentId, cancellationToken).ConfigureAwait(false);

        if (_getTournamentBO.ErrorDetail != null)
        {
            Errors = [_getTournamentBO.ErrorDetail];

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
    /// <param name="squad"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SquadId?> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken);
}
