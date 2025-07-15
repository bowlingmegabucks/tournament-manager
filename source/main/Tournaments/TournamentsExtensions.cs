using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Tournaments;
internal static class TournamentsExtensions
{
    public static IServiceCollection AddTournamentsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();
        services.AddTransient<Add.Form>();

        services.AddTransient<Portal.Form>();

        services.AddTransient<Results.AtLarge>();
        services.AddTransient<Results.IAdapter, Results.Adapter>();

        services.AddTransient<Retrieve.Form>();
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        services.AddTransient<Seeding.IAdapter, Seeding.Adapter>();
        services.AddTransient<Seeding.Form>();

        return services;
    }
}
