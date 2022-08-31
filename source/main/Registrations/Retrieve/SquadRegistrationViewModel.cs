using System.Text;

namespace NortheastMegabuck.Registrations.Retrieve;
internal class SquadRegistrationViewModel : ISquadRegistrationViewModel
{
    public BowlerId BowlerId {get;}

    public string BowlerName { get; }

    public string DivisionName { get; }

    public int DivisionNumber { get; }

    public string LaneAssignment { get; }

    public int Average { get; }

    public int Handicap { get; }

    public SquadRegistrationViewModel(Models.SquadRegistration registration)
    {
        BowlerId = registration.Bowler.Id;
        BowlerName = registration.Bowler.ToString();

        DivisionName = registration.Division.Name;
        DivisionNumber = registration.Division.Number;

        LaneAssignment = registration.LaneAssignment;
        Average = registration.Average.GetValueOrDefault(0);
        Handicap = registration.Handicap;
    }

    public override string ToString()
        => new StringBuilder(LaneAssignment)
            .Append('\t').Append('\t').Append(BowlerId)
            .Append('\t').Append(DivisionNumber)
            .Append('\t').Append(Handicap).ToString();
}

internal interface ISquadRegistrationViewModel
{ 
    BowlerId BowlerId { get; }

    string BowlerName { get; }

    string DivisionName { get; }

    int DivisionNumber { get; }

    string LaneAssignment { get; }

    int Average { get; }

    int Handicap { get; }
}
