using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Divisions.Add;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    internal Adapter(IConfiguration config)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => new BusinessLogic(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => mockBusinessLogic);
    }

    public Guid? Execute(IViewModel viewModel)
    {
        var model = new Models.Division(viewModel);

        var id = BusinessLogic.Execute(model);

        Errors = BusinessLogic.Errors;

        return id;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Guid? Execute(IViewModel viewModel);
}