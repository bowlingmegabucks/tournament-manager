using System.Text.RegularExpressions;
using FluentValidation.Validators;
using FluentValidation;

namespace NewEnglandClassic.Validators;

internal class PhoneNumber<T> : PropertyValidator<T, string>
{
    public override string Name => "PhoneNumberValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "Property {PropertyName} is not a valid phone number.";

    public override bool IsValid(ValidationContext<T> context, string value)
     => !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^\s*\+?\s*([0-9][\s-]*){9,}$");
}

internal static partial class Extensions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    internal static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.SetValidator(new PhoneNumber<T>());
}
