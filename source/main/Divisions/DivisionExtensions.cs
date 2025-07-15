using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Divisions;
internal static class DivisionExtensions
{
    public static IServiceCollection AddDivisionModule(this IServiceCollection services)
    {
        services.AddTransient<Add.Form>();
        services.AddTransient<Add.IAdapter, Add.Adapter>();

        services.AddTransient<Retrieve.Form>();
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        return services;
    }
}
