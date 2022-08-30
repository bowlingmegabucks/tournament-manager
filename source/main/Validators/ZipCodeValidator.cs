using FluentValidation.Validators;
using FluentValidation;

namespace NortheastMegabuck.Validators;
internal class ZipCode<T> : PropertyValidator<T, string>
{
    public override string Name => "ZipCodeValidator";

    public override bool IsValid(ValidationContext<T> context, string value)
        => !string.IsNullOrWhiteSpace(value) && (value.Length == 5 || value.Length == 9) && long.TryParse(value, out _);

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "Property {PropertyName} is not a valid zip code.";
}

internal static partial class Extensions
{
    internal static IRuleBuilderOptions<T, string> ZipCode<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.SetValidator(new ZipCode<T>());
}
