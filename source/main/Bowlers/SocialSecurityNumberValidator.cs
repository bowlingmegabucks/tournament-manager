using FluentValidation;

namespace NortheastMegabuck.Bowlers;
internal sealed class SocialSecurityNumberValidator : FluentValidation.Validators.PropertyValidator<Models.Bowler, string>
{
    public override string Name => "SocialSecurityNumberValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "Property {PropertyName} is not a valid social security number.";

    public override bool IsValid(ValidationContext<Models.Bowler> context, string value)
    {
        var ssn = value.Decrypt();

        return ssn.Length == 9 && ssn.All(char.IsDigit);
    }
}

internal static class Extensions
{
    internal static IRuleBuilderOptions<Models.Bowler, string> SocialSecurityNumber(this IRuleBuilder<Models.Bowler, string> ruleBuilder)
        => ruleBuilder.SetValidator(new SocialSecurityNumberValidator());
}