using FluentValidation.Validators;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Validators;
internal class PastDate<T> : PropertyValidator<T, DateOnly>
{
    public override string Name => "PastDateValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    => "Property {PropertyName} must be in the past";

    public override bool IsValid(ValidationContext<T> context, DateOnly value)
     => value < DateOnly.FromDateTime(DateTime.Today);
}

internal static partial class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    internal static IRuleBuilderOptions<T, DateOnly> PastDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
        => ruleBuilder.SetValidator(new PastDate<T>());

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    internal static IRuleBuilderOptions<T, DateOnly?> PastDate<T>(this IRuleBuilder<T, DateOnly?> ruleBuilder)
        => ruleBuilder.SetValidator(new PastDate<T>());
}
