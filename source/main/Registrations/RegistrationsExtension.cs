using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.Registrations;

internal static class RegistrationsExtension
{
    public static IServiceCollection AddRegistrationsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.Form>();

        services.AddTransient<Retrieve.RetrieveTournamentRegistrationsForm>();

        services.AddTransient<Update.UpdateRegistrationAverageForm>();
        services.AddTransient<Update.UpdateRegistrationDivisionForm>();

        return services;
    }
}
