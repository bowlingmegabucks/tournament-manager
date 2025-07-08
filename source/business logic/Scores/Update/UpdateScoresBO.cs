using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Scores.Update;

/// <summary>
/// 
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    private readonly List<Models.ErrorDetail> _errors;

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors => _errors;

    private readonly IValidator<IEnumerable<Models.SquadScore>> _validator;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    public BusinessLogic(IConfiguration config)
    {
        _errors = [];
        _validator = new Validator();
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    internal BusinessLogic(IValidator<IEnumerable<Models.SquadScore>> mockValidator, IDataLayer mockDataLayer)
    {
        _errors = [];
        _validator = mockValidator;
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadScores"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<Models.SquadScore> squadScores, CancellationToken cancellationToken)
    {
        var valid = new List<Models.SquadScore>();
        var invalid = new List<Models.SquadScore>();

        var bowlerScores = squadScores.GroupBy(squadScore => squadScore.Bowler.Id);

        foreach (var bowlerScore in bowlerScores)
        {
            var result = await _validator.ValidateAsync(bowlerScore, cancellationToken).ConfigureAwait(false);

            if (result.IsValid)
            {
                valid.AddRange(bowlerScore);
            }
            else
            {
                invalid.AddRange(bowlerScore);
            }
        }

        try
        {
            await DataLayer.ExecuteAsync(valid, cancellationToken).ConfigureAwait(false);

            return invalid;
        }
        catch (Exception ex)
        {
            _errors.Add(new Models.ErrorDetail(ex.Message));

            return squadScores;
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
    /// <param name="squadScores"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<Models.SquadScore> squadScores, CancellationToken cancellationToken);
}