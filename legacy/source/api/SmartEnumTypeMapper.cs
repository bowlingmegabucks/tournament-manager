using Ardalis.SmartEnum;
using NJsonSchema;
using NJsonSchema.Generation.TypeMappers;

namespace BowlingMegabucks.TournamentManager.Api;

internal class SmartEnumTypeMapper<TEnum, TVal> : PrimitiveTypeMapper
    where TEnum : SmartEnum<TEnum, TVal>
    where TVal : IComparable<TVal>, IEquatable<TVal>
{
    public SmartEnumTypeMapper() : base(typeof(TEnum), x => {
        x.Type = JsonObjectType.String;

        foreach (var item in SmartEnum<TEnum, TVal>.List.OrderBy(e => e.Value))
        {
            x.Enumeration.Add(item.Name);
        }
    }) { }
}

internal sealed class SmartEnumTypeMapper<T> : SmartEnumTypeMapper<T, int>
    where T : SmartEnum<T>
{
}