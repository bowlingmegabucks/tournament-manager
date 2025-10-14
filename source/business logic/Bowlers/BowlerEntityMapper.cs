
namespace BowlingMegabucks.TournamentManager.Bowlers;
internal class EntityMapper : IEntityMapper
{
    public Database.Entities.Bowler Execute(Models.Bowler bowler)
        => new()
        {
            Id = bowler.Id,
            FirstName = bowler.Name.First,
            MiddleInitial = bowler.Name.MiddleInitial,
            LastName = bowler.Name.Last,
            Suffix = bowler.Name.Suffix,
            StreetAddress = bowler.StreetAddress,
            CityAddress = bowler.CityAddress,
            StateAddress = bowler.StateAddress,
            ZipCode = bowler.ZipCode,
            EmailAddress = bowler.EmailAddress,
            DateOfBirth = bowler.DateOfBirth,
            Gender = bowler.Gender,
            USBCId = bowler.USBCId,
            PhoneNumber = ExtractMain10Digits(bowler.PhoneNumber) ?? string.Empty,
            SocialSecurityNumber = bowler.SocialSecurityNumber
        };

    private static string? ExtractMain10Digits(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        var digits = new System.Text.StringBuilder(10);
        var skippedCountryCode = false;
        var trimmedInput = input.TrimStart();
        foreach (var c in input)
        {
            if (char.IsDigit(c))
            {
                // Skip the first digit if it's '1' and we haven't collected any digits yet (likely country code)
                if (digits.Length == 0 && c == '1' && !skippedCountryCode &&
                    (trimmedInput.StartsWith("+1", StringComparison.Ordinal) || 
                     trimmedInput.StartsWith("1 ", StringComparison.Ordinal)))
                {
                    skippedCountryCode = true;
                    continue;
                }
                
                digits.Append(c);
                if (digits.Length == 10)
                {
                    break;
                }
            }
        }

        return digits.Length == 10 ? digits.ToString() : null;
    }
}

internal interface IEntityMapper
{
    Database.Entities.Bowler Execute(Models.Bowler bowler);
}