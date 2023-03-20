
namespace NortheastMegabuck.Bowlers.Update;

internal interface INameViewModel
{
    BowlerId Id { get; }

    string FirstName { get; }

    string MiddleInitial { get; }

    string LastName { get; }

    string Suffix { get; }
}
