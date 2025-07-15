using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Sweepers;
internal static class SweepersExtensions
{
    public static IServiceCollection AddSweepersModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();
        services.AddTransient<Add.Form>();

        services.AddTransient<Complete.IAdapter, Complete.Adapter>();

        services.AddTransient<Portal.Form>();

        services.AddTransient<Results.IAdapter, Results.Adapter>();
        services.AddTransient<Results.Form>();

        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();
        services.AddTransient<Retrieve.Form>();

        return services;
    }
}
