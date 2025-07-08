
namespace NortheastMegabuck.Bowlers.Update;

internal interface INameViewModel
{
    string FirstName { get; }

    string MiddleInitial { get; }

    string LastName { get; }

    string Suffix { get; }
}

internal static class NameViewModelExtensions
{
    public static Models.PersonName ToPersonName(this INameViewModel viewModel)
    {
        return new Models.PersonName
        {
            First = viewModel.FirstName,
            MiddleInitial = viewModel.MiddleInitial,
            Last = viewModel.LastName,
            Suffix = viewModel.Suffix,
        };
    }
}