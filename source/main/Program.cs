#if RELEASE
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
#endif

using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BowlingMegabucks.TournamentManager.Bowlers;
using BowlingMegabucks.TournamentManager.Divisions;
using BowlingMegabucks.TournamentManager.LaneAssignments;
using BowlingMegabucks.TournamentManager.Registrations;
using BowlingMegabucks.TournamentManager.Scores;
using BowlingMegabucks.TournamentManager.Squads;
using BowlingMegabucks.TournamentManager.Sweepers;
using BowlingMegabucks.TournamentManager.Tournaments;
using QuestPDF.Infrastructure;

namespace BowlingMegabucks.TournamentManager;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var configBuilder = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddEnvironmentVariables();
#if DEBUG
        configBuilder.AddUserSecrets<Tournaments.Retrieve.Form>();
        configBuilder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
#else
        configBuilder.AddJsonFile("appsettings.json");

        var builtConfiguration = configBuilder.Build();

        var kvUrl = builtConfiguration["KEYVAULT_URL"]!;
        var tenantId = builtConfiguration["KeyVaultConfig:TenantId"];
        var clientId = builtConfiguration["KeyVaultConfig:ClientId"];
        var clientSecret = builtConfiguration["KEYVAULT_CLIENT_SECRET"];

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        var client = new SecretClient(new Uri(kvUrl), credential);
        configBuilder.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
#endif

        var config = configBuilder.Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);

        Encryption.Key = config["EncryptionKey"] ?? throw new ConfigurationErrorsException("Cannot get encryption key");

        QuestPDF.Settings.License = LicenseType.Community;

        services.AddPresentation(config)
            .AddBowlersModule()
            .AddDivisionModule()
            .AddRegistrationsModule()
            .AddScoresModule()
            .AddSquadsModule()
            .AddSweepersModule()
            .AddTournamentsModule();

#if WINDOWS
        using var form = services.BuildServiceProvider().GetRequiredService<Tournaments.Retrieve.Form>();
        Application.Run(form);
#endif
    }
}
