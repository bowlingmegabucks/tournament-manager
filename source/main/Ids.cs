using StronglyTypedIds;

[assembly:StronglyTypedIdDefaults(StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]

namespace NewEnglandClassic;

[StronglyTypedId]
public partial struct BowlerId { }

[StronglyTypedId]
public partial struct DivisionId { }

[StronglyTypedId]
public partial struct RegistrationId { }

[StronglyTypedId]
public partial struct SquadId { }

[StronglyTypedId]
public partial struct TournamentId { }
