namespace NewEnglandClassic.UI;

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
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile("appsettings.json")
#if DEBUG
                            .AddUserSecrets<Tournaments.Retrieve.Form>()
#endif
                            .Build();

        Application.Run(new Tournaments.Retrieve.Form(config));
    }
}
