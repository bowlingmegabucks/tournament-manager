namespace NortheastMegabuck.Squads;

/// <summary>
/// 
/// </summary>
public sealed class HandicapCalculator : IHandicapCalculatorInternal
{
    int IHandicapCalculatorInternal.Calculate(Database.Entities.Registration registration)
        => Calculate(registration.Average, registration.Division.HandicapBase, registration.Division.HandicapPercentage, registration.Division.MaximumHandicapPerGame);

    int IHandicapCalculator.Calculate(Models.Registration registration)
        => Calculate(registration.Average, registration.Division.HandicapBase, registration.Division.HandicapPercentage, registration.Division.MaximumHandicapPerGame);

    private static int Calculate(int? average, int? handicapBase, decimal? handicapPercentage, int? maxHandicap)
    {
        if (!(average.HasValue && handicapBase.HasValue && handicapPercentage.HasValue))
        {
            return 0;
        }

        if (average >= handicapBase)
        {
            return 0;
        }

        var handicap = (int)Math.Floor((handicapBase.Value - average.Value) * handicapPercentage.Value);

        return Math.Min(handicap, maxHandicap.GetValueOrDefault(999));
    }
}

internal interface IHandicapCalculatorInternal
    : IHandicapCalculator
{
    int Calculate(Database.Entities.Registration registration);
}

/// <summary>
/// 
/// </summary>
public interface IHandicapCalculator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registration"></param>
    /// <returns></returns>
    int Calculate(Models.Registration registration);
}