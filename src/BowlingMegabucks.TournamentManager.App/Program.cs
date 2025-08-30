using BowlingMegabucks.TournamentManager.App.Tournaments;
using BowlingMegabucks.TournamentManager.App.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Presentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.App;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
#pragma warning disable S125 // Sections of code should not be commented out
    {
        ApplicationConfiguration.Initialize();

        IConfigurationBuilder configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

#if DEBUG
        configBuilder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        configBuilder.AddUserSecrets<IAppMarker>(optional: true, reloadOnChange: true);
#endif

        configBuilder.AddEnvironmentVariables();

        IConfiguration config = configBuilder.Build();
        var services = new ServiceCollection();
        services.AddSingleton(config);

        services
            .AddPresentationServices(config)
            .AddTournaments();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

#if WINDOWS
        IGetTournamentsFormFactory formFactory = serviceProvider.GetRequiredService<IGetTournamentsFormFactory>();
        using GetTournamentsForm form = formFactory.Create();

        Application.Run(form);
#endif
    }
#pragma warning restore S125 // Sections of code should not be commented out
}
