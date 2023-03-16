
using System.Text;

namespace NortheastMegabuck.Models;

internal class Bowler
{
    public BowlerId Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string MiddleInitial { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Suffix { get; set; } = string.Empty;

    public string StreetAddress { get; set; } = string.Empty;

    public string CityAddress { get; set; } = string.Empty;

    public string StateAddress { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string USBCId { get; set; } = string.Empty;

    public DateOnly? DateOfBirth { get; set; }

    public Gender? Gender { get; set; }

    public int? Age => AgeOn(DateOnly.FromDateTime(DateTime.Today));

    internal int? AgeOn(DateOnly date)
    {
        if (!DateOfBirth.HasValue)
        {
            return null;
        }

        var age = date.Year - DateOfBirth.Value.Year;

        if (DateOfBirth > date.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    public string SocialSecurityNumber { get; set; } = string.Empty;

    public Bowler(Database.Entities.Bowler bowler)
    {
        Id = bowler.Id;
        FirstName = bowler.FirstName;
        MiddleInitial = bowler.MiddleInitial;
        LastName = bowler.LastName;
        Suffix = bowler.Suffix;
        StreetAddress = bowler.StreetAddress;
        CityAddress = bowler.CityAddress;
        StateAddress = bowler.StateAddress;
        ZipCode = bowler.ZipCode;
        EmailAddress = bowler.EmailAddress;
        USBCId = bowler.USBCId;
        PhoneNumber = bowler.PhoneNumber;
        DateOfBirth = bowler.DateOfBirth;
        Gender = bowler.Gender;
        SocialSecurityNumber = bowler.SocialSecurityNumber;
    }

    public Bowler(Bowlers.Add.IViewModel viewModel)
    {
        Id = viewModel.Id;
        FirstName = viewModel.FirstName;
        MiddleInitial = viewModel.MiddleInitial;
        LastName = viewModel.LastName;
        Suffix = viewModel.Suffix;
        StreetAddress = viewModel.StreetAddress;
        CityAddress = viewModel.CityAddress;
        StateAddress = viewModel.StateAddress;
        ZipCode = viewModel.ZipCode;
        EmailAddress = viewModel.EmailAddress;
        PhoneNumber = viewModel.PhoneNumber;
        USBCId = viewModel.USBCId;
        DateOfBirth = viewModel.DateOfBirth;
        Gender = viewModel.Gender;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Bowler()
    {

    }

    public override string ToString()
    {
        var name = new StringBuilder($"{FirstName} {LastName}");

        if (!string.IsNullOrEmpty(Suffix))
        {
            name.Append($", {Suffix}");
        }

        return name.ToString();
    }

    public override int GetHashCode()
        => Id.GetHashCode();

    public override bool Equals(object? obj) 
        => obj != null && obj is Bowler model && Id == model.Id;
}
