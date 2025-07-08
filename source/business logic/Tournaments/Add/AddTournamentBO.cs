using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Tournaments.Add;

/// <summary>
/// 
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly IValidator<Models.Tournament> _validator;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    public BusinessLogic(IConfiguration config)
    {
        _validator = new Validator();
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(IValidator<Models.Tournament> mockValidator, IDataLayer mockDataLayer)
    {
        _validator = mockValidator;
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tournament"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TournamentId?> ExecuteAsync(Models.Tournament tournament, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(tournament, cancellationToken).ConfigureAwait(false);

        if (!validation.IsValid)
        {
            Errors = validation.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));
            return null;
        }

        try
        {
            return await DataLayer.ExecuteAsync(tournament, cancellationToken).ConfigureAwait(false);
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
    /// <param name="tournament"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TournamentId?> ExecuteAsync(Models.Tournament tournament, CancellationToken cancellationToken);
}