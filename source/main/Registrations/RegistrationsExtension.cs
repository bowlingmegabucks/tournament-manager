using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Registrations;

internal static class RegistrationsExtension
{
    public static IServiceCollection AddRegistrationsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();
        services.AddTransient<Add.Form>();

        services.AddTransient<Delete.IAdapter, Delete.Adapter>();

        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();
        services.AddTransient<Retrieve.RetrieveTournamentRegistrationsForm>();

        services.AddTransient<Update.IAdapter, Update.Adapter>();
        services.AddTransient<Update.UpdateRegistrationAverageForm>();
        services.AddTransient<Update.UpdateRegistrationDivisionForm>();

        return services;
    }
}
