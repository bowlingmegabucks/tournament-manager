
namespace NortheastMegabuck.Sweepers.Add;

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

    private readonly Lazy<FluentValidation.IValidator<Models.Sweeper>> _validator;
    private FluentValidation.IValidator<Models.Sweeper> Validator => _validator.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    internal BusinessLogic(Tournaments.Retrieve.IBusinessLogic getTournamentBO, FluentValidation.IValidator<Models.Sweeper> validator, IDataLayer dataLayer)
    {
        _getTournamentBO = getTournamentBO;
        _validator = new Lazy<FluentValidation.IValidator<Models.Sweeper>>(() => validator);
        _dataLayer = new Lazy<IDataLayer>(() => dataLayer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sweeper"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SquadId?> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(sweeper);

        var tournament = await _getTournamentBO.ExecuteAsync(sweeper.TournamentId, cancellationToken).ConfigureAwait(false);

        if (_getTournamentBO.ErrorDetail != null)
        {
            Errors = [_getTournamentBO.ErrorDetail];

            return null;
        }

        sweeper.Tournament = tournament!;

        var validationResults = await Validator.ValidateAsync(sweeper, cancellationToken).ConfigureAwait(false);

        if (!validationResults.IsValid)
        {
            Errors = validationResults.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));

            return null;
        }

        try
        {
            return await DataLayer.ExecuteAsync(sweeper, cancellationToken).ConfigureAwait(false);
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
    /// <param name="sweeper"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SquadId?> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken);
}
