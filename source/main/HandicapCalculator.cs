
namespace NortheastMegabuck;
internal class HandicapCalculator : IHandicapCalculator
{
    int IHandicapCalculator.Calculate(Database.Entities.Registration registration)
        => Calculate(registration.Average, registration.Division.HandicapBase, registration.Division.HandicapPercentage, registration.Division.MaximumHandicapPerGame);

    int IHandicapCalculator.Calculate(Models.Registration registration)
        => Calculate(registration.Average, registration.Division.HandicapBase, registration.Division.HandicapPercentage, registration.Division.MaximumHandicapPerGame);
    
    private int Calculate(int? average, int? handicapBase, decimal? handicapPercentage, int? maxHandicap)
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

internal interface IHandicapCalculator
{
    int Calculate(Database.Entities.Registration registration);

    int Calculate(Models.Registration registration);
}
