using StronglyTypedIds;

[assembly:StronglyTypedIdDefaults(StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.SystemTextJson)]

namespace NewEnglandClassic;

[StronglyTypedId]
internal partial struct BowlerId { }

[StronglyTypedId]
internal partial struct DivisionId { }

[StronglyTypedId]
internal partial struct RegistrationId { }

[StronglyTypedId]
internal partial struct SquadId { }
