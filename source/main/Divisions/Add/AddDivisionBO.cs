using FluentValidation;

namespace NortheastMegabuck.Divisions.Add;
internal class BusinessLogic : IBusinessLogic
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    private readonly IValidator<Models.Division> _validator;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    internal BusinessLogic(IConfiguration config)
    {
        _validator = new Validator();
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockValidator"></param>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(IValidator<Models.Division> mockValidator, IDataLayer mockDataLayer)
    {
        _validator = mockValidator;
        _dataLayer = new Lazy<IDataLayer>(() => mockDataLayer);
    }

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
            Errors = new[] { new Models.ErrorDetail(ex) };
            return null;
        }
    }
}

internal interface IBusinessLogic
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<DivisionId?> ExecuteAsync(Models.Division division, CancellationToken cancellationToken);
}