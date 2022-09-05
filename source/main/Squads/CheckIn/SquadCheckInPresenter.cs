using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Squads.CheckIn;
internal class Presenter
{
    private readonly IView _view;

    public Presenter(IView view)
    {
        _view = view;
    }

    public void Load()
    {
        var laneAssignemnts = GenerateLaneAssignments(_view.StartingLane, _view.NumberOfLanes, _view.MaxPerPair);

        _view.BuildLanes(laneAssignemnts);
    }

    //todo abstract to its own class
    private IEnumerable<string> GenerateLaneAssignments(int startingLane, int numberOfLanes, int maxPerPair)
    {
        var oddLetters = new List<string> { "A" };
        var evenLetters = new List<string>();

        switch (maxPerPair)
        {
            case 2:
                evenLetters.Add("B");
                break;
            case 3:
                evenLetters.Add("B");
                evenLetters.Add("C");
                break;
            case 4:
                oddLetters.Add("B");
                evenLetters.Add("C");
                evenLetters.Add("D");
                break;
            case 5:
                oddLetters.Add("B");
                evenLetters.Add("C");
                evenLetters.Add("D");
                evenLetters.Add("E");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(maxPerPair));
        }

        var laneAssignments = new List<string>();

        for (var i = startingLane; i <= startingLane + numberOfLanes - 1; i += 2)
        {
            laneAssignments.AddRange(oddLetters.Select(letter => $"{i}{letter}"));
            laneAssignments.AddRange(evenLetters.Select(letter => $"{i+1}{letter}"));
        }

        return laneAssignments;
    }
}
