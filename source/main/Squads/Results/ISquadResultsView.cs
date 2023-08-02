
namespace NortheastMegabuck.Squads.Results;
internal interface IView
{
    SquadId SquadId { get; }

    void DisplayError(string message);

    void BindResults(string divisionName, ICollection<IViewModel> scores);
}
