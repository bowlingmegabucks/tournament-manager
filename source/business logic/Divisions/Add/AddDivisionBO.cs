using FluentValidation;

namespace NortheastMegabuck.Divisions.Add;

/// <summary>
/// 
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly IValidator<Models.Division> _validator;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    internal BusinessLogic(IValidator<Models.Division> validator, IDataLayer dataLayer)
    {
        _validator = validator;
        _dataLayer = new Lazy<IDataLayer>(() => dataLayer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="division"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DivisionId?> ExecuteAsync(Models.Division division, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(division, cancellationToken).ConfigureAwait(false);

        if (!validation.IsValid)
        {
            Errors = validation.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));
            return null;
        }

        try
        {
            return await DataLayer.ExecuteAsync(division, cancellationToken).ConfigureAwait(false);
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
    /// <param name="division"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DivisionId?> ExecuteAsync(Models.Division division, CancellationToken cancellationToken);
}