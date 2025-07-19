
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Squads.Results;
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

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var scoresByDivision = (await _adapter.ExecuteAsync(_view.SquadId, cancellationToken).ConfigureAwait(true)).OrderBy(score => score.Key);

        if (_adapter.Error != null)
        {
            _view.DisplayError(_adapter.Error.Message);

            return;
        }

        foreach (var divisionScores in scoresByDivision)
        {
            _view.BindResults(divisionScores.Key, divisionScores.Any(score => score.Handicap > 0), [.. divisionScores]);
        }
    }
}
