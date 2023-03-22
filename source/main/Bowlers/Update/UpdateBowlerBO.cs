using FluentValidation;

namespace NortheastMegabuck.Bowlers.Update;

internal sealed class BusinessLogic : IBusinessLogic
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    private readonly Lazy<IValidator<Models.PersonName>> _nameValidator;
    private IValidator<Models.PersonName> NameValidator => _nameValidator.Value;

    private readonly IDataLayer _dataLayer;

    public BusinessLogic(IConfiguration config)
    {
        _nameValidator = new Lazy<IValidator<Models.PersonName>>(() => new PersonNameValidator());
        _dataLayer = new DataLayer(config);
    }

    internal BusinessLogic(IValidator<Models.PersonName> mockNameValidator, IDataLayer mockDataLayer)
    {
        _nameValidator = new Lazy<IValidator<Models.PersonName>>(() => mockNameValidator);
        _dataLayer = mockDataLayer;
    }

    void IBusinessLogic.Execute(BowlerId id, Models.PersonName name)
    {
        var validation = NameValidator.Validate(name);

        if (!validation.IsValid)
        {
            Errors = validation.Errors.Select(error => new Models.ErrorDetail(error.ErrorMessage));

            return;
        }

        try
        {
            _dataLayer.Execute(id, name);
        }
        catch (Exception ex)
        {
            Errors = new[] { new Models.ErrorDetail(ex) };
        }
    }
}

internal interface IBusinessLogic
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    void Execute(BowlerId id, Models.PersonName name);
}