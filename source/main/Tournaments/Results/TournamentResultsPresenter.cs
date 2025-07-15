
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Tournaments.Results;

internal class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _adapter;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;

        _adapter = services.GetRequiredService<IAdapter>();
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockView"></param>
    /// <param name="mockAdapter"></param>
    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    public async Task AtLargeAsync(CancellationToken cancellationToken)
    {
        var atLargeResults = (await _adapter.AtLargeAsync(_view.Id, cancellationToken).ConfigureAwait(true)).GroupBy(result => result.DivisionName);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);

            return;
        }

        foreach (var divisionResult in atLargeResults)
        {
            _view.BindResults(divisionResult.Key, divisionResult);
        }
    }
}
