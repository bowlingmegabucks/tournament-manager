#if RELEASE
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
#endif

namespace NortheastMegabuck.UI;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var config = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
#if DEBUG
        config.AddUserSecrets<Tournaments.Retrieve.Form>();                      
#else
        config.AddJsonFile("appsettings.json");
        
        var builtConfiguration = config.Build();
        
        var kvUrl = builtConfiguration["KeyVaultConfig:KVUrl"];
        var tenantId = builtConfiguration["KeyVaultConfig:TenantId"];
        var clientId = builtConfiguration["KeyVaultConfig:ClientId"];
        var clientSecret = builtConfiguration["KeyVaultConfig:ClientSecret"];

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        var client = new SecretClient(new Uri(kvUrl), credential);
        config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
#endif

        Application.Run(new Tournaments.Retrieve.Form(config.Build()));
    }
}
