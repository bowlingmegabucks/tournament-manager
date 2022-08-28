using System.Runtime.CompilerServices;
using StronglyTypedIds;

[assembly: InternalsVisibleTo("NewEnglandClassic.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

[assembly: StronglyTypedIdDefaults(StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]