using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Registrations;

internal static class RegistrationsExtension
{
    public static IServiceCollection AddRegistrationsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();

        services.AddTransient<Delete.IAdapter, Delete.Adapter>();

        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        services.AddTransient<Update.IAdapter, Update.Adapter>();

        return services;
    }
}
