using FluentValidation;

namespace NortheastMegabuck.Bowlers.Update;

/// <summary>
/// 
/// </summary>
internal sealed class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly Lazy<IValidator<Models.PersonName>> _nameValidator;
    private IValidator<Models.PersonName> NameValidator => _nameValidator.Value;

    private readonly Lazy<IValidator<Models.Bowler>> _bowlerValidator;
    private IValidator<Models.Bowler> BowlerValidator => _bowlerValidator.Value;

    private readonly IDataLayer _dataLayer;

    public BusinessLogic(IValidator<Models.PersonName> nameValidator, IUpdateBowlerValidator bowlerValidator, IDataLayer dataLayer)
    {
        _nameValidator = new Lazy<IValidator<Models.PersonName>>(() => nameValidator);
        _bowlerValidator = new Lazy<IValidator<Models.Bowler>>(() => bowlerValidator);

        _dataLayer = dataLayer;
    }

    async Task IBusinessLogic.ExecuteAsync(BowlerId id, Models.PersonName name, CancellationToken cancellationToken)
    {
        var validation = await NameValidator.ValidateAsync(name, cancellationToken).ConfigureAwait(false);

        if (!validation.IsValid)
        {
            Errors = validation.Errors.Select(error => new Models.ErrorDetail(error.ErrorMessage));

            return;
        }

        try
        {
            await _dataLayer.ExecuteAsync(id, name, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Errors = [new Models.ErrorDetail(ex)];
        }
    }

    async Task IBusinessLogic.ExecuteAsync(Models.Bowler bowler, CancellationToken cancellationToken)
    {
        var validation = await BowlerValidator.ValidateAsync(bowler, cancellationToken).ConfigureAwait(false);

        if (!validation.IsValid)
        {
            Errors = validation.Errors.Select(error => new Models.ErrorDetail(error.ErrorMessage));

            return;
        }

        try
        {
            await _dataLayer.ExecuteAsync(bowler, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Errors = [new Models.ErrorDetail(ex)];
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
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(BowlerId id, Models.PersonName name, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(Models.Bowler bowler, CancellationToken cancellationToken);
}