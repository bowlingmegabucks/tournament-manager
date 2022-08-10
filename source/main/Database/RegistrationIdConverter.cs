using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NewEnglandClassic.Database;
internal class RegistrationIdConverter : ValueConverter<RegistrationId, Guid>
{
    public RegistrationIdConverter() : base(id=> id.Value, value=> new RegistrationId(value))
    {

    }
}
