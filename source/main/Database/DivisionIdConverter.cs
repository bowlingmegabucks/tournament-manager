using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NewEnglandClassic.Database;
internal class DivisionIdConverter : ValueConverter<DivisionId, Guid>
{
    public DivisionIdConverter() : base(id=> id.Value, value=> new DivisionId(value))
    {

    }
}
