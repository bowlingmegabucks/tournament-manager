namespace BowlingMegabucks.TournamentManager.LaneAssignments.Retrieve;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    async Task<IEnumerable<IViewModel>> IAdapter.ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
        => (await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false))
            .OrderBy(laneAssignment => laneAssignment.Bowler.Name.Last)
            .ThenBy(laneAssignment => laneAssignment.Bowler.Name.First)
            .Select(laneAssignment => new ViewModel(laneAssignment));
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}
