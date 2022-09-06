namespace NortheastMegabuck.LaneAssignments;
internal class Presenter
{
    private readonly IView _view;
    private readonly ILaneAvailability _laneAvailability;
    public Presenter(IView view)
    {
        _view = view;
        _laneAvailability = new LaneAvailability();
    }

    public void Load()
    {
        var laneAssignemnts = _laneAvailability.Generate(_view.StartingLane, _view.NumberOfLanes, _view.MaxPerPair);

        _view.BuildLanes(laneAssignemnts);
    }
}
