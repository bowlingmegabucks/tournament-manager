
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Tournaments.Seeding;
internal class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _adapter;

    public Presenter(IView view, IServiceProvider services)
    {
        _view = view;
        
        _adapter = services.GetRequiredService<IAdapter>();
    }

    internal Presenter(IView mockView, IAdapter mockAdapter)
    {
        _view = mockView;
        _adapter = mockAdapter;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var scoresByDivision = (await _adapter.ExecuteAsync(_view.Id, cancellationToken).ConfigureAwait(true)).GroupBy(score => score.DivisionName);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);

            return;
        }

        foreach (var divisionScores in scoresByDivision)
        {
            _view.BindResults(divisionScores.Key, [.. divisionScores]);
        }
    }
}
