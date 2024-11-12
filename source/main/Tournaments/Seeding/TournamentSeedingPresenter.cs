
namespace NortheastMegabuck.Tournaments.Seeding;
internal class Presenter
{
    private readonly IView _view;
    private readonly IAdapter _adapter;

    public Presenter(IConfiguration config, IView view)
    {
        _view = view;
        _adapter = new Adapter(config);
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
            _view.BindResults(divisionScores.Key, divisionScores.ToList());
        }
    }
}
