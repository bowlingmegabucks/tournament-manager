namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public class Bowler
{
    /// <summary>
    /// 
    /// </summary>
    public BowlerId Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public PersonName Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string StreetAddress { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string CityAddress { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string StateAddress { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string USBCId { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public string SocialSecurityNumber { get; set; } = string.Empty;

    internal Bowler(Database.Entities.Bowler bowler)
    {
        Id = bowler.Id;

        Name = new PersonName
        {
            First = bowler.FirstName,
            MiddleInitial = bowler.MiddleInitial,
            Last = bowler.LastName,
            Suffix = bowler.Suffix,
        };

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

    /// <summary>
    /// 
    /// </summary>
    public Bowler()
    {
        Name = new PersonName();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => Name.ToString();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
        => Id.GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
        => obj is Bowler model && Id == model.Id;
}
