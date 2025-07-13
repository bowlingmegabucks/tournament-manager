using Bogus;
using NortheastMegabuck.Api.Divisions;
using NortheastMegabuck.Models;

namespace NortheastMegabuck.Api.BogusData;

internal sealed class BogusDivisionDetailDto
    : Faker<DivisionDetailDto>
{
    public BogusDivisionDetailDto()
    {
        RuleFor(dto => dto.Id, _ => DivisionId.New());
        RuleFor(dto => dto.Name, f => f.Company.Random.Word() + " Division");

        RuleFor(dto => dto.MinimumAge, f => f.Random.Short(18, 60).OrNull(f));
        RuleFor(dto => dto.MaximumAge, (f, dto) =>
        {
            if (dto.MinimumAge.HasValue)
            {
                // 50% chance to be null, otherwise greater than MinimumAge
                return f.Random.Bool()
                    ? null
                    : f.Random.Short((short)(dto.MinimumAge.Value + 1), 65);
            }
            else
            {
                // 50% chance to be null, otherwise random between 18 and 65
                return f.Random.Short(18, 65).OrNull(f);
            }
        });

        // Minimum/Maximum Average: 150 to 230, same logic as age
        RuleFor(dto => dto.MinimumAverage, f => f.Random.Short(150, 230).OrNull(f));
        RuleFor(dto => dto.MaximumAverage, (f, dto) =>
        {
            if (dto.MinimumAverage.HasValue)
            {
                // 50% chance to be null, otherwise greater than MinimumAverage
                return f.Random.Bool()
                    ? null
                    : f.Random.Short((short)(dto.MinimumAverage.Value + 1), 230);
            }
            else
            {
                // 50% chance to be null, otherwise random between 150 and 230
                return f.Random.Short(150, 230).OrNull(f);
            }
        });

        RuleFor(dto => dto.Gender, f => f.PickRandom(Gender.Male, Gender.Female).OrNull(f));
    }
}