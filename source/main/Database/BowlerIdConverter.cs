using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NewEnglandClassic.Database;
internal class BowlerIdConverter : ValueConverter<BowlerId, Guid>
{
    public BowlerIdConverter() : base(id=> id.Value, value=> new BowlerId(value))
    {

    }
}
